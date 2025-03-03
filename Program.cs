using CompulsoryREST.Services;
using CompulsoryREST.Models;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables
builder.Configuration.AddEnvironmentVariables();

// Configure MongoDB settings
builder.Services.Configure<MongoDBSettings>(options =>
{
    options.ConnectionURI = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_URI") ?? "";
    options.DatabaseName = Environment.GetEnvironmentVariable("MONGODB_DATABASE_NAME") ?? "";
    options.CollectionName = Environment.GetEnvironmentVariable("MONGODB_COLLECTION_NAME") ?? "";
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
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
