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
    [Route("api/news")]
    [ApiController]
    public class NewsItemController : ControllerBase
    {
        private readonly NewsItemContext _context;
        private IUserService userService;

        public NewsItemController(NewsItemContext context)
        {
            _context = context;

            if (_context.NewsItems.Count() == 0)
            {
                _context.NewsItems.Add(new NewsItem { Text = "this is a sample text" });
                _context.SaveChanges();
            }
        }

        //GET
        [HttpGet]
        public ActionResult<List<NewsItem>> GetAll()
        {
            return _context.NewsItems.ToList();
        }

        [HttpGet("{id}", Name = "GetNews")]
        public ActionResult<NewsItem> GetById(long id)
        {
            var item = _context.NewsItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        //POST
        [HttpPost]
        public IActionResult Create(NewsItem item)
        {
            _context.NewsItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetNews", new { id = item.Id }, item);
        }

        //UPDATE
        [HttpPut("{id}")]
        public IActionResult Update(long id, NewsItem item)
        {
            var todo = _context.NewsItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.Date = item.Date;
            todo.Text = item.Text;

            _context.NewsItems.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        //DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.NewsItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.NewsItems.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
