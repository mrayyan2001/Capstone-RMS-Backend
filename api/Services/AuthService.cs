using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Client;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace api.Services
{
    public class AuthService : IAuthService
    {
        private readonly string _connString;
        public AuthService(IConfiguration config)
        {
            _connString = config.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException("No Connection String");
        }

        public async Task<ClientInfoDTO?> GetUserByEmail(string email)
        {
            ClientInfoDTO targetUser = new ClientInfoDTO();
            string query = "select [id],[UserNameHash],[PasswordHash],[Email],[FirstName],[LastName],[IsLogging],[IsActive] from Users where [Email]=@email";
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@email", email);
                    conn.Open();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            targetUser.Email = reader.GetString("Email");
                            targetUser.Id = reader.GetInt32("id");
                            targetUser.UserNameHash = reader.GetString("UserNameHash");
                            targetUser.PasswordHash = reader.GetString("PasswordHash");
                            targetUser.FirstName = reader.GetString("FirstName");
                            targetUser.LastName = reader.GetString("LastName");
                            targetUser.IsLogging = reader.GetBoolean("IsLogging");
                            targetUser.IsActive = reader.GetBoolean("IsActive");

                            return targetUser;
                        }
                    }
                    return null;
                }

            }
        }
        public async Task<ClientDTO?> Login(ClientLoginDTO dto)
        {
            string selectCommandText =
                            @"SELECT * 
                                FROM Users 
                                WHERE Email = @Email AND PasswordHash = @HashPassword";
            using (SqlConnection conn = new SqlConnection(_connString))
            {

                using (SqlDataAdapter adapter = new SqlDataAdapter(selectCommandText, conn))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@Email", dto.Email);
                    adapter.SelectCommand.Parameters.AddWithValue(
                    "@HashPassword", PasswordHelper.ComputeSHA512Hash(dto.Password)
                        );
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    if (table.Rows.Count == 1)
                        return new ClientDTO()
                        {
                            Id = Convert.ToInt32(table.Rows[0]["Id"].ToString()),
                            FirstName = table.Rows[0]["FirstName"]?.ToString() ?? string.Empty,
                            LastName = table.Rows[0]["LastName"]?.ToString() ?? string.Empty,
                            Email = table.Rows[0]["Email"]?.ToString() ?? string.Empty,
                        };
                    return null;
                }
            }
        }
        public async Task<bool> CheckExistsClient(string email)
        {
            if (await GetUserByEmail(email) == null)
            {
                return false;
            }
            return true;
        }
        public async Task<string?> RequestOtp(string email)
        {

            using (SqlConnection conn = new SqlConnection(_connString))
            {

                using (SqlCommand command = new SqlCommand("RequestOtp", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", email);

                    await conn.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            if (!string.IsNullOrEmpty(reader["OTP"].ToString()))
                            {
                                return reader["OTP"].ToString();
                            }
                        }
                    }
                }

                return null;
            }
        }
        private async Task IncrementAttempt(int otpId)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {

                using (SqlCommand cmd = new SqlCommand("UPDATE OTPs SET Attempt = Attempt + 1 WHERE Id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", otpId);
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        private async Task DeactivateOtp(int otpId)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE OTPs SET IsActive = 0 WHERE Id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", otpId);
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        private async Task<int> GetOtpId(string Email)
        {
            const string selectQuery = @"
               SELECT TOP 1 o.Id
               FROM OTPs o
               INNER JOIN Users u ON o.UserId = u.Id
               WHERE u.Email = @Email
               ORDER BY o.CreatedAt DESC";

            int otpId;

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                using (SqlCommand cmd = new SqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", Email);
                    await conn.OpenAsync();


                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync())
                            throw new Exception("OTP not found. Please request a new one.");

                        otpId = reader.GetInt32(reader.GetOrdinal("Id"));
                    }

                    return otpId;
                }
            }
        }
        public async Task<bool> VerifyOtp(VerifyOtpDTO dto)
        {

            const string selectQuery = @"
               SELECT TOP 1 o.Id, o.OTPCode, o.IsActive, o.ExpiryDate, o.Attempt, o.CreatedAt 
               FROM OTPs o
               INNER JOIN Users u ON o.UserId = u.Id
               WHERE u.Email = @Email
               ORDER BY o.CreatedAt DESC";

            int otpId;
            string otpCode;
            bool isActive;
            DateTime expiryDate;
            int attempts;

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                using (SqlCommand cmd = new SqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", dto.Email);
                    await conn.OpenAsync();


                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync())
                            throw new Exception("OTP not found. Please request a new one.");

                        otpId = reader.GetInt32(reader.GetOrdinal("Id"));
                        otpCode = reader.GetString(reader.GetOrdinal("OTPCode"));
                        isActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                        expiryDate = reader.GetDateTime(reader.GetOrdinal("ExpiryDate"));
                        attempts = reader.GetInt32(reader.GetOrdinal("Attempt"));
                    }

                    if (!isActive)
                        throw new Exception("OTP is no longer valid.");

                    if (expiryDate < DateTime.Now || attempts == 3)
                    {
                        await DeactivateOtp(otpId);
                        throw new Exception("OTP has expired. Please request a new one.");
                    }
                    if (otpCode != dto.Otp)
                    {
                        await IncrementAttempt(otpId);
                        throw new Exception("Incorrect OTP.");
                    }

                    return true;
                }
            }
        }
        public async Task<bool> ResetPassword(ResetPasswordDTO dto)
        {
            if (!await VerifyOtp(new VerifyOtpDTO()
            {
                Email = dto.Email,
                Otp = dto.Otp
            }))
            {
                throw new UnauthorizedAccessException("Otp is incorrect.");
            }
            string query = "update users set PasswordHash=@PasswordHash where Email=@Email";

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@Email", dto.Email);
                    cmd.Parameters.AddWithValue("@PasswordHash", PasswordHelper.ComputeSHA512Hash(dto.ConfirmPassword));
                    conn.Open();
                    await cmd.ExecuteNonQueryAsync();
                }
            }

            await DeactivateOtp(await GetOtpId(dto.Email));
            return true;
        }
        public async Task<ClientDTO?> Signup(ClientSignUpDTO dto)
        {

            using (SqlConnection conn = new SqlConnection(_connString))
            {

                using (SqlCommand command = new SqlCommand("ClientSignUp", conn))
                {
                    //string firstName = dto.FullName.Split(' ')[0];
                    //string lastName = dto.FullName.Split(' ').Length > 1 ? dto.FullName.Split(' ')[1] : string.Empty;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FirstName", dto.FirstName);
                    command.Parameters.AddWithValue("@LastName", dto.LastName);
                    command.Parameters.AddWithValue("@Email", dto.Email);
                    command.Parameters.AddWithValue("@UserNameHash", PasswordHelper.ComputeSHA512Hash(dto.Username));
                    command.Parameters.AddWithValue("@BirthDate", dto.BirthDate);
                    command.Parameters.AddWithValue("@PhoneNumber", dto.Phone);
                    command.Parameters.AddWithValue("@PasswordHash", PasswordHelper.ComputeSHA512Hash(dto.Password));

                    await conn.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            int newUserId;
                            if (int.TryParse(reader["NewUserID"].ToString(), out newUserId))
                            {
                                if (newUserId > 0)
                                {
                                    return new ClientDTO()
                                    {
                                        Id = newUserId,
                                        FirstName = dto.FirstName,
                                        LastName = dto.LastName,
                                        Email = dto.Email,
                                        BirthDate = dto.BirthDate,
                                        Phone = dto.Phone
                                    };
                                }
                            }
                        }
                    }
                }

                return null;
            }

        }

        public Task<bool> IsLogin(int userId)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
               
                conn.Open();
                string query = "SELECT COUNT(*) FROM Users WHERE Id = @UserId AND IsLogging = 1";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@UserId", userId);   
                    int result = (int)command.ExecuteScalar();
                    return Task.FromResult(result > 0);
                }
            }
        }

    }
}