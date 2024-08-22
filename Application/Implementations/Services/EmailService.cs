using Application.Contracts.Services;
using Application.Models;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;
using Application.Data;


namespace Application.Implementations.Services
{
    public class EmailService : IEmailService
    {
        private EmailSettings _emailsettings { get; set; }
        private readonly IUnitOfWork _unitOfWork;
   
        public EmailService(IOptions<EmailSettings> emailsettings, IUnitOfWork unitOfWork)
        {
            _emailsettings = emailsettings.Value;
            _unitOfWork = unitOfWork;
        }
        public async Task BroadcastEmailAsync()
        {
            var client = new SendGridClient(_emailsettings.ApiKey);
            var to = new List<EmailAddress>();

            var pendingemail = await _unitOfWork.emailqueuerepository.GetAsync(q => q.Status == "PENDING");
            if (pendingemail != null)
            {
                foreach (var item in pendingemail.Recepiants)
                {
                    to.Add(new EmailAddress(item));
                }
                var from = new EmailAddress
                {
                    Email = _emailsettings.FromAddress,
                    Name = _emailsettings.FromName
                };
                try
                {
                    var message = MailHelper.CreateSingleEmailToMultipleRecipients(from, to, pendingemail.Subject, pendingemail.Body, pendingemail.Body);
                    var response = await client.SendEmailAsync(message);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Accepted)
                    {
                        _unitOfWork.emailqueuerepository.RemoveAsync(pendingemail);
                        var deleteresponse = await _unitOfWork.Save();
                       // _logger.LogInformation($"{pendingemail.Id} email successfully sent");

                    }
                }
                catch (Exception ex)
                {

                   // _logger.LogError(ex, ex.Message);
                }

            }

        }

        public async Task<bool> AddToQueue(EmailBroadcast emails)
        {
            Emailqueue email = new Emailqueue();
            email.Subject = emails.Subject;
            email.Body = emails.Body;
            email.HtmlBody = $"<h5>Good day</h5>"
                             + $"<p>{emails.Body} </p>";
            if (emails.Attachment != null)
            {
                email.HtmlBody += $"<a href='{emails.Attachment}' style='background-color: #04AA6D;  border: none;color: white;  padding: 15px 32px;text-align: center;text-decoration: none; display: inline-block;font-size: 16px;margin: 4px 2px;cursor: pointer;'>Click here to access attachment</a>";
            }



            email.Status = "PENDING";
            email.Recepiants = emails.emails;

            await _unitOfWork.emailqueuerepository.AddAsync(email);
            var response = await _unitOfWork.Save();
            return response > 0 ? true : false;
        }
    }
}
