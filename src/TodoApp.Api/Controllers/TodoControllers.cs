// Controllers/TodoController.cs
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private static List<Todo> todos = new List<Todo>();

        [HttpPost]
        public ActionResult<Todo> PostTodo(Todo todo)
        {
            if (todo == null || string.IsNullOrEmpty(todo.Title) )//|| string.IsNullOrEmpty(todo.Description))
            {
                return BadRequest("Title and description are required");
            }

            todo.Id = todos.Count > 0 ? todos.Max(t => t.Id) + 1 : 1;
            todo.IsCompleted = false; // デフォルト値を設定
            todos.Add(todo);
            return CreatedAtAction(nameof(PostTodo), new { id = todo.Id }, todo);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Todo>> GetTodos()
        {
            return Ok(todos);
        }

        [HttpPut("{id}")]
        public ActionResult<Todo> UpdateTodo(long id, Todo updatedTodo)
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            if (updatedTodo == null || string.IsNullOrEmpty(updatedTodo.Title) ) //|| string.IsNullOrEmpty(updatedTodo.Description))
            {
                return BadRequest("Title and description are required");
            }

            todo.Title = updatedTodo.Title;
            todo.Description = updatedTodo.Description;
            todo.IsCompleted = updatedTodo.IsCompleted;

            return Ok(todo);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTodo(long id)
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            todos.Remove(todo);
            return NoContent();
        }
    }
}