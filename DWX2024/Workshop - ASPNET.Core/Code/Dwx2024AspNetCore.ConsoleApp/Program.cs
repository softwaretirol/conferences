
var baseAddress = "http://localhost:5021/";
var httpClient = new HttpClient()
{
    BaseAddress = new Uri(baseAddress)
};
// httpClient.DefaultRequestHeaders.Authorization
var client = new Dwx2024AspNetCore.Web.Client.Client(httpClient);

var persons = await client.PersonGetAsync();
foreach (var person in persons)
{
    Console.WriteLine("Vorname = " + person.Vorname);
}

