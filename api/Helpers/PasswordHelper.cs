using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace api.Helpers
{
    public static class PasswordHelper
    {
        public static string ComputeSHA512Hash(string input)
        {
            // Algorithm
            // Convert to byte using Encoding.UTF8.GetBytes
            // Create instance of SHA512
            // call ComputeHash for the instance and pass the bytes
            // convert the hasBytes back into string (use string builder and foreach)
            // return the string builder .ToString()

            byte[] bytes = Encoding.UTF8.GetBytes(input);

            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] hashBytes = sha512.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}