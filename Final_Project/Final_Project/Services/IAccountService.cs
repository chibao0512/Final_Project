using Final_Project.DTOs;
using static Final_Project.Responses.CustomResponses;

namespace Final_Project.Services
{
    public interface IAccountService
    {

        Task<RegistrationResponses> RegistrationAsync(RegisterDTO model);
        Task<LoginResponses> LoginAsync(LoginDTO model);
    }
}
