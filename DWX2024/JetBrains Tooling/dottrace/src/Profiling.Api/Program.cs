using Profiling.Api.Services.BlockingThreads;
using Profiling.Api.Services.HighCpuUsage;
using Profiling.Api.Services.MemoryLeak;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IHighCpuUsageService, HighCpuUsageService>();
builder.Services.AddTransient<IBlockingThreadsService, BlockingThreadsService>();
builder.Services.AddSingleton<IMemoryLeakService, MemoryLeakService>();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
