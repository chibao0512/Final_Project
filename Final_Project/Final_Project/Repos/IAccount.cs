using Final_Project.DTOs;
using static Final_Project.Responses.CustomResponses;

namespace Final_Project.Repos
{
    public interface IAccount
    {
        Task<RegistationResponses> RegistationAsync(RegisterDTO model);
        Task<LoginResponses> LoginAsync(LoginDTO model);
    }
}
