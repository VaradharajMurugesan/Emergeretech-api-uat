using System.Net.Mail;
using System.Net;
using System.Net.Mail;
using EmergereJobCareerWebApi.Models;
using Microsoft.Extensions.Configuration;

namespace EmergereJobCareerWebApi.Services
{
    public class ContactUsService : IContactUsService
    {
        private readonly IDBService _dbService;
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        Program obj = new Program();
        
        public ContactUsService(IDBService dbService, IConfiguration configure, ILoggerManager logger)
        {
            _dbService = dbService;
            _configuration = configure;
            _logger = logger;
        }

        public async Task<bool> InsertContact(ContactUs contact)
        {
            var ContactList = await _dbService.GetAll<ContactUs>("SELECT * FROM tbl_contactus WHERE contact_email = ltrim(rtrim(@contact_email))", new {contact.contact_email });
            if (ContactList.Count == 0)
            {
                var result =
                    await _dbService.EditData(
                        "INSERT INTO tbl_contactus(contact_name, contact_email, contact_phone, contact_message) VALUES (@contact_name, @contact_email, @contact_phone, @contact_message)",
                        contact);

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<ContactUs>> GetContacts()
        {
            var ContactList = await _dbService.GetAll<ContactUs>("SELECT * FROM tbl_contactus", new { });
            return ContactList;
        }

        public async Task<bool> SendEmail(ContactUs objContact)
        {
            try
            {
                EncryptDecrypt objPassword = new EncryptDecrypt();

                var smtpClient = new SmtpClient(_configuration.GetValue<string>("Email:SMPTServer"))
                {
                    Port = _configuration.GetValue<int>("Email:Port"),
                    Credentials = new NetworkCredential(_configuration.GetValue<string>("Email:EmailUser"), objPassword.Decrypt(_configuration.GetValue<string>("Email:EmailPwd"))),
                    EnableSsl = _configuration.GetValue<bool>("Email:EnableSsl"),
                };

                //smtpClient.Send("email", "recipient", "subject", "body");


                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_configuration.GetValue<string>("Email:EmailFrom")),
                    Subject = _configuration.GetValue<string>("Email:ContactEmailSubject"),
                    Body = String.Format(_configuration.GetValue<string>("Email:ContactEmailBody"), objContact.contact_name, objContact.contact_email, objContact.contact_phone, objContact.contact_message),
                    IsBodyHtml = _configuration.GetValue<bool>("Email:IsBodyHtml"),
                };
                mailMessage.To.Add(_configuration.GetValue<string>("Email:EmailTo"));
                mailMessage.CC.Add(_configuration.GetValue<string>("Email:EmailCc"));
                smtpClient.Send(mailMessage);
                _logger.LogInfo("Successfully sent email to the corresponding recipient(s) on new contact");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured in SendEmail: " + ex.Message);
                return false;
            }
        }
    }
}
