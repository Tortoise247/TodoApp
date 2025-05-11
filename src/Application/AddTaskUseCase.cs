using TodoApp.Services;

namespace TodoApp.Application
{
    public class AddTaskUseCase
    {
        private readonly ITodoService _todoService;

        public AddTaskUseCase(ITodoService todoService)
        {
            _todoService = todoService;
        }

        public void Execute(string task)
        {
            if (string.IsNullOrWhiteSpace(task))
            {
                throw new ArgumentException("Task cannot be empty.");
            }

            _todoService.AddTask(task);
        }
    }
}