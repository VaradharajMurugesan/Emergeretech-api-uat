using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EmergereJobCareerWebApi.Models
{
    public class JobDetail
    {
        [JsonPropertyName("jobId")]
        [BindProperty(Name = "jobId")]
        public int job_id { get; set; }
        [JsonPropertyName("jobTitle")]
        [BindProperty(Name = "jobTitle")]
        public string JobTitle { get; set; }
        [JsonPropertyName("jobDescription")]
        [BindProperty(Name = "jobDescription")]
        public string JobDescription { get; set; }
        [JsonPropertyName("noOfVacancies")]
        [BindProperty(Name = "noOfVacancies")]
        public string NoOfVacancies { get; set; }
        [JsonPropertyName("expectedExperience")]
        [BindProperty(Name = "expectedExperience")]
        public string ExpectedExperience { get; set; }
        [JsonPropertyName("jobLocation")]
        [BindProperty(Name = "jobLocation")]
        public string JobLocation { get; set; }
        [JsonPropertyName("postedOn")]
        [BindProperty(Name = "postedOn")]
        public string PostedOn { get; set; }
        [JsonPropertyName("isActive")]
        [BindProperty(Name = "isActive")]
        public bool isActive { get; set; }
        [JsonPropertyName("shortJobDescription")]
        [BindProperty(Name = "shortJobDescription")]
        public string shortJobDescription { get; set; }

    }
}
