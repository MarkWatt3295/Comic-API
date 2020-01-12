using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Comic_API
{
    public class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }

        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
           // ApiClient.BaseAddress = new Uri(""); //Set base address of the API
            ApiClient.DefaultRequestHeaders.Accept.Clear();

            //Give back json
            ApiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
