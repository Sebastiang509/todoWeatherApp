namespace TodoWeatherApp.Controllers

open Microsoft.AspNetCore.Mvc
open TodoWeatherApp.Models
open TodoWeatherApp.Services
open System
open System.Threading.Tasks

[<ApiController>]
[<Route("api/todo")>]
type TodoController() =

    let mutable todos = [
        { Id = Guid.NewGuid(); Title = "Learn F#"; IsCompleted = false; Location = "New York"; Weather = None }
        { Id = Guid.NewGuid(); Title = "Go Hiking"; IsCompleted = true; Location = "Los Angeles"; Weather = None }
    ]

    // GET: api/todo
    [<HttpGet>]
    member _.GetTodos() : Task<TodoItem list> =
        task {
            let! updatedTodos =
                todos
                |> List.map (fun todo ->
                    task {
                        let! weather = WeatherService.getWeatherAsync todo.Location
                        return { todo with Weather = weather }
                    })
                |> Task.WhenAll

            return updatedTodos |> List.ofArray
        }
