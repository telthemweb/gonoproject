using Application.Models.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace AdminClient.Authentication
{
    public class CustomeAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomeAuthenticationStateProvider(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userSessionStorageResult = await _sessionStorage.GetAsync<AuthResponse>("UserSession");
                var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;
                if (userSession == null)
                    return await Task.FromResult(new AuthenticationState(_anonymous));
                ClaimsIdentity claimsIdentity = new ClaimsIdentity();
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, userSession.UserName));
                claims.Add(new Claim(ClaimTypes.Sid, userSession.Id));
                claims.Add(new Claim(ClaimTypes.Expired, userSession.isExpired));
                if (userSession.Roles != null && userSession.Roles.Count() > 0)
                {
                    foreach (var item in userSession.Roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, item));
                    }
                }
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "CustomAuth"));
               // NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }

        }

        public async Task UpdateAuthenticationState(AuthResponse userSession)
        {
            ClaimsPrincipal claimsPrincipal;
            if (userSession != null)
            {
                await _sessionStorage.SetAsync("UserSession", userSession);
                ClaimsIdentity claimsIdentity = new ClaimsIdentity();
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, userSession.UserName));
                claims.Add(new Claim(ClaimTypes.Expired, userSession.isExpired));
                claims.Add(new Claim(ClaimTypes.Sid, userSession.Id));
                if (userSession.Roles != null && userSession.Roles.Count() > 0)
                {
                    foreach (var item in userSession.Roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, item));
                    }
                }


                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "CustomAuth"));
            }
            else
            {
                await _sessionStorage.DeleteAsync("UserSession");
                claimsPrincipal = _anonymous;
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}
