using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

using Amazon.Lambda.Core;
using System.Net.Http.Headers;
using System.IO;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace Lambda_test_2
{
    public class Function
    {



        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<object> getSoftheonAccessToken(string input, ILambdaContext context)
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

            Stream receiveStream = await response.Result.Content.ReadAsStreamAsync();
            StreamReader readStream = new StreamReader(receiveStream, System.Text.Encoding.UTF8);
            string responseText = readStream.ReadToEnd();

            var definition = new { access_token = "", expires_in = "", token_type = "" };
            var obj = JsonConvert.DeserializeAnonymousType(responseText, definition);

            return obj;
        }
    }
}
