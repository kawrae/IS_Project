using Microsoft.EntityFrameworkCore;
using project.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "chinook.db");

builder.Services.AddDbContext<ChinookDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
