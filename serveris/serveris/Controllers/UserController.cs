using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using serveris.Models;

//Speju sitas nenaudojamas -> UserItem
namespace serveris.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;

            if (_context.UserItems.Count() == 0)
            {
                //sukuriam nauja userItem jei empty, reiskia negalim istrint visu userItems
                _context.UserItems.Add(new UserItem { Name = "Itemas" });
                _context.SaveChanges();
            }
        }


        // GET METODAS
        [HttpGet]
        public ActionResult<List<UserItem>> GetAll()
        {
            return _context.UserItems.ToList();
        }

        [HttpGet("{id}", Name = "GetUser")]
        public ActionResult<UserItem> GetById(long id)
        {
            var item = _context.UserItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        //POST METODAS
        [HttpPost]
        public IActionResult Create(UserItem item)
        {
            _context.UserItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetUser", new { id = item.Id }, item);
        }

        //UPDATE METODAS
        [HttpPut("{id}")]
        public IActionResult Update(long id, UserItem item)
        {
            var todo = _context.UserItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.AccountBalance = item.AccountBalance;
            todo.Name = item.Name;

            _context.UserItems.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        //DELETE METODAS
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.UserItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.UserItems.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
