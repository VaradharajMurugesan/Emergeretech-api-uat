using EmergereJobCareerWebApi.Models;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.Net.NetworkInformation;

namespace EmergereJobCareerWebApi.Services
{
    
    public class UploadResumeService : IUploadResumeService
    {
        private readonly IConfiguration _configuration;
        private readonly IDBService _dbService;
        private readonly ILoggerManager _logger;
        public UploadResumeService(IDBService dbService, IConfiguration configure, ILoggerManager logger)
        {
            _dbService = dbService;
            _configuration = configure;
            _logger = logger;
        }

        public async Task<bool> DeleteResume(int resume_id)
        {
            try
            {
                var deleteResume = await _dbService.EditData("DELETE FROM tbl_resume_upload WHERE resume_id=@resume_id", new { resume_id });
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError("Exception occured: " + ex.Message);
                throw;
            }

        }

        public async Task<List<GetResumeDetail>> GetResume(int resume_id)
        {
            try
            {
                var resume = await _dbService.GetAll<GetResumeDetail>("SELECT * FROM tbl_resume_upload where resume_id=@resume_id", new { resume_id });
                return resume;
            }
            catch(Exception ex)
            {
                _logger.LogError("Exception occured: " + ex.Message);
                throw;
            }

        }

        public async Task<ResumeResult> GetResumeList(Paging paging)
        {
            try
            {
                string search_key = string.Empty;
                string is_active = string.Empty;
                ResumeResult resumeresults = new ResumeResult();
                search_key = string.IsNullOrEmpty(@paging.search.search_key.Trim()) ? "' '" : @paging.search.search_key;
                is_active = string.IsNullOrEmpty(@paging.isActive.Trim()) ? "' '" : @paging.isActive;
                int data_offset = (paging.page_number - 1) * paging.no_of_records;
                string query = string.Empty;
                string querycount = string.Empty;
                var resumeList = await _dbService.GetAll<GetResumeDetail>("SELECT * FROM tbl_resume_upload", new { });
                querycount = "SELECT COUNT(1) FROM tbl_resume_upload WHERE CASE WHEN nullif( '" + search_key + "', '') is null THEN 1=1 ELSE candidate_name LIKE '%" + search_key + "%' OR candidate_email LIKE '%" + search_key + "%' END";
                query = "SELECT * FROM tbl_resume_upload WHERE CASE WHEN nullif('" + search_key + "', '') is null THEN 1=1 ELSE candidate_name LIKE '%" + search_key + "%' OR candidate_email LIKE '%" + search_key + "%' END ORDER BY " + @paging.sort_by_column + " " + @paging.sort_by + " LIMIT " + @data_offset + "," + @paging.no_of_records;
                var count = await _dbService.GetAsync<int>(querycount, new { });
                var ResumeList = await _dbService.GetAll<ResumeDetail>(query, new { });
                resumeresults.resumeDetails = ResumeList;
                resumeresults.totalCount = count;
                return resumeresults;
            }
            catch(Exception ex)
            {
                _logger.LogError("Exception occured: " + ex.Message);
                throw;
            }
        }

        public async Task<bool> InsertResume(InsertResumeDetail resume, string path)
        {
            try
            {

                var query = "INSERT INTO tbl_resume_upload (candidate_name,DOB,resume_link, job_id, joining_date, about_candidate, gender, candidate_email) VALUES (@candidate_name,@DOB,'" + path + "', @job_id, @joining_date, @about_candidate, @gender, @candidate_email)";
                var result =
                    await _dbService.EditData(
                        query,
                        resume);

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError("Exception occured: " + ex.Message);
                throw;
            }

        }
        public async Task<bool> SendEmail(InsertResumeDetail objResume, string blobURL, string jobTitle)
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
                    Subject = _configuration.GetValue<string>("Email:EmailSubject") + jobTitle,
                    Body = String.Format(_configuration.GetValue<string>("Email:EmailBody"), objResume.candidate_name, jobTitle, blobURL),
                    IsBodyHtml = _configuration.GetValue<bool>("Email:IsBodyHtml"),
                };
                mailMessage.To.Add(_configuration.GetValue<string>("Email:EmailTo"));
                mailMessage.CC.Add(_configuration.GetValue<string>("Email:EmailCc"));
                smtpClient.Send(mailMessage);
                _logger.LogInfo("Successfully sent email to the corresponding recipient(s) on resume upload");
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError("Exception occured in SendEmail: " + ex.Message);
                return false;
            }
        }
    }
}
