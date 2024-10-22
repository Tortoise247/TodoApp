using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class IndexModel : PageModel
{
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl;

    public IndexModel(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiBaseUrl = configuration["TodoAppAPI:BaseUrl"];
    }

    public List<TodoItem> Todos { get; private set; } = new List<TodoItem>();
    [BindProperty]
    public TodoItem NewTodo { get; set; } = new TodoItem();

    public async Task OnGet()
    {
        // TodoApp.API を呼び出して Todo リストを取得
        Todos = await _httpClient.GetFromJsonAsync<List<TodoItem>>(_apiBaseUrl);
    }

    public async Task<IActionResult> OnPostAddAsync()
    {
        // TodoApp.API を呼び出して新しい Todo を追加
        var response = await _httpClient.PostAsJsonAsync(_apiBaseUrl, NewTodo);
        if (response.IsSuccessStatusCode)
        {
            // 最新の Todo リストを取得
            await OnGet();
            return Page();
        }
        return Page();
    }

    public async Task<IActionResult> OnPostUpdateAsync(int id, string title, bool isCompleted, string description)
    {
        // TodoApp.API を呼び出して Todo を更新
        var updatedTodo = new TodoItem { Id = id, Title = title, IsCompleted = isCompleted , Description = description };
        var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/{id}", updatedTodo);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage();
        }
        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        // TodoApp.API を呼び出して Todo を削除
        var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{id}");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage();
        }
        return Page();
    }
}

public class TodoItem
{
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public bool IsCompleted { get; set; } = false;
}