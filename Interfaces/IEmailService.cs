using MyWebApi.Core.Model;

namespace MyWebApi.Interfaces
{
    public interface IEmailService
    {
        Task<EmailResponse> GetUserDetailsByIDAsync(int  userId);
    }
}
