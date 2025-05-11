using TodoApp.Services;

namespace TodoApp.Application
{
    public class ListTasksUseCase
    {
        private readonly ITodoService _todoService;

        public ListTasksUseCase(ITodoService todoService)
        {
            _todoService = todoService;
        }

        public List<string> Execute()
        {
            return _todoService.GetTasks();
        }
    }
}