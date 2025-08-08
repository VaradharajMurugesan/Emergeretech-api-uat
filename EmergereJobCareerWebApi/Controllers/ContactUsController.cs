using System.Net;
using Azure.Core;
using EmergereJobCareerWebApi.Models;
using EmergereJobCareerWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace EmergereJobCareerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IContactUsService _contactService;
        private readonly ILoggerManager _logger;

        private string _connectingString = "";
        private string _container = "uploadresume";
        private readonly IDBService _dbService;

        public ContactUsController(IContactUsService contactService, ILoggerManager logger, IDBService dbService)
        {
            _contactService = contactService;
            _logger = logger;
            _dbService = dbService;

        }

        [HttpGet]
        [Route("GetAllContacts")]
        public async Task<IActionResult> Get()
        {
            try
            {
                _logger.LogInfo("Retrieving all the Contact details.");
                var result = await _contactService.GetContacts();
                _logger.LogInfo("Successfully retrieved all the Contact details");
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured: " + ex.Message);
                return BadRequest("Error occured in GetAllContacts");
            }

        }

        [HttpPost]
        [Route("AddContact")]
        public async Task<IActionResult> InsertContact([FromBody] ContactUs contact)
        {
            try
            {
                _logger.LogInfo("Adding the Contact details");
                var result = await _contactService.InsertContact(contact);
                if (result == true)
                {
                    _logger.LogInfo("Successfully added the Contact details");
                    _logger.LogInfo("Sending email to admin team with contact details");
                    var emailResult = _contactService.SendEmail(contact);
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Contact Email already present");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured: " + ex.Message);
                return BadRequest("Error occured in AddContact");
            }
        }

    }
}
