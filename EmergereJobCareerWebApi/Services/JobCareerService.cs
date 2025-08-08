using EmergereJobCareerWebApi.Models;

namespace EmergereJobCareerWebApi.Services
{
    public class JobCareerService : IJobCareerService
    {
        private readonly IDBService _dbService;

        public JobCareerService(IDBService dbService)
        {
            _dbService = dbService;
        }

        public async Task<bool> CreateJob(JobDetail job)
        {
            var result =
                 await _dbService.EditData(
                     "INSERT INTO tbl_Job_career (JobTitle,JobDescription,NoOfVacancies,ExpectedExperience, JobLocation,PostedOn, is_active, shortJobDescription) VALUES (@JobTitle,@JobDescription,@NoOfVacancies,@ExpectedExperience, @JobLocation,@PostedOn, @isActive, @shortJobDescription)",
                     job);

            return true;
        }

        public async Task<JobResult> GetJobList(Paging paging)
        {
            try
            {
                string search_key = string.Empty;
                string is_active = string.Empty;
                JobResult jobresults = new JobResult();
                search_key = string.IsNullOrEmpty(@paging.search.search_key.Trim()) ? "' '" : @paging.search.search_key;
                is_active = string.IsNullOrEmpty(@paging.isActive.Trim()) ? "' '" : @paging.isActive;
                int data_offset = (paging.page_number - 1) * paging.no_of_records;
                string query = string.Empty;
                string querycount = string.Empty;
                /*
                if (string.IsNullOrEmpty(search_key))
                {
                    querycount = "SELECT count(1) FROM tbl_Job_career WHERE 1=1";
                    query = "SELECT * FROM tbl_Job_career WHERE 1=1 ORDER BY " + @paging.sort_by_column + " " + @paging.sort_by + " LIMIT " + @data_offset + "," + @paging.no_of_records;

                }
                else
                {
                    querycount = "SELECT count(1) FROM tbl_Job_career WHERE JobTitle LIKE '%" + search_key + "%' OR JobDescription LIKE '%" + search_key + "%'";
                    query = "SELECT * FROM tbl_Job_career WHERE JobTitle LIKE '%" + search_key + "%' OR JobDescription LIKE '%" + search_key + "%' ORDER BY " + @paging.sort_by_column + " " + @paging.sort_by + " LIMIT " + @data_offset + "," + @paging.no_of_records;

                }
                */
                querycount = "SELECT COUNT(1) FROM tbl_Job_career WHERE CASE WHEN nullif( '"+ is_active + "', '') is null THEN 1=1 ELSE is_active = "+ is_active + " END AND CASE WHEN nullif( '"+ search_key + "', '') is null THEN 1=1 ELSE JobTitle LIKE '%" + search_key + "%' OR JobDescription LIKE '%" + search_key + "%' END";
                //query = "SELECT * FROM tbl_Job_career WHERE CASE WHEN nullif( '"+ is_active + "', '') is null THEN 1=1 ELSE is_active = "+ is_active + " END AND CASE WHEN nullif('" + search_key + "', '') is null THEN 1=1 ELSE JobTitle LIKE '%" + search_key + "%' OR JobDescription LIKE '%" + search_key + "%' END ORDER BY " + @paging.sort_by_column + " " + @paging.sort_by + " LIMIT " + @data_offset + "," + @paging.no_of_records;
                query = "SELECT * FROM tbl_Job_career WHERE CASE WHEN nullif( '" + is_active + "', '') is null THEN 1=1 ELSE is_active = " + is_active + " END AND CASE WHEN nullif('" + search_key + "', '') is null THEN 1=1 ELSE JobTitle LIKE '%" + search_key + "%' OR JobDescription LIKE '%" + search_key + "%' END ORDER BY updated_date DESC LIMIT " + @data_offset + "," + @paging.no_of_records;
                var count = await _dbService.GetAsync<int>(querycount, new { });
                var JobList = await _dbService.GetAll<JobDetail>(query, new { });
                jobresults.jobDetails = JobList;
                jobresults.totalCount = count;
                return jobresults;


            }
            catch (Exception ex)
            {
                string error_msg = ex.Message;
                return null;
            }
        }


        public async Task<List<JobDetail>> getJobDetail_by_id(int job_id)
        {
            var JobList = await _dbService.GetAll<JobDetail>("SELECT * FROM tbl_Job_career WHERE job_id=@job_id", new { job_id });
            return JobList;
        }

        public async Task<List<JobDetail>> getJobDetail_by_name(string jobname)
        {
            var JobList = await _dbService.GetAll<JobDetail>("SELECT * FROM tbl_Job_career WHERE JobTitle LIKE '%" + @jobname + "%'", new { jobname });
            return JobList;
        }

        public async Task<JobDetail> UpdateJob_by_id(JobDetail job)
        {
            var updateJob =
                await _dbService.EditData(
                    "UPDATE tbl_job_career SET `jobtitle` = @JobTitle, `JobDescription` = @JobDescription, `NoOfVacancies` = @NoOfVacancies, `ExpectedExperience` = @ExpectedExperience, `JobLocation` = @JobLocation, `PostedOn` = @PostedOn, `is_active` = @isActive, `shortJobDescription` = @shortJobDescription, `updated_date` = now() WHERE `job_id` = @job_id;",
                    job);
            return job;
        }

        public async Task<bool> DeleteJob(int job_id)
        {
            var deleteJob = await _dbService.EditData("UPDATE tbl_job_career SET `is_active` = 0 WHERE job_id=@job_id", new { job_id });
            return true;
        }

    }
}
