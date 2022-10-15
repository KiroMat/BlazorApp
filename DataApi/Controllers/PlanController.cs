using Data.Db;
using DataApi.Options;
using DataApi.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IdentityOptions _identity;

        public PlanController(DataContext context, IdentityOptions identity)
        {
            _context = context;
            _identity = identity;
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

        // POST api/<PlanController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PlanController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PlanController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
