using EmergereJobCareerWebApi.Models;

namespace EmergereJobCareerWebApi.Services
{
    public interface IJobCareerService
    {
        Task<bool> CreateJob(JobDetail job);
        Task<JobResult> GetJobList(Paging paging);
        Task<List<JobDetail>> getJobDetail_by_id(int job_id);
        Task<List<JobDetail>> getJobDetail_by_name(string jobname);
        Task<JobDetail> UpdateJob_by_id(JobDetail job);
        Task<bool> DeleteJob(int job_id);
    }
}
