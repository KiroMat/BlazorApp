using Data.Db;
using DataApi.Options;
using DataApi.Shared.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security;
using System.Security.Principal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IdentityOptions _identity;
        private readonly IValidator<Plan> _validator;

        public PlanController(DataContext context, IdentityOptions identity, IValidator<Plan> validator)
        {
            _context = context;
            _identity = identity;
            _validator = validator;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedList<Plan>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<PagedList<Plan>>> Get(string? query, int page = 1, int pageSize = 12)
        {
            if(!await _context.Plans.AnyAsync())
                NotFound();

            if (string.IsNullOrWhiteSpace(query))
                query = "";
            if (page < 1)
                page = 1;
            if (pageSize < 5)
                pageSize = 5;
            if (pageSize > 50)
                pageSize = 50;

            var data = await _context.Plans
                    .Include(x => x.ToDoItems)
                    .Where(x => x.UserId == _identity.UserId && 
                        !x.IsDeleted && (x.Title.Contains(query) || 
                        x.Description.Contains(query)))
                    .OrderByDescending(x => x.CreatedDate)
                    .ToListAsync();

            return Ok(new PagedList<Plan>(data, page, pageSize));
        }

        // GET api/<PlanController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody][FromForm] Plan plan)
        {
            var validResult = await _validator.ValidateAsync(plan);

            if (!validResult.IsValid)
            {
                return ValidationProblem(new ValidationProblemDetails(validResult.ToDictionary()));
            }



            plan.Id = Guid.NewGuid().ToString();
            plan.UserId = _identity.UserId;
            plan.CreatedDate = DateTime.Now;

            await _context.Plans.AddAsync(plan);
            await _context.SaveChangesAsync();

            return Ok(plan);
        }

        // PUT api/<PlanController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody][FromForm] Plan plan)
        {
            var validResult = await _validator.ValidateAsync(plan);

            if (!validResult.IsValid)
            {
                return ValidationProblem(new ValidationProblemDetails(validResult.ToDictionary()));
            }

            var entity = await _context.Plans.FirstOrDefaultAsync(x => x.Id == id);
            if (entity is null)
                return BadRequest();

            entity.Title = plan.Title;
            entity.Description = plan.Description;
            entity.CoverPath = plan.CoverPath;
            entity.ModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE api/<PlanController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var entity = await _context.Plans.FirstOrDefaultAsync(x => x.Id == id);
            if (entity is null)
                return BadRequest();

            _context.Plans.Remove(entity);
            return NoContent();
        }
    }
}
