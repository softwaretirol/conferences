

using MyNamespace;

Client client = new Client(new HttpClient()
{
    BaseAddress = new Uri("https://localhost:12345"),
    DefaultRequestHeaders =
    {
        { "authorize", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJjb25mZXJlbmNlIjoiRERDMjAyNCIsIm5iZiI6MTczMjU0OTYwOSwiZXhwIjoxNzMyNTUzMjA5LCJpYXQiOjE3MzI1NDk2MDl9.DGv1774vEDH9uqZ67q6CUnmxonEuEoJfdjUiMy9ejKg"}
    }
});
var person = await client.CustomersAsync(42);
Console.ReadLine();