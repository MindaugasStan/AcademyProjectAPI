using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using MouseTagProject.Context;
using MouseTagProject.Interfaces;
using MouseTagProject.Models;
using System;


namespace ToDoListProject.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly MouseTagProjectContext _context;

        public EmailService(IConfiguration config, MouseTagProjectContext context)
        {
            _config = config;
            _context = context;
        }

        public bool SendEmail(string letterHTML)
        {
            string server = _config["EmailConfigServer:Server"];
            int port = Convert.ToInt32(_config["EmailConfigServer:Port"]);
            string name = _config["EmailConfigServer:Name"];
            string email = _config["EmailConfigServer:Email"];
            string password = _config["EmailConfigServer:Password"];

            string subjectTo = _config["EmailConfigTo:Subject"];
            string emailTo = _config["EmailConfigTo:Email"];

            MimeMessage mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(name, email));
            mimeMessage.To.Add(new MailboxAddress(subjectTo, emailTo));
            mimeMessage.Subject = subjectTo;
            mimeMessage.Body = new TextPart(TextFormat.Html) { Text = letterHTML };

            using (var smtp = new SmtpClient())
            {
                try
                {
                    smtp.Connect(server, port, SecureSocketOptions.StartTls);
                    smtp.Authenticate(email, password);
                    smtp.Send(mimeMessage);
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    smtp.Disconnect(true);
                }
            }
            return true;
        }

        public string GenerateLetter(List<Candidate> candidates)
        {
            string table = File.ReadAllText("Templates/EmailTemplate.html");
            string line = "";
            foreach (var candidate in candidates)
            {
                string trBlank = File.ReadAllText("Templates/TrElement.html");
                string tr = trBlank.Replace("{1}", candidate.Name).Replace("{2}", candidate.Surname).Replace("{3}", candidate.Comment).Replace("{4}", candidate.Linkedin).Replace("{5}", candidate.WillBeContacted.ToString());

                line += tr;
            }
            return table.Replace("{1}", line);
        }
    }
}

