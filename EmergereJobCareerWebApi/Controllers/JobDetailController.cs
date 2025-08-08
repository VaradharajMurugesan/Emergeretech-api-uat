using Microsoft.AspNetCore.Mvc;
using EmergereJobCareerWebApi.Services;
using EmergereJobCareerWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace EmergereJobCareerWebApi.Controllers
{
    //[Authorize]
    [EnableCors("AllowOrigin")]
    [ApiController]
    [Route("api/[controller]")]
    public class JobDetailController : Controller
    {
        private readonly IJobCareerService _jobService;
        private readonly ILoggerManager _logger;

        public JobDetailController(IJobCareerService jobService, ILoggerManager logger)
        {
            _jobService = jobService;
            _logger = logger;
        }

        [HttpPost]
        [Route("GetAllJobs")]
        public async Task<IActionResult> Get([FromBody] Paging paging)
        {
            try
            {
                /*
               _logger.LogDebug("Here is debug message from the controller.");
               _logger.LogWarn("Here is warn message from the controller.");
               _logger.LogError("Here is error message from the controller.");
                */


                _logger.LogInfo("Retrieving all the Job details.");
                var result = await _jobService.GetJobList(paging);
                _logger.LogInfo("Successfully retrieved all the Job details");
                return Ok(result);

               
                
            }
            catch(Exception ex)
            {
                _logger.LogError("Exception occured: " + ex.Message);
                return BadRequest("Error occured in GetAllJobs");
            }

   
        }

        [HttpGet]
        [Route("GetJob_by_ID")]
        public async Task<IActionResult> getJobDetail_by_ID(int job_id)
        {
            try
            {
                _logger.LogInfo("Retrieving the Job details for the title "+ job_id);
                var result = await _jobService.getJobDetail_by_id(job_id);
                _logger.LogInfo("Successfully retrieved the Job details for the title " + job_id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured: " + ex.Message);
                return BadRequest("Error occured in GetJob_by_ID");
            }
        }

        [HttpGet]
        [Route("GetJob_by_Name")]
        public async Task<IActionResult> getJobDetail_by_name(string jobname)
        {
            try
            {
                _logger.LogInfo("Retrieving the Job details for the title " + jobname);
                var result = await _jobService.getJobDetail_by_name(jobname);
                _logger.LogInfo("Successfully retrieved the Job details for the title " + jobname);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured: " + ex.Message);
                return BadRequest("Error occured in GetJob_by_Name");
            }
        }

        [Authorize]
        [HttpPost]
        [Route("AddJob")]
        public async Task<IActionResult> AddJob([FromBody] JobDetail job)
        {
            try
            {
                _logger.LogInfo("Adding the Job details");
                var result = await _jobService.CreateJob(job);
                _logger.LogInfo("Successfully added the Job details");
                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError("Exception occured: " + ex.Message);
                return BadRequest("Error occured in AddJob");
            }
        }

        [HttpDelete]
        [Route("DeleteJob")]
        public async Task<IActionResult> DeleteJob(int job_id)
        {
            try
            {
                _logger.LogInfo("Deleting the Job details");
                var result = await _jobService.DeleteJob(job_id);
                _logger.LogInfo("Successfully deleted the Job details");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured: " + ex.Message);
                return BadRequest("Error occured in DeleteJob");
            }
        }

        //[Authorize]
        [HttpPost]
        [Route("Update_Job_by_ID")]
        public async Task<IActionResult> UpdateJob_by_id([FromBody] JobDetail job)
        {
            try
            {
                _logger.LogInfo("Updating the Job details");
                var result = await _jobService.UpdateJob_by_id(job);
                _logger.LogInfo("Successfully updated the Job details");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured: " + ex.Message);
                return BadRequest("Error occured in Update_Job_by_ID");
            }
        }

    }
}
