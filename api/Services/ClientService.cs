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
    public class ClientService : IClientService
    {
        private readonly string _connString;
        public ClientService(IConfiguration config)
        {
            _connString = config.GetConnectionString("DefaultConnection");
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

        public async Task<Client?> Login(ClientLoginDTO dto)
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
                        return new Client()
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




        public async Task<bool> VerifyOtp(VerifyOtpDTO dto)
        {

            //const string selectQuery = @"
            //    SELECT TOP 1 o.Id, o.OTPCode, o.IsActive, o.ExpiryDate, o.Attempt, o.CreatedAt 
            //    FROM OTPs o
            //    INNER JOIN Users u ON o.UserId = u.Id
            //    WHERE u.Email = @Email
            //    ORDER BY o.CreatedAt DESC";
            const string selectQuery = @"SELECT TOP 1 o.Id, o.OTPCode, o.IsActive, o.ExpiryDate, o.Attempt, o.CreatedAt
                                        FROM OTPs o 
                                        WHERE o.UserId = @UserId
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
                    cmd.Parameters.AddWithValue("@UserId", dto.UserId);
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

                    await DeactivateOtp(otpId);
                    return true;
                }
            }
        }
        public async Task<bool> ResetPassword(ResetPasswordDTO dto)
        {
            string query = "update users set PasswordHash=@pass where id=@id";

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", dto.UserId);
                    cmd.Parameters.AddWithValue("@pass", dto.ConfirmPassword);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }

        public async Task<Client?> Signup(ClientSignUpDTO dto)
        {

            using (SqlConnection conn = new SqlConnection(_connString))
            {

                using (SqlCommand command = new SqlCommand("ClientSignUp", conn))
                {
                    string firstName = dto.FullName.Split(' ')[0];
                    string lastName = dto.FullName.Split(' ').Length > 1 ? dto.FullName.Split(' ')[1] : string.Empty;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@Email", dto.Email);
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
                                    return new Client()
                                    {
                                        Id = newUserId,
                                        FirstName = firstName,
                                        LastName = lastName,
                                        Email = dto.Email,
                                        BirthDate = dto.BirthDate,
                                        Phone = dto.Phone,
                                    };
                                }
                            }
                        }
                    }
                }

                return null;
            }

        }


    }
}