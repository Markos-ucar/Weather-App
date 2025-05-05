using Flurl.Http;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class WeatherService
    {
        public async Task<ResponseModel> GetWeatherAsync(string location)
        {


            var weatherApiUrl = Constants.WeatherApiUrl;
            var weatherApiKey = Constants.WeatherApiKey;

            string apiUrl = $"{weatherApiUrl}.json?key={weatherApiKey}&q={location}";

            try
            {
                var response = await apiUrl.GetAsync();

                if (response.StatusCode == 200)
                {
                    var responseData = await response.GetJsonAsync<WeatherModel>();
                    return ResponseModel.Success(responseData);
                }
                else
                {
                    return ResponseModel.Error(response.ResponseMessage.ToString());
                }
            }
            catch (FlurlHttpException ex)
            {
                return ResponseModel.Error(ex.Message); // Return the exception message as an error
            }
        }
    }
}
