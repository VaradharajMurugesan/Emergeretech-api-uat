using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace EmergereJobCareerWebApi.Models
{
    public class ContactUs
    {
        [JsonPropertyName("contactName")]
        [BindProperty(Name = "contactName")]
        public string contact_name { get; set; }
        [JsonPropertyName("contactEmail")]
        [BindProperty(Name = "contactEmail")]
        public string contact_email { get; set; }
        [JsonPropertyName("contactPhone")]
        [BindProperty(Name = "contactPhone")]
        public string contact_phone { get; set; }
        [JsonPropertyName("contactMessage")]
        [BindProperty(Name = "contactMessage")]
        public string contact_message { get; set; }
    }
}
