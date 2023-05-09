using BusinessLayer.Abstract;
using BusinessLayer.Concretes;
using DataAccessLayer.Abstracts;
using DataAccessLayer.Concretes;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<DbContext, DataAccessLayer.Context>();

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICourseService, CourseService>();

builder.Services.AddMvc();
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/Denied";
        options.Events = new CookieAuthenticationEvents()
        {
            OnSigningIn = async context =>
            {
                await Task.CompletedTask;
            },
            OnSignedIn = async context =>
            {
                await Task.CompletedTask;
            },
            OnValidatePrincipal = async context =>
            {
                await Task.CompletedTask;
            }
        };
    });
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
