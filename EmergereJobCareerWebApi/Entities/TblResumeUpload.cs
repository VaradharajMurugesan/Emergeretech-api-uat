using System;
using System.Collections.Generic;

namespace EmergereJobCareerWebApi.Entities
{
    public partial class TblResumeUpload
    {
        public string? JobTitle { get; set; }
        public string? CandidateName { get; set; }
        public DateOnly? Dob { get; set; }
        public string? ResumeLink { get; set; }
    }
}
