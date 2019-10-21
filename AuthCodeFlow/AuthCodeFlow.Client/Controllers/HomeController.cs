using AuthCodeFlow.Client.Helpers;
using AuthCodeFlow.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AuthCodeFlow.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAppHttpClient _http;
        public HomeController(IAppHttpClient http)
        {
            _http = http;
        }

        public async Task<IActionResult> Index()
        {
           await WriteOutIdentityInformation();
            // call the API
            var httpClient = await _http.GetClient();
            var response = await httpClient.GetAsync("api/books").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var booksAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var books = new List<Book>(JsonConvert.DeserializeObject<IList<Book>>(booksAsString).ToList());
                return View(books);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                    response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("AccessDenied", "Authorization");
            }

            throw new Exception($"A problem happened while calling the API: {response.ReasonPhrase}");
        }

        public async Task WriteOutIdentityInformation()
        {
            // get the saved identity token
            var identityToken = await HttpContext
                .GetTokenAsync(OpenIdConnectParameterNames.IdToken);
            // write it out
            Debug.WriteLine($"Identity token: {identityToken}");

            // write out the user claims
            foreach (var claim in User.Claims)
            {
                Debug.WriteLine($"Claim type: {claim.Type} - Claim value: {claim.Value}");
            }
        }

    }
}