using Demo.Foundation.DatabaseAccessor;
using Demo.Module.StoreManage.Models;
using Demo.Module.UserManage.Models;
using Demo.WebHost.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(o =>
o.UseNpgsql("Server=127.0.0.1;Port=5432;Database=test;Username=postgres;Password=postgres"));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();

var serviceProvider = app.Services.CreateScope().ServiceProvider;
if (serviceProvider!.GetService<AppDbContext>().Database.CanConnect())
{
    var userRepository = serviceProvider.GetRequiredService<IRepository<User>>();
    userRepository.Add(new User { UserName = "1" });
    userRepository.Add(new User { UserName = "2" });
    userRepository.Add(new User { UserName = "3" });
    var count = userRepository.AsQueryable().Count();
    Console.WriteLine(count);
    Console.ReadKey();
}