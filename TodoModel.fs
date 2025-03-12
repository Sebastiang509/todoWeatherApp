namespace TodoWeatherApp.Models

open System

type WeatherInfo = {
    Temperature: float
    Condition: string
}

type TodoItem = {
    Id: Guid
    Title: string
    IsCompleted: bool
    Location: string
    Weather: WeatherInfo option  // Nullable, will be fetched dynamically
}
