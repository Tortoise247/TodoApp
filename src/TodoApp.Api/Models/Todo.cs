// Models/Todo.cs
namespace TodoApi.Models
{
    public class Todo
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; }= string.Empty;

        public bool IsCompleted { get; set; } = false;
    }
}