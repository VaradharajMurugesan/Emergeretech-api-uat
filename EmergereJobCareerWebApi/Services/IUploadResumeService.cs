using EmergereJobCareerWebApi.Models;

namespace EmergereJobCareerWebApi.Services
{
    public interface IUploadResumeService
    {
        Task<bool> InsertResume(InsertResumeDetail resume, string path);
        Task<ResumeResult> GetResumeList(Paging paging);
        Task<List<GetResumeDetail>> GetResume(int resume_id);
        Task<bool> DeleteResume(int resume_id);
        Task<bool> SendEmail(InsertResumeDetail objResume, string blobURL, string jobTitle);
    }
}
