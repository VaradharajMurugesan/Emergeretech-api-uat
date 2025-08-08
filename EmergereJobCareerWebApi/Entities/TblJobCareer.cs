using System;
using System.Collections.Generic;

namespace EmergereJobCareerWebApi.Entities
{
    public partial class TblJobCareer
    {
        public string? JobTitle { get; set; }
        public string? JobDescription { get; set; }
        public string? NoOfVacancies { get; set; }
        public string? ExpectedExperience { get; set; }
        public string? JobLocation { get; set; }
        public string? PostedOn { get; set; }
    }
}
