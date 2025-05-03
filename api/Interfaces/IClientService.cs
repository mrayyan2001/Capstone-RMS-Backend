using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Client;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Interfaces
{
    public interface IClientService
    {
        public Task<ClientDTO?> Signup(ClientSignUpDTO dto);
        public Task<ClientDTO?> Login(ClientLoginDTO dto);
        public Task<string?> RequestOtp(string email);
        public Task<bool> VerifyOtp(VerifyOtpDTO dto);
        public Task<bool> ResetPassword(ResetPasswordDTO dto);
        public Task<bool> CheckExistsClient(string email);
        public Task<ClientInfoDTO> GetUserByEmail(string email);
    }
}