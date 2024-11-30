using Microsoft.Extensions.Configuration;
using MimeKit;
using ShopTARgv23.Core.Dto;
using ShopTARgv23.Core.ServiceInterface;
using MailKit.Net.Smtp;


namespace ShopTARgv23.ApplicationServices.Services
{
    public class EmailsServices : IEmailsServices
    {
        private readonly IConfiguration _config;

        public EmailsServices
            (
                IConfiguration config
            )
        {
            _config = config;
        }

        public void SendEmail(EmailDto dto)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUserName").Value));
            email.To.Add(MailboxAddress.Parse(dto.To));
            email.Subject = dto.Subject;
            //email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            //{
            //    Text = dto.Body
            //};
            var builder = new BodyBuilder
            {
                HtmlBody = dto.Body
            };

            foreach (var file in dto.Attachment)
            {
                if (file.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        file.CopyTo(stream);
                        stream.Position = 0;
                        builder.Attachments.Add(file.FileName, stream.ToArray());
                    }
                }
            }
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            smtp.Connect(_config.GetSection("EmailHost").Value, 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUserName").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
