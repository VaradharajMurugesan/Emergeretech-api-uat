using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;


namespace EmergereJobCareerWebApi.Models
{
    public class ResumeDetail
    {
        [JsonPropertyName("resumeId")]
        [BindProperty(Name = "resumeId")]
        public int resume_id { get; set; }
        [JsonPropertyName("candidateName")]
        [BindProperty(Name = "candidateName")]
        public string? candidate_name { get; set; }
        [JsonPropertyName("dateOfBirth")]
        [BindProperty(Name = "dateOfBirth")]
        public string? DOB { get; set; }

        //[IgnoreDataMember]
        //public string resume_link { get; set; }

        [JsonPropertyName("fileToUpload")]
        [BindProperty(Name = "fileToUpload")]
        public IFormFile FileToUpload { get; set; }
        [JsonPropertyName("jobId")]
        [BindProperty(Name = "jobId")]
        public int job_id { get; set; }
        [JsonPropertyName("joiningDate")]
        [BindProperty(Name = "joiningDate")]
        public string joining_date { get; set; }
        [JsonPropertyName("aboutCandidate")]
        [BindProperty(Name = "aboutCandidate")]
        public string about_candidate { get; set; }
        [JsonPropertyName("gender")]
        [BindProperty(Name = "gender")]
        public string gender { get; set; }

        [JsonPropertyName("candidateEmail")]
        [BindProperty(Name = "candidateEmail")]
        public string candidate_email { get; set; }
        

    }
}
