using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Text.Json;

namespace BlazorApp.Auth
{
    /// <summary>
    /// Quản lý session của user trong Blazor Server.
    /// Sử dụng ProtectedSessionStorage để lưu trữ bảo mật ở phía browser.
    /// </summary>
    public class SessionService
    {
        private const string SessionKey = "CoffeeShopUser";
        private readonly ProtectedSessionStorage _sessionStorage;

        public SessionService(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public async Task SetUserAsync(UserSession user)
        {
            await _sessionStorage.SetAsync(SessionKey, user);
        }

        public async Task<UserSession?> GetUserAsync()
        {
            try
            {
                var result = await _sessionStorage.GetAsync<UserSession>(SessionKey);
                return result.Success ? result.Value : null;
            }
            catch
            {
                return null;
            }
        }

        public async Task ClearAsync()
        {
            await _sessionStorage.DeleteAsync(SessionKey);
        }

        public async Task<bool> IsLoggedInAsync()
        {
            var user = await GetUserAsync();
            return user != null;
        }
    }
}
