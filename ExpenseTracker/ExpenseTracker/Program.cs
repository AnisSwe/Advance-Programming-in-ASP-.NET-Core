using BLL.Services;
using DAL.EF;
using DAL.Repos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// DB
builder.Services.AddDbContext<ExpenseTrackerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConn")));

// Session
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

// DAL
builder.Services.AddScoped<ExpenseRepo>();
builder.Services.AddScoped<BudgetRepo>();
builder.Services.AddScoped<AlertRepo>();
builder.Services.AddScoped<UserRepo>();
builder.Services.AddScoped<CategoryRepo>();

// BLL
builder.Services.AddScoped<ExpenseService>();
builder.Services.AddScoped<BudgetService>();
builder.Services.AddScoped<AlertService>();
builder.Services.AddScoped<UserService>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();