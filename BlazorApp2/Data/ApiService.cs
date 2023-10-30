using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorApp2.Data
{
    public class ApiService
    {
        private readonly HttpClient httpClient;

        public ApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> GetApiData()
        {
            try
            {
                // Make the HTTP GET request to your API endpoint
                var response = await httpClient.GetAsync("/WeatherForecast/get-db");

                // Ensure the response was successful
                response.EnsureSuccessStatusCode();

                // Read the response content as a string
                var responseBody = await response.Content.ReadAsStringAsync();

                return responseBody;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the API call
                return ex.Message;
            }
        }
    }
}