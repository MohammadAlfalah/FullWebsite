using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Domain.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Here you can log the email details, simulate sending an email, or just do nothing
            System.Diagnostics.Debug.WriteLine($"Email sent to {email} with subject {subject}");
            return Task.CompletedTask;
        }
    }
}
