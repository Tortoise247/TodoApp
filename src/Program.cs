using System;
using TodoApp.Services;
using TodoApp.Application;
using TodoApp.InterfaceAdapters;

class Program
{
    static void Main(string[] args)
    {
        ITodoService todoService = new TodoService();
        var addTaskUseCase = new AddTaskUseCase(todoService);
        var listTasksUseCase = new ListTasksUseCase(todoService);

        var consoleController = new ConsoleController(addTaskUseCase, listTasksUseCase);
        consoleController.Run();
    }
}
