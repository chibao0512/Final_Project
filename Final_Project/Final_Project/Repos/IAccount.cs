﻿using Final_Project.DTOs;
using static Final_Project.Responses.CustomResponses;

namespace Final_Project.Repos
{
    public interface IAccount
    {
        Task<RegistrationResponses> RegistrationAsync(RegisterDTO model);
        Task<LoginResponses> LoginAsync(LoginDTO model);
    }
}
