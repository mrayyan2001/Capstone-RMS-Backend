using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Client
{
    public class ClientLoginDTO:IClientDTO
    {
        //[Required]
        public  string Email { get; set; } = string.Empty;
        //[Required]
        public string Password { get; set; } = string.Empty;

    }
}