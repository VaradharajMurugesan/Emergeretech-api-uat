using Microsoft.AspNetCore.Mvc;
using EmergereJobCareerWebApi.Services;
using EmergereJobCareerWebApi.Models;
using System.Reflection;
using Azure.Storage.Blobs;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using System.Net.Mail;
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmergereJobCareerWebApi.Controllers
{
    //[Authorize]
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeDetailController : ControllerBase
    {
        // GET: api/<ValuesController>
        private readonly IUploadResumeService _resumeService;
        private readonly ILoggerManager _logger;
        private string _connectingString;
        private string _container;
        private readonly IDBService _dbService;

        public ResumeDetailController(IUploadResumeService resumeService, ILoggerManager logger, IDBService dbService)
        {
            logger.LogInfo("ResumeDetailController initialized.");
            _resumeService = resumeService;
            _logger = logger;
            _dbService = dbService;
            
            _connectingString = Environment.GetEnvironmentVariable("BlobConnectionString");
            _container = "uploadresume";
        }
     
        [HttpPost("GetResumeList")]
        public async Task<IActionResult> Get([FromBody] Paging paging)
        {
            try
            {
                _logger.LogInfo("Retrieving all the Resumes uploaded.");
                var result = await _resumeService.GetResumeList(paging);
                _logger.LogInfo("Successfully retrieved all the resume details");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured in GetResumeList: " + ex.Message);
                return BadRequest("Error occured in GetResumeList");
            }
        }


        [HttpGet("GetResume")]
        public async Task<IActionResult> Get(int resume_id)
        {
            try
            {
                _logger.LogInfo("Retrieving the Resume for the job title: ." + resume_id);
                var result = await _resumeService.GetResume(resume_id);
                _logger.LogInfo("Successfully retrieved resume details for the job title: " + resume_id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured in GetResume: " + ex.Message);
                return BadRequest("Error occured in GetResume");
            }
        }

        // POST api/<ValuesController>
        [HttpPost("UploadResume")]
        public async Task<IActionResult> Post([FromForm] InsertResumeDetail model)
        {
            try
            {
                _logger.LogInfo("Invoked the Upload Resume API");
                //if (model == null || model.FileToUpload == null || model.FileToUpload.Length == 0)
                //    return Content("file not selected");
                //string jobTitle = await _dbService.GetAsync<string>("SELECT JobTitle FROM tbl_Job_career WHERE job_id="+model.job_id, new {});
                //var path = Path.Combine(Directory.GetCurrentDirectory(), "Resumes", model.FileToUpload.FileName);
                //string file_extension = System.IO.Path.GetExtension(model.FileToUpload.FileName);
                //string file_name = model.candidate_name + "_" + DateTime.UtcNow.ToString("yyyyMMddHHmmssfff",
                //                            CultureInfo.InvariantCulture)+ file_extension;
                //using (var stream = new FileStream(path, FileMode.Create))
                //{

                //    await model.FileToUpload.CopyToAsync(stream);
                //}

                //BlobServiceClient blobServiceClient = new BlobServiceClient(_connectingString);
                //BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(_container);
                //BlobClient blobClient = blobContainerClient.GetBlobClient(file_name);
                //await blobClient.UploadAsync(path, true);
                //var blobUrl = blobClient.Uri.AbsoluteUri;

                //var result = _resumeService.InsertResume(model, blobUrl);

                //var emailResult = _resumeService.SendEmail(model, blobUrl, jobTitle);

                return Ok("File uploaded successfully.......");

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured: " + ex.Message);
                return BadRequest(ex.StackTrace+"Error occured in UploadResume");
            }

        }


        // DELETE api/<ValuesController>/5
        [HttpDelete("DeleteResume")]
        public async Task<IActionResult> Delete(int resume_id)
        {
            try
            {
                _logger.LogInfo("Deleting the Resume for the job title: " + resume_id);
                var result = await _resumeService.DeleteResume(resume_id);
                _logger.LogInfo("Successfully deleted resume details for the job title: " + resume_id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured: " + ex.Message);
                return BadRequest("Error occured in DeleteResume");
            }
        }
    }
}
