using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MyWebApi.Core.Dto;
using MyWebApi.Core.Model;
using MyWebApi.Infrastructure.Data;
using MyWebApi.Interfaces;
using RazorLight;

namespace MyWebApi.Infrastructure.Repository___service
{
    public class EmailService : IEmailService
    {
        private readonly ApplicationDbContext _context;
        private readonly SmtpSettings _smtpSettings;
        private readonly RazorLightEngine _razorEngine;

        public EmailService(ApplicationDbContext context, IOptions<SmtpSettings> smtpSettings)
        {
            _context = context;
            _smtpSettings = smtpSettings.Value;

            //_razorEngine = new RazorLightEngineBuilder()
            // ////.UseFileSystemProject(templatePath)
            // //.UseMemoryCaching()  // Enables caching
            // .Build();

        }

        public async Task<EmailResponse> GetUserDetailsByIDAsync(int userId)
        {
            var response = new EmailResponse();

            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                    return new EmailResponse { IsSuccess = false, Message = "User not found." };

                if (string.IsNullOrWhiteSpace(user.Email))
                    return new EmailResponse { IsSuccess = false, Message = "User does not have a valid email." };
                var engine = new RazorLight.RazorLightEngineBuilder()
                 .UseFileSystemProject(Path.Combine(Directory.GetCurrentDirectory(), "View"))
                 .UseMemoryCachingProvider()
                 .Build();

                var emailBody = await engine.CompileRenderAsync("Email.cshtml", user); // Pass UserEmail model


                // ✅ Create Email
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Assimilate", _smtpSettings.SenderEmail));
                message.To.Add(new MailboxAddress(user.UserName, user.Email));
                message.Subject = "Welcome to Assimilate!";
                message.Body = new TextPart("html") { Text = emailBody };

                // ✅ Send Email
                using var client = new SmtpClient();
                await client.ConnectAsync(_smtpSettings.SmtpServer, _smtpSettings.SmtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_smtpSettings.SenderEmail, _smtpSettings.SenderPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                response.IsSuccess = true;
                response.Message = "Email sent successfully.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Failed to send email. Error: {ex.Message}";
            }

            return response;
        }
    }
}










































