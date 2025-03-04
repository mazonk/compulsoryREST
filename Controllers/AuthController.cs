using Microsoft.AspNetCore.Mvc;
using CompulsoryREST.Services;
using CompulsoryREST.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Linq;

namespace CompulsoryREST.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly IMongoCollection<User> _usersCollection;

        public AuthController(JwtService jwtService, IConfiguration configuration)
        {
            _jwtService = jwtService;

            // Get MongoDB settings from environment variables or configuration
            var connectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_URI");
            var databaseName = Environment.GetEnvironmentVariable("MONGODB_DATABASE_NAME");
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            // Assuming there's a 'users' collection where the usernames and passwords are stored
            _usersCollection = database.GetCollection<User>("users");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Look for a user with matching username and password in the database
            var user = _usersCollection.Find(u => u.Username == request.Username).FirstOrDefault();
            
            if (user == null || user.Password != request.Password)
            {
                return Unauthorized("Invalid username or password");
            }

            // Generate JWT token
            var token = _jwtService.GenerateToken(request.Username);
            return Ok(new { Token = token });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
