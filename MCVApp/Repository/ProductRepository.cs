using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace MCVApp.Repository
{
    public class ProductRepository
    {
        public HttpClient client { get; set; }
        public ProductRepository()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"].ToString());
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        }
        public HttpResponseMessage GetCategoryProduct(string url)
        {
            return client.GetAsync(url).Result;
        }
        public HttpResponseMessage AddProduct(string url, object productModel)
        {
            return client.PostAsJsonAsync(url, productModel).Result;
        }
        public HttpResponseMessage UpdateProduct(string url, object productModel)
        {
            return client.PutAsJsonAsync(url, productModel).Result;
        }
        public HttpResponseMessage GetProductById(string url)
        {
            return client.GetAsync(url).Result;
        }
        public HttpResponseMessage DeleteProduct(string url)
        {
            return client.DeleteAsync(url).Result;
        }
    }
}