using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using Amazon.Lambda.Serialization.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWSLambda2
{
    public class Function
    {

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public List<Dictionary<string, AttributeValue>> FunctionHandler(string input, ILambdaContext context)
        {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();

            // Define item key
            //  Hash-key of the target item is string value "Mark Twain"
            //  Range-key of the target item is string value "The Adventures of Tom Sawyer"
            Dictionary<string, AttributeValue> key = new Dictionary<string, AttributeValue>
{
                { "Restaurant", new AttributeValue { S = "Chez_Roger" } },
                { "ID", new AttributeValue { N = "111" } }
            };

            // Create GetItem request
            var request = new ScanRequest
            {
                TableName = input,
            };

            // Issue request
            var result = client.ScanAsync(request);
            string[] output = new string[10];
            int i = 0;
            List <Dictionary<string, AttributeValue>> items = result.Result.Items;
            foreach(var item in items)
                foreach (var keyValuePair in item)
                {
                    Console.WriteLine(keyValuePair.Value);
                    output[i] = keyValuePair.Value.S;
                    i++;
                }

            return items;
        }
    }
}
