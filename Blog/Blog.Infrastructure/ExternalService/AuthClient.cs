using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.ExternalService
{
    public class AuthClient : IAuthClient
    {
        private readonly HttpClient _httpClient;

        public AuthClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<UserInfoDto?> Getme(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            var response = await _httpClient.GetAsync("Me");
            if(!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<UserInfoDto>();
        }
    }
}
