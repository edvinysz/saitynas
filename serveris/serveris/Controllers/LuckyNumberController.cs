using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using serveris.Models;
using Microsoft.AspNetCore.Authorization;
using serveris.Services;

namespace serveris.Controllers
{
    [Route("api/luckynumber")]
    [ApiController]
    public class LuckyNumberController : ControllerBase
    {
        private readonly LuckyNumberContext _context;
        private IUserService _userService;

        public LuckyNumberController(LuckyNumberContext context)
        {
            _context = context;

            if (_context.LuckyNumbers.Count() == 0)
            {
                Random random = new Random();
                int randomNumber = random.Next(0, 100);

                //sukuriam nauja userItem jei empty, reiskia negalim istrint visu userItems
                _context.LuckyNumbers.Add(new LuckyNumber { Number = randomNumber, Date = DateTime.Today });
                _context.SaveChanges();
            }
        }


        // GET METODAS
        [HttpGet]
        public ActionResult<List<LuckyNumber>> GetAll()
        {
            return _context.LuckyNumbers.ToList();
        }

        [HttpGet("{id}", Name = "GetNumber")]
        public ActionResult<LuckyNumber> GetById(long id)
        {
            var item = _context.LuckyNumbers.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        //POST METODAS
        [HttpPost]
        public IActionResult Create(LuckyNumber item) //input token and check if used
        {
            /*var user = _userService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });
                */
            _context.LuckyNumbers.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetUser", new { id = item.Id }, item);
        }

        //DELETE METODAS
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.LuckyNumbers.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.LuckyNumbers.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
