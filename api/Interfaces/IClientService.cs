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
        public Task<Client> Signup(ClientSignUpDTO dto);
        public Task<Client?> Login(ClientLoginDTO dto);
        public Task<string?> RequestOtp(RequestOtpDto dto);
        public Task<bool> VerifyOtp(VerifyOtpDto dto);
        public Task<bool> ResetPassword(ResetPasswordDTO dto);
    }
}