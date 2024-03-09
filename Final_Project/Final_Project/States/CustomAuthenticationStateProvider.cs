using Final_Project.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Final_Project.States
{
    public class CustomAuthenticationStateProvider: AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal _claimsPrincipal = new (new ClaimsIdentity());
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(Constants.JwtToken))
                    return await Task.FromResult(new AuthenticationState(_claimsPrincipal));

                var getUserClaims = DecryptToken(Constants.JwtToken);
                if (getUserClaims == null) return await Task.FromResult(new AuthenticationState(_claimsPrincipal));

                var claimPrincipal = SetClaimPrincipal(getUserClaims);
                return await Task.FromResult(new AuthenticationState(claimPrincipal));
            }
            catch { return await Task.FromResult(new AuthenticationState(_claimsPrincipal)); }
           
        }

        public async void UdateAuthenticationState(string jwtToken)
        {
            var ClaimsPrincipal = new ClaimsPrincipal();
            if (!string.IsNullOrEmpty(jwtToken))
            {
                Constants.JwtToken = jwtToken;
                var getUserClaims = DecryptToken(jwtToken);
                ClaimsPrincipal = SetClaimPrincipal(getUserClaims);
            }
            else
            {
                Constants.JwtToken = null;
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(ClaimsPrincipal)));
        }

        private  static CustomUserClaims DecryptToken(string jwtToken)
        {
           if(!string.IsNullOrEmpty(jwtToken)) return new CustomUserClaims();
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);

            var lastName = token.Claims.FirstOrDefault(_ =>_.Type==ClaimTypes.Name);
            var firstName = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Name);
            var email = token.Claims.FirstOrDefault(_ =>_.Type == ClaimTypes.Email);
            return new CustomUserClaims(lastName!.Value, firstName!.Value, email!.Value);
        }

        public static ClaimsPrincipal SetClaimPrincipal(CustomUserClaims claims)
        {
            if (claims.user_Email is null) return new ClaimsPrincipal();
            return new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>
                {
                    new(ClaimTypes.Name, claims.user_Lastname),
                    new(ClaimTypes.Name, claims.user_Firstname),
                    new(ClaimTypes.Email, claims.user_Email),
                }, "JwtAuth")
                );
        }

    }
}
