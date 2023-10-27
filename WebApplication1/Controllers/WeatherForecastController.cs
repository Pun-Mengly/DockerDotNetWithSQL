using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("get-db")]
        public IEnumerable<string> GetPosts()
        {
            try
            {
                List<string> list = new List<string>();

                // Create a connection string.
                //string connectionString = "Server=uat-tab-01;Database=SpringDB;User Id=userapp;Password=AMKAdmin*&*;";
                string connectionString = "Server=db:1433;Database=tempdb;User Id=sa;Password=Mengly@123";

                // Create a SqlConnection object.
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection.
                    conn.Open();

                    // Create a SqlCommand object.
                    SqlCommand cmd = new SqlCommand("SELECT * FROM tblPosts", conn);

                    // Execute the query and get the results.
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Iterate through the results and print them to the console.
                    while (reader.Read())
                    {
                        list.Add(reader["Id"].ToString() + ": " + reader["Title"].ToString());
                    }

                    // Close the reader.
                    reader.Close();

                    // Close the connection.
                    conn.Close();
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}