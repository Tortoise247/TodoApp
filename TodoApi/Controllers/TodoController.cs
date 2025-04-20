using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoShared;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly TodoDbContext _context;

    public TodoController(TodoDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<TodoItem>>> Get()
    {
        return await _context.Todos.ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Post(TodoItem item)
    {
        // 締め切りが指定されていない場合、デフォルト値を設定
        if (item.deadline == default)
        {
            item.deadline = DateTime.UtcNow.AddDays(7); // デフォルトで1週間後
        }

        _context.Todos.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, TodoItem item)
    {
        var existing = await _context.Todos.FindAsync(id);
        if (existing == null) return NotFound();

        existing.Title = item.Title;
        existing.IsDone = item.IsDone;
        existing.deadline = item.deadline; // 締め切りを更新
        await _context.SaveChangesAsync();

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Todos.FindAsync(id);
        if (item == null) return NotFound();

        _context.Todos.Remove(item);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
