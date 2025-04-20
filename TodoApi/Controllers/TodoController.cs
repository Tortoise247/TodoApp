using Microsoft.AspNetCore.Mvc;
using TodoShared;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private static List<TodoItem> _todos = new();

    [HttpGet]
    public ActionResult<List<TodoItem>> Get() => _todos;

    [HttpPost]
    public IActionResult Post(TodoItem item)
    {
        item.Id = _todos.Count > 0 ? _todos.Max(x => x.Id) + 1 : 1;
        _todos.Add(item);
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, TodoItem item)
    {
        var existing = _todos.FirstOrDefault(x => x.Id == id);
        if (existing == null) return NotFound();

        existing.Title = item.Title;
        existing.IsDone = item.IsDone;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var item = _todos.FirstOrDefault(x => x.Id == id);
        if (item == null) return NotFound();

        _todos.Remove(item);
        return NoContent();
    }
}
