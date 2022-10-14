using Data.Db;
using DataApi.Shared.Models;
using DataApi.Shared.Responses;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IValidator<User> _validator;

        private const string securityKey = "BE462382102B9116FF2E5B368E4F7E1C";

        public UserController(DataContext context, IValidator<User> validator)
        {
            context.Database.EnsureCreated();
            _context = context;
            _validator = validator;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<User>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await _context.Users.AnyAsync() ? 
                Ok(await _context.Users.ToListAsync()) :
                NotFound();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type =typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if(user is null)
                return NotFound();
            else
                return Ok(user);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            var validResult = await _validator.ValidateAsync(user);

            if(!validResult.IsValid)
            {
                return ValidationProblem(new ValidationProblemDetails(validResult.ToDictionary()));
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User user)
        {
            var validResult = await _validator.ValidateAsync(user);

            if (!validResult.IsValid)
            {
                return ValidationProblem(new ValidationProblemDetails(validResult.ToDictionary()));
            }

            var userDb = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (userDb is null)
            {
                return NotFound();
            }
            else
            {
                userDb.Password = user.Password;                  // <- 
                userDb.Login = user.Login;                        // <-
                                                                  //  |
                _context.ChangeTracker.DetectChanges(); // Detectes changes but only when is replace value by value
                await _context.SaveChangesAsync();
            }

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user is null)
                return NotFound();
            else
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var userDb = await _context.Users.FirstOrDefaultAsync(x => x.Login == user.Login);
            if (userDb is null)
                return BadRequest();
            if(userDb.Password != user.Password)
                return BadRequest();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userDb.Id.ToString()),
                new Claim(ClaimTypes.GivenName, userDb.Login)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var token = new JwtSecurityToken(
                issuer: "http://localhost:3000",
                audience: "http://localhost:3000",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
            var tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new TokenResponse { Token = tokenAsString });
        }
    }
}
