using Microsoft.EntityFrameworkCore;
using Yomikaze.Application.Data.Access;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities;
using Yomikaze.Domain.Database.Entities.Identity;
using Yomikaze.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("YomikazeDbContextConnection") ?? throw new InvalidOperationException("Connection string 'YomikazeDbContextConnection' not found.");

builder.Services.AddDbContext<YomikazeDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<YomikazeDbContext>();
builder.Services.AddRazorPages();


// Add services to the container.
builder.Services.AddControllersWithViews();
var services = builder.Services;
services.AddScoped<IDao<Comic>, ComicDao>();
services.AddScoped<IDao<Chapter>, ChapterDao>();
services.AddScoped<IDao<Page>, PageDao>();
services.AddScoped<IDao<Comment>, CommentDao>();
services.AddScoped<IDao<Genre>, GenreDao>();
services.AddScoped<IDao<LibraryEntry>, LibraryDao>();
services.AddSignalR();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();