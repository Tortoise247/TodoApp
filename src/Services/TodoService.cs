using System;
using System.Collections.Generic;

namespace TodoApp.Services
{
    public class TodoService
    {
        private readonly List<string> _todoList = new();

        public void AddTask(string task)
        {
            if (string.IsNullOrEmpty(task))
            {
                throw new ArgumentException("Task cannot be empty.");
            }
            _todoList.Add(task);
        }

        public List<string> GetTasks()
        {
            return new List<string>(_todoList);
        }
    }
}