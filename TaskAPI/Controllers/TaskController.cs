using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskAPI.Models;

namespace TaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        // CREATE => POST
        // READ => GET
        // UPDATE => PUT/PATCH
        // DELETE => DELETE

        // will use in memory storage
        private static readonly List<ToDoItems> _todoItems = [];

        // GET api/tasks
        [HttpGet]

        public ActionResult<IEnumerable<ToDoItems>> Get() 
        {
            return Ok(_todoItems);
        }

        // GET api/tasks/1
        [HttpGet("{id}")]

        public ActionResult<ToDoItems> Get(int id)
        {
            var todoItem = _todoItems.FirstOrDefault(x => x.Id == id);
            if (todoItem == null)
            { 
                return NotFound();
            }
            return Ok(todoItem);
        }

        //POST api/tasks
        [HttpPost]
        public ActionResult Post([FromBody] ToDoItems todoItem)
        {

            _todoItems.Add(todoItem);
            return CreatedAtAction(nameof(Get), new { id = todoItem.Id }, todoItem);
        }

        // POST api/tasks/1
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ToDoItems todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            var todoItemToUpdate = _todoItems.FirstOrDefault(x => x.Id == id);
            if (todoItemToUpdate == null) 
            { 
                return NotFound();
            }
            
            todoItemToUpdate.Title = todoItem.Title;
            todoItemToUpdate.Description = todoItem.Description;
            todoItemToUpdate.IsCompleted = todoItem.IsCompleted;

            return NoContent();
        }

        //DELETE api/tasks/1
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var todoItemToDelete = _todoItems.FirstOrDefault(x => x.Id == id);
            if (todoItemToDelete == null)
            {
                return NotFound();
            }

            _todoItems.Remove(todoItemToDelete);
            return NoContent();
        }
    }
}
