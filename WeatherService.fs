namespace TodoWeatherApp.Services

open System.Net.Http
open System.Text.Json
open System.Threading.Tasks

type WeatherResponse = {
    main: {| temp: float |}
    weather: {| description: string |} array
}

module WeatherService =

    let httpClient = new HttpClient()
    let apiKey = "your-api-key-here" // Replace with your actual API key
    let baseUrl = "https://api.openweathermap.org/data/2.5/weather?q="

    let getWeatherAsync (city: string) : Task<WeatherInfo option> =
        task {
            try
                let url = $"{baseUrl}{city}&appid={apiKey}&units=metric"
                let! response = httpClient.GetStringAsync(url)
                let weatherData = JsonSerializer.Deserialize<WeatherResponse>(response)

                return Some {
                    Temperature = weatherData.main.temp
                    Condition = weatherData.weather.[0].description
                }
            with
            | _ -> return None
        }
