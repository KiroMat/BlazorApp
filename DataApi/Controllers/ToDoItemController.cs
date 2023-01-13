using Data.Db;
using DataApi.Options;
using DataApi.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IdentityOptions _identity;

        public ToDoItemController(DataContext context, IdentityOptions identity)
        {
            _context = context;
            _identity = identity;
        }

        [HttpGet("plan/{planId}")]
        public async Task<IActionResult> GetAllByPlanId(string planId)
        {
            var allItems = await _context.ToDoItems.Where(x => x.PlanId == planId).ToListAsync();
            return Ok(allItems);
        }

        [HttpGet("{toDoItemId}")]
        public async Task<IActionResult> GetById(string toDoItemId)
        {
            var item = await _context.ToDoItems.FirstOrDefaultAsync(x => x.Id == toDoItemId);

            if (item is null)
                return NotFound();
            else
                return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ToDoItem item)
        {
            if(item is null)
                return BadRequest(item);

            item.Id = Guid.NewGuid().ToString().Substring(0,5);

            await _context.ToDoItems.AddAsync(item);
            await _context.SaveChangesAsync();

            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] ToDoItem toDoItem)
        {
            var item = await _context.ToDoItems.FirstOrDefaultAsync(x => x.Id == id);
            if (item is null)
                return BadRequest(item);

            item.Description= toDoItem.Description;
            item.IsDone=toDoItem.IsDone;
            await _context.SaveChangesAsync();

            return Ok(item);
        }

        [HttpDelete("{toDoItemId}")]
        public async Task<IActionResult> Delete(string toDoItemId)
        {
            var item = await _context.ToDoItems.FirstOrDefaultAsync(x => x.Id == toDoItemId);
            if(item is null)
            {
                return NotFound();
            }
            else
            {
                _context.ToDoItems.Remove(item);
            }
                
            return Ok();
        }
    }
}
