using System;
using TodoApp.Services;

class Program
{
    static void Main(string[] args)
    {
        ITodoService todoService = new TodoService(); // インターフェースを使用
        string command;

        Console.WriteLine("Simple TodoApp");
        Console.WriteLine("Commands: add [task], list, exit");

        do
        {
            Console.Write("> ");
            command = Console.ReadLine()?.Trim();

            if (command?.StartsWith("add ") == true)
            {
                var task = command.Substring(4).Trim();
                try
                {
                    todoService.AddTask(task);
                    Console.WriteLine($"Added: {task}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else if (command == "list")
            {
                Console.WriteLine("Todo List:");
                var tasks = todoService.GetTasks();
                if (tasks.Count == 0)
                {
                    Console.WriteLine("  (No tasks)");
                }
                else
                {
                    for (int i = 0; i < tasks.Count; i++)
                    {
                        Console.WriteLine($"  {i + 1}. {tasks[i]}");
                    }
                }
            }
            else if (command != "exit")
            {
                Console.WriteLine("Unknown command. Use 'add [task]', 'list', or 'exit'.");
            }

        } while (command != "exit");

        Console.WriteLine("Goodbye!");
    }
}
