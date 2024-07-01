namespace Dwx2024AspNetCore.Apis;

public static class PersonApi
{
    public static IEndpointRouteBuilder MapPersonApi(this IEndpointRouteBuilder app)
    {
        var personApi = app
            .MapGroup("/api/person")
            .RequireAuthorization();
        
        personApi.MapGet("/", GetAllPersons);
        personApi.MapGet("/{id}", (int id) =>
        {
            return new
            {
                Vorname = "Christian " + id,
                Nachname = "Giesswein"
            };
        });
        return app;
    }

    /// <summary>
    /// Query for all persons...
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    private static Task<Person[]> GetAllPersons(IConfiguration configuration)
    {
        return Task.FromResult<Person[]>([new Person()
        {
            Vorname = DateTime.Now.ToString()
        }]);
    }
}

public class Person
{
    public string Vorname { get; set; }
}