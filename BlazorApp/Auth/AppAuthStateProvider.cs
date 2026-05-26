using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorApp.Auth
{
    public class AppAuthStateProvider : AuthenticationStateProvider
    {
        private readonly SessionService _sessionService;

        public AppAuthStateProvider(SessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var userSession = await _sessionService.GetUserAsync();

            if (userSession == null)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userSession.PublicId.ToString()),
                new Claim(ClaimTypes.Name, userSession.FullName),
                new Claim(ClaimTypes.Email, userSession.Email),
                new Claim(ClaimTypes.Role, userSession.IsManager ? "Manager" : userSession.IsEmployee ? "Employee" : "Customer")
            };

            var identity = new ClaimsIdentity(claims, "CustomAuth");
            var principal = new ClaimsPrincipal(identity);

            return new AuthenticationState(principal);
        }

        public void NotifyUserAuthentication(UserSession userSession)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userSession.PublicId.ToString()),
                new Claim(ClaimTypes.Name, userSession.FullName),
                new Claim(ClaimTypes.Email, userSession.Email),
                new Claim(ClaimTypes.Role, userSession.IsManager ? "Manager" : userSession.IsEmployee ? "Employee" : "Customer")
            };

            var identity = new ClaimsIdentity(claims, "CustomAuth");
            var principal = new ClaimsPrincipal(identity);
            var authState = Task.FromResult(new AuthenticationState(principal));
            
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
