using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeInPink.Utils
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";

        public void Send(String toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);

            var from = new EmailAddress("admin@beinpink.com", "BE IN PINK Administrator");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }

    }
}