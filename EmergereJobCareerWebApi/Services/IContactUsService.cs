using EmergereJobCareerWebApi.Models;

namespace EmergereJobCareerWebApi.Services
{
    public interface IContactUsService
    {
        Task<bool> InsertContact(ContactUs contact);
        Task<List<ContactUs>> GetContacts();

        Task<bool> SendEmail(ContactUs objContact);

    }
}
