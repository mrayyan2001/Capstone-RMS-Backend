using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Client
{
    public class ResetPasswordDTO
    {
        //public string Email { get; set; } = string.Empty;
        //public string Otp { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}