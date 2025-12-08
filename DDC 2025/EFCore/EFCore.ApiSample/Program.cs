using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddDbContextPool<ConferenceDb>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("ConferenceDb"));
    });
// builder.Services.AddDbContextPool<>();

builder
    .Services
    .AddDbContextFactory<ConferenceDb>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("ConferenceDb"));
    });
// builder.Services.AddPooledDbContextFactory<>();

var app = builder.Build();
app.UseHttpsRedirection();
app.MapGet("/", (ConferenceDb db) =>
{
    return db.Conferences.Select(x => new
    {
        x.Id,
        x.Name
    }).ToList();
});
app.MapGet("/factory", (IDbContextFactory<ConferenceDb> factory) =>
{
    using var db = factory.CreateDbContext();
    return db.Conferences.Select(x => new
    {
        x.Id,
        x.Name
    }).ToList();
});
app.Run();