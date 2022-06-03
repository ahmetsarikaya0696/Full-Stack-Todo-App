using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoListAPI.Data;

namespace TodoListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public TodoItemsController(AppDbContext db)
        {
            _db = db;
        }


        [HttpGet]
        public List<TodoItem> Get()
        {
            // JSON'a çevirir.
            return _db.TodoItems.OrderBy(x => x.IsDone).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItem> Get(int id)
        {
            var todoItem = _db.TodoItems.Find(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }


        [HttpPost]
        public ActionResult<TodoItem> Post(TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                _db.TodoItems.Add(todoItem);
                _db.SaveChanges();
                return todoItem;
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _db.Update(todoItem);
                _db.SaveChanges();
                return NoContent();
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todoItem = _db.TodoItems.Find(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _db.Remove(todoItem);
            _db.SaveChanges();

            return NoContent();
        }
    }
}
