using System.Net.Http;
using System.Threading.Tasks;

namespace AuthCodeFlow.Client.Helpers
{
    public interface IAppHttpClient
    {
        Task<HttpClient> GetClient();
    }
}
