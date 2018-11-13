using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using serveris.Models;

namespace serveris.Controllers
{
    [Route("api/serveris")]
    [ApiController]
    public class serverisController : ControllerBase
    {
        private readonly serverisContext _context;

        public serverisController(serverisContext context)
        {
            _context = context;

            if (_context.serverisItems.Count() == 0)
            {
                //sukuriam nauja serverisItem jei empty, reiskia negalim istrint visu serverisItems
                _context.serverisItems.Add(new serverisItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }
         

        // GET METODAS
        [HttpGet]
        public ActionResult<List<serverisItem>> GetAll()
        {
            return _context.serverisItems.ToList();
        }

        [HttpGet("{id}", Name = "Getserveris")]
        public ActionResult<serverisItem> GetById(long id)
        {
            var item = _context.serverisItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        //POST METODAS
        [HttpPost]
        public IActionResult Create(serverisItem item)
        {
            _context.serverisItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("Getserveris", new { id = item.Id }, item);
        }

        //UPDATE METODAS
        [HttpPut("{id}")]
        public IActionResult Update(long id, serverisItem item)
        {
            var todo = _context.serverisItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            _context.serverisItems.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        //DELETE METODAS
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.serverisItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.serverisItems.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
