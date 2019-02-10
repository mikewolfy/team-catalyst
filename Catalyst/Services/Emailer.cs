using Catalyst.Models;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalyst.Services
{
    public class EmailService: IEmailer
    {
        private IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task EmailJoke(JokeToEmail joke, List<ToEmailAddress> toEmails)
        {
            var client = new SendGridClient(_config["SendGridKey"]);
            var msg = new SendGridMessage();

            msg.SetFrom(new EmailAddress(_config["ReturnEmail"], "Juan\'s Jokes"));

            var emails = new List<EmailAddress>();

            toEmails.ForEach(r =>
            {
                emails.Add(new EmailAddress(r.Email, r.Name));
            });

            msg.AddTos(emails);

            msg.SetSubject("Juan has a new joke for you");

            var builder = new StringBuilder();
            builder.Append($"<h1>{joke.Text}</h1><br/><br/>{joke.Punchline}");
            builder.Append("<br/><br/>");
            var siteUrl = _config["SiteUrl"];
            builder.Append($"<p><small>For more jokes visit the <a href=\"{siteUrl}\">Jokes Site</a>.</small></p>");
            
            //msg.AddContent(MimeType.Text, message);
            msg.AddContent(MimeType.Html, builder.ToString());

            await client.SendEmailAsync(msg);
        }
    }

    public class ToEmailAddress
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }

    public class JokeToEmail
    {
        public string Text { get; set; }
        public string Punchline { get; set; }
    }
}
