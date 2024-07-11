using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;


using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;



public static class ChatGPTCustomPost
{




    
    [FunctionName("ChatGPTCustomPost")]
  public static async Task<IActionResult>  Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
    {
            try    
        {


        // Configure in Function->Configuration->applicaiton Settings
        string AOAIEndpoint =   Environment.GetEnvironmentVariable("AOAIEndpoint");
        string AOAIDeploymentId = Environment.GetEnvironmentVariable("AOAIDeploymentId");
        string AOAIKey = Environment.GetEnvironmentVariable("AOAIKey");
        string ChatGptUrl = Environment.GetEnvironmentVariable("ChatGptUrl");
        string ChatGptKey = Environment.GetEnvironmentVariable("ChatGptKey");
        string SearchEndpoint = Environment.GetEnvironmentVariable("SearchEndpoint");
        string SearchKey = Environment.GetEnvironmentVariable("SearchKey");
        string SearchIndex = Environment.GetEnvironmentVariable("SearchIndex");




        // Read the request body
       // string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            string userMessage = req.Query["userMessage"];

        // Prepare the request payload
        var payload = new
        {
            dataSources = new[]
            {
                new
                {
                    type = "AzureCognitiveSearch",
                    parameters = new
                    {
                        endpoint = SearchEndpoint,
                        key = SearchKey,
                        indexName = SearchIndex
                    }
                }
            },
            messages = new[]
            {
                new
                {
                    role = "user",
                    content = userMessage
                }
            }
        };

        // Create an HttpClient instance
        using (HttpClient client = new HttpClient())
        {
            // Set the request headers
            client.DefaultRequestHeaders.Add("api-key", AOAIKey);
            client.DefaultRequestHeaders.Add("chatgpt_url", ChatGptUrl);
            client.DefaultRequestHeaders.Add("chatgpt_key", ChatGptKey);

            // Serialize the payload
            string serializedPayload = JsonConvert.SerializeObject(payload);

            // Create the request content
            StringContent content = new StringContent(serializedPayload, System.Text.Encoding.UTF8, "application/json");

            // Make the POST request
            HttpResponseMessage response = await client.PostAsync($"{AOAIEndpoint}/openai/deployments/{AOAIDeploymentId}/extensions/chat/completions?api-version=2023-06-01-preview", content);

            // Read the response content
            string responseContent = await response.Content.ReadAsStringAsync();
        



            //log request query
         //  await SaveDataToTableStorage(userMessage);





            // Return the response from the API
            return new ContentResult
            {
                Content = responseContent,
                ContentType = "application/json",
                StatusCode = (int)response.StatusCode
            };
           


        }
           }
        catch (Exception ex)
        {
            return new ContentResult
            {
                Content = $"An error occurred: {ex.Message}",
                ContentType = "text/plain",
                StatusCode = 500
            };
        }


    }



    
 private static async Task SaveDataToTableStorage(string userMessage)
    {

                 string AOAITableConnString =   Environment.GetEnvironmentVariable("AOAITableConnString");

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(AOAITableConnString);
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
                CloudTable table = tableClient.GetTableReference("aoiqs");
                await table.CreateIfNotExistsAsync();

                QS entity = new QS{ qs= userMessage};

                entity.PartitionKey = "bb";
                entity.RowKey = Guid.NewGuid().ToString();

                TableOperation insertOperation = TableOperation.InsertOrReplace(entity);

                await table.ExecuteAsync(insertOperation);

    }

}
