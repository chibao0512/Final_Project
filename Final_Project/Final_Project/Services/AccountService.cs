using Final_Project.DTOs;
using Final_Project.Responses;
using static Final_Project.Responses.CustomResponses;

namespace Final_Project.Services
{
    public class AccountService: IAccountService
    {
        private readonly HttpClient _httpClient;
        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    public async Task<LoginResponses> LoginAsync(LoginDTO model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/account/login",model);
            var result = await response.Content.ReadFromJsonAsync<LoginResponses>();
            return result;
        }

        public async Task<RegistrationResponses> RegistrationAsync(RegisterDTO model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/account/register", model);
            var result = await response.Content.ReadFromJsonAsync<RegistrationResponses>();
            return result;
        }
    }
   
}

