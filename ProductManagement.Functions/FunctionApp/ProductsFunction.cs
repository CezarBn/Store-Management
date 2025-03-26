using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProductManagement.Models;

namespace ProductManagement.FunctionApp
{
    public static class ProductsFunction
    {
        private static readonly string ConnectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

        [FunctionName("GetProducts")]
        public static async Task<IActionResult> GetProducts(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "products")] HttpRequest req,
            ILogger log)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Products FOR JSON AUTO", connection);
                var result = await command.ExecuteScalarAsync();
                return new OkObjectResult(result);
            }
        }

        [FunctionName("CreateProduct")]
        public static async Task<IActionResult> CreateProduct(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "products")] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Product>(requestBody);

            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("InsertProduct", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Name", data.Name);
                command.Parameters.AddWithValue("@Description", data.Description);
                command.Parameters.AddWithValue("@Price", data.Price);
                command.Parameters.AddWithValue("@CompanyId", data.CompanyId);
                await command.ExecuteNonQueryAsync();
            }

            return new OkResult();
        }
    }
}
