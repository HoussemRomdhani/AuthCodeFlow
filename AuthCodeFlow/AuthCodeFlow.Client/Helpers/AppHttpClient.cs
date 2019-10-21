using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AuthCodeFlow.Client.Helpers
{
    public class AppHttpClient : IAppHttpClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private HttpClient _httpClient = new HttpClient();

        public AppHttpClient(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<HttpClient> GetClient()
        {
            string accessToken = string.Empty;

            // get the current HttpContext to access the tokens
            var currentContext = _httpContextAccessor.HttpContext;

            // get access token
            accessToken = await currentContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                // set as Bearer token
                _httpClient.SetBearerToken(accessToken);
            }

            _httpClient.BaseAddress = new Uri("https://localhost:44351/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return _httpClient;
        }
    }
}
