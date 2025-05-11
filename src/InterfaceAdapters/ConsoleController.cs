using System;
using TodoApp.Application;

namespace TodoApp.InterfaceAdapters
{
    public class ConsoleController
    {
        private readonly AddTaskUseCase _addTaskUseCase;
        private readonly ListTasksUseCase _listTasksUseCase;

        public ConsoleController(AddTaskUseCase addTaskUseCase, ListTasksUseCase listTasksUseCase)
        {
            _addTaskUseCase = addTaskUseCase;
            _listTasksUseCase = listTasksUseCase;
        }

        public void Run()
        {
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
                        _addTaskUseCase.Execute(task);
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
                    var tasks = _listTasksUseCase.Execute();
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
}