using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using api.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace api.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var from = _config["EmailSettings:From"];
            var appPassword = _config["EmailSettings:AppPassword"];
            var host = _config["EmailSettings:Host"]; // Sets the SMTP server address for Gmail.
            int port = Convert.ToInt32(_config["EmailSettings:Port"]); // Sets the SMTP port.
            // Port 465 requires SSL stream, but SmtpClient in .NET doesn’t handle implicit SSL (port 465) properly — it expects to start with plain and then upgrade to TLS (using port 587).
            //  25 for unencrypted
            // 587 for TLS
            // 465 for SSL

            using var client = new SmtpClient(host, port)
            {
                EnableSsl = true, // EnableSsl = true: enables SSL encryption.
                Credentials = new NetworkCredential(from, appPassword)
            };

            await client.SendMailAsync(
                new MailMessage(
                    from: from,
                    to: to,
                    subject: subject,
                    body: body
                )
            );
        }
    }
}