using CompulsoryREST.Services;
using CompulsoryREST.Models;
using DotNetEnv;


var builder = WebApplication.CreateBuilder(args);

Env.Load();

// Load environment variables
builder.Configuration.AddEnvironmentVariables();

// Configure MongoDB settings
builder.Services.Configure<MongoDBSettings>(options =>
{
    options.ConnectionURI = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_URI") ?? throw new InvalidOperationException("MONGODB_CONNECTION_URI is not set");
    options.DatabaseName = Environment.GetEnvironmentVariable("MONGODB_DATABASE_NAME") ?? throw new InvalidOperationException("MONGODB_DATABASE_NAME is not set");
    options.CollectionName = Environment.GetEnvironmentVariable("MONGODB_COLLECTION_NAME") ?? throw new InvalidOperationException("MONGODB_COLLECTION_NAME is not set");
});

// Add services to the container.
builder.Services.AddSingleton<MongoDBService>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Ensure routing and controllers are registered
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers();
});

Console.WriteLine("🚀 Application is starting...");

app.Run();
