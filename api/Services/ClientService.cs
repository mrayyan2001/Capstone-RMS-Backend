using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Client;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace api.Services
{
    public class ClientService : IClientService
    {
        private readonly SqlConnection _conn;
        public ClientService(IConfiguration config)
        {
            _conn = new SqlConnection(config.GetConnectionString("DefaultConnection"));
        }

        public async Task<Client?> Login(ClientLoginDTO dto)
        {
            string selectCommandText =
                            @"SELECT * 
                                FROM Users 
                                WHERE Email = @Email AND PasswordHash = @HashPassword";
            using (SqlDataAdapter adapter = new SqlDataAdapter(selectCommandText, _conn))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@Email", dto.Email);
                adapter.SelectCommand.Parameters.AddWithValue(
                    "@HashPassword", PasswordHelper.ComputeSHA512Hash(dto.Password)
                    );
                DataTable table = new DataTable();
                adapter.Fill(table);

                if (table.Rows.Count > 0)
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
        public async Task<string?> RequestOtp(RequestOtpDto dto)
        {
            using (SqlCommand command = new SqlCommand("RequestOtp", _conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Email", dto.Email);

                await _conn.OpenAsync();

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

        public async Task<bool> ResetPassword(ResetPasswordDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<Client> Signup(ClientSignUpDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> VerifyOtp(VerifyOtpDto dto)
        {
            throw new NotImplementedException();
        }
    }
}