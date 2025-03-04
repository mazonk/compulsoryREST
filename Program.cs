using CompulsoryREST.Services;
using CompulsoryREST.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DotNetEnv;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables
Env.Load();
builder.Configuration.AddEnvironmentVariables();

// Configure MongoDB settings
builder.Services.Configure<MongoDBSettings>(options =>
{
    options.ConnectionURI = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_URI") ?? throw new InvalidOperationException("MONGODB_CONNECTION_URI is not set");
    options.DatabaseName = Environment.GetEnvironmentVariable("MONGODB_DATABASE_NAME") ?? throw new InvalidOperationException("MONGODB_DATABASE_NAME is not set");
    options.CollectionName = Environment.GetEnvironmentVariable("MONGODB_COLLECTION_NAME") ?? throw new InvalidOperationException("MONGODB_COLLECTION_NAME is not set");
});

// Add MongoDB service
builder.Services.AddSingleton<MongoDBService>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure JWT authentication
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? throw new InvalidOperationException("JWT_SECRET_KEY is not set");
    
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ValidateIssuer = true,
        ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? throw new InvalidOperationException("JWT_ISSUER is not set"),
        ValidateAudience = true,
        ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? throw new InvalidOperationException("JWT_AUDIENCE is not set"),
        ValidateLifetime = true
    };
});

// Add authorization services
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Ensure routing and controllers are registered
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

Console.WriteLine("🚀 Application is starting...");

app.Run();
