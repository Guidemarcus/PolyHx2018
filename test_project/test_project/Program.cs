using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace test_project
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
                System.Text.Encoding.ASCII.GetBytes(
                    string.Format("{0}:{1}", "efe9485c-63b6-4583-9f12-4cbd9996e376", "eb41c52f-575e-44a7-b973-2bdfca279b98")
                )
            ));
            StringContent content = new StringContent("scopes=paymentapi&grant_type=client_credentials", System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
            //HttpResponseMessage response = await client.PostAsync("https://hack.softheon.io/oauth2/connect/token", content);
            Task<HttpResponseMessage> response = client.PostAsync("https://hack.softheon.io/oauth2/connect/token", content);
            response.Wait();

            Task<Stream> receiveStream = response.Result.Content.ReadAsStreamAsync();
            receiveStream.Wait();
            StreamReader readStream = new StreamReader(receiveStream.Result, System.Text.Encoding.UTF8);
            string responseText = readStream.ReadToEnd();

            var definition = new { access_token = "", expires_in = "", token_type = ""};

            var lolol = JsonConvert.DeserializeAnonymousType(responseText, definition);

            Console.Write(responseText);
            Console.ReadLine();
        }
    }
}
