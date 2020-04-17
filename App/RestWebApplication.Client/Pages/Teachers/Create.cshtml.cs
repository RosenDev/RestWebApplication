using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RestWebApplication.Client.Pages.Teachers
{
    public class Create : PageModel
    {
        private readonly IHttpClientFactory httpClientFactory;

        public Create(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        
        public void OnGet()
        {
            
        }

        public void OnPost()
        {
            
        }
    }
}