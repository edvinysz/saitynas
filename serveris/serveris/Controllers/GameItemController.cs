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
    [Route("api/game")]
    [ApiController]
    public class GameItemController : ControllerBase
    {
        public static BetItemContext _context;
        private IUserService userService;

        public GameItemController(BetItemContext context)
        {
            _context = context;

            if (_context.GameItems.Count() == 0)
            {
                _context.GameItems.Add(new GameItem { FirstTeamId = "Zalgiris", SecondTeamId = "Rytas", Firstkof = 1.5, Secondkof = 3.0, Winner = "", IsComplete = false });
                _context.GameItems.Add(new GameItem { FirstTeamId = "Antanas", SecondTeamId = "Pranas", Firstkof = 1.8, Secondkof = 1.7, Winner = "1", IsComplete = false });
                _context.SaveChanges();
            }
        }

        public static List<GameItem> test()
        {
            return _context.GameItems.ToList();
        }

        //GET
        [HttpGet]
        public ActionResult<List<GameItem>> GetAll()
        {
            return _context.GameItems.ToList();
        }

        [HttpGet("{id}", Name = "GetGame")]
        public ActionResult<GameItem> GetById(long id)
        {
            var item = _context.GameItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        //POST
        [HttpPost]
        public IActionResult Create(GameItem item)
        {
            _context.GameItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetGame", new { id = item.Id }, item);
        }

        //UPDATE
        [HttpPut("{id}")]
        public IActionResult Update(long id, GameItem item)
        {
            var todo = _context.GameItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }
            
            todo.FirstTeamId = item.FirstTeamId;
            todo.SecondTeamId = item.SecondTeamId;
            todo.Firstkof = item.Firstkof;
            todo.Secondkof = item.Secondkof;
            todo.Winner = item.Winner;
            todo.IsComplete = item.IsComplete;

            _context.GameItems.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        //DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.GameItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.GameItems.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
