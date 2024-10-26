// Controllers/TodoController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // 追加
using TodoApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {

        private readonly TodoContext _context;
        private static List<Todo> todos = new List<Todo>();
        public TodoController(TodoContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> PostTodo(Todo todo)
        {
            if (todo == null || string.IsNullOrEmpty(todo.Title))//|| string.IsNullOrEmpty(todo.Description))
            {
                return BadRequest("Title and description are required");
            }

            todo.Id = todos.Count > 0 ? todos.Max(t => t.Id) + 1 : 1;
            todo.IsCompleted = false; // デフォルト値を設定
            todos.Add(todo);

            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostTodo), new { id = todo.Id }, todo);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
        {
            return await _context.Todos.ToListAsync();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(long id, Todo updatedTodo)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            if (updatedTodo == null || string.IsNullOrEmpty(updatedTodo.Title) || string.IsNullOrEmpty(updatedTodo.Description))
            {
                return BadRequest("Title and description are required");
            }

            todo.Title = updatedTodo.Title;
            todo.Description = updatedTodo.Description;
            todo.IsCompleted = updatedTodo.IsCompleted;

            await _context.SaveChangesAsync();

            return Ok(todo);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(long id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}