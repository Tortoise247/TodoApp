using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        var todoList = new List<string>();
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
                if (!string.IsNullOrEmpty(task))
                {
                    todoList.Add(task);
                    Console.WriteLine($"Added: {task}");
                }
                else
                {
                    Console.WriteLine("Task cannot be empty.");
                }
            }
            else if (command == "list")
            {
                Console.WriteLine("Todo List:");
                if (todoList.Count == 0)
                {
                    Console.WriteLine("  (No tasks)");
                }
                else
                {
                    for (int i = 0; i < todoList.Count; i++)
                    {
                        Console.WriteLine($"  {i + 1}. {todoList[i]}");
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
