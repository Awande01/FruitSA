using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;
using System.Net;
using System.Configuration;

namespace Repository
{
    public class CategoryRepository
    {
        public HttpClient client { get; set; }
        public CategoryRepository()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"].ToString());
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        }
        public HttpResponseMessage GetCategoryDetails(string url)
        {
            
            return client.GetAsync(url).Result;
        }
        public HttpResponseMessage AddCategory(string url, object categoryM)
        {
            return client.PostAsJsonAsync(url, categoryM).Result;
        }
        public HttpResponseMessage UpdateCategory(string url, object categoryM)
        {
            return client.PutAsJsonAsync(url, categoryM).Result;
        }
        public HttpResponseMessage GetCategoryByCode(string url)
        {
            return client.GetAsync(url).Result;
        }
        public HttpResponseMessage GetCategoryByID(string url)
        {
            return client.GetAsync(url).Result;
        }
    }

}
