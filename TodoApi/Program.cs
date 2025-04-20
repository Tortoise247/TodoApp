using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoApi.Data;

var builder = WebApplication.CreateBuilder(args);

// SQLite データベースの設定を追加
builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseSqlite("Data Source=todo.db"));

// Add services to the container.
builder.Services.AddControllers(); // 必要なサービスを追加
   builder.Services.AddCors(options =>
   {
       options.AddDefaultPolicy(policy =>
       {
           policy.WithOrigins("http://localhost:5168") // クライアントのURL
                 .AllowAnyHeader()
                 .AllowAnyMethod();
       });
   });

var app = builder.Build();
   app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // コントローラーをマッピング
});

app.Run();
