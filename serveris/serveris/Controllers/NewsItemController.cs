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
                _context.NewsItems.Add(new NewsItem { Text = "this is a sample text news 1", Date = new DateTime(2018, 11, 23), Title = "Title 1" });
                _context.NewsItems.Add(new NewsItem { Text = "this is a sample text news 2", Date = new DateTime(2018, 11, 24), Title = "Title 2" });
                _context.NewsItems.Add(new NewsItem { Text = "this is a sample text news 3", Date = new DateTime(2018, 11, 25), Title = "Title 3" });
                _context.NewsItems.Add(new NewsItem { Text = "this is a sample text news 4", Date = new DateTime(2018, 11, 26), Title = "Title 4" });
                _context.NewsItems.Add(new NewsItem { Text = "this is a sample text news 5", Date = new DateTime(2018, 11, 27), Title = "Title 5" });
                _context.NewsItems.Add(new NewsItem { Text = "this is a sample text news 6", Date = new DateTime(2018, 11, 28), Title = "Title 6" });
                _context.NewsItems.Add(new NewsItem { Text = "this is a sample text news 7", Date = new DateTime(2018, 11, 29), Title = "Title 7" });
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
