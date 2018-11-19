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
    [Route("api/bet")]
    [ApiController]
    public class BetItemController : ControllerBase
    {
        private readonly BetItemContext _context;
        private IUserService userService;

        public BetItemController(BetItemContext context)
        {
            _context = context;

            if (_context.betItems.Count() == 0)
            {
                _context.betItems.Add(new BetItem { PersonId = 999 });
                _context.SaveChanges();
            }
        }

        //GET
        [HttpGet]
        public ActionResult<List<BetItem>> GetAll()
        {
            return _context.betItems.ToList();
        }

        [HttpGet("{id}", Name = "GetBet")]
        public ActionResult<BetItem> GetById(long id)
        {
            var item = _context.betItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        //POST
        [HttpPost]
        public IActionResult Create(BetItem item)
        {
            _context.betItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetBet", new { id = item.Id }, item);
        }

        //UPDATE
        [HttpPut("{id}")]
        public IActionResult Update(long id, BetItem item)
        {
            var todo = _context.betItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.BetMoney = item.BetMoney;
            todo.ChosenId = item.ChosenId;
            todo.GameId = item.GameId;
            todo.HasWon = item.HasWon;
            todo.PersonId = item.PersonId;
            todo.PossibleWinMoney = item.PossibleWinMoney;

            _context.betItems.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        //DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.betItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.betItems.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
