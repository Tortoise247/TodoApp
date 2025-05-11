using System.Collections.Generic;

namespace TodoApp.Services
{
    public interface ITodoService
    {
        void AddTask(string task);
        List<string> GetTasks();
    }
}