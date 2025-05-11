using System;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.Services;
using TodoApp.Application;
using TodoApp.InterfaceAdapters;

class Program
{
    static void Main(string[] args)
    {
        // DIコンテナのセットアップ
        var serviceProvider = new ServiceCollection()
            .AddSingleton<ITodoService, TodoService>() // ITodoServiceの実装を登録
            .AddSingleton<AddTaskUseCase>()            // AddTaskUseCaseを登録
            .AddSingleton<ListTasksUseCase>()          // ListTasksUseCaseを登録
            .AddSingleton<ConsoleController>()         // ConsoleControllerを登録
            .BuildServiceProvider();

        // ConsoleControllerのインスタンスを取得して実行
        var consoleController = serviceProvider.GetService<ConsoleController>();
        consoleController?.Run();
    }
}
