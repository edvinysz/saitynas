using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using serveris.Models;
using serveris.Services;
using Microsoft.AspNetCore.Authorization;

namespace serveris.Controllers
{
    [Authorize]
    [Route("api/win")]
    [ApiController]
    public class WinnerController : ControllerBase
    {
        private readonly WinnerContext _context;
        private IUserService userService;

        public WinnerController(WinnerContext context)
        {
            _context = context;

            if (_context.Winners.Count() == 0)
            {
                _context.Winners.Add(new Winner { GameId = 1, Win = "first" });
                _context.SaveChanges();
            }
        }

        //GET
        [HttpGet]
        public ActionResult<List<Winner>> GetAll()
        {
            return _context.Winners.ToList();
        }

        [HttpGet("{id}", Name = "GetWin")]
        public ActionResult<Winner> GetById(long id)
        {
            var item = _context.Winners.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        //POST
        [HttpPost]
        public IActionResult Create(Winner item)
        {
            _context.Winners.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetWin", new { id = item.Id }, item);
        }

        //DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.Winners.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Winners.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
