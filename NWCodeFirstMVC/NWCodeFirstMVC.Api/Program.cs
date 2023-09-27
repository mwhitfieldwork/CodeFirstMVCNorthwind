using Microsoft.EntityFrameworkCore;
using NWCodeFirstMVC.App.Contracts;
using NWCodeFirstMVC.Infrastructure.Services;
using NWCodeFirstMVC.Domain.Models;
using NWCodeFirstMVC.Domain;
using NLog;
using NuGet.Protocol.Plugins;
using NWCodeFirstMVC.Api.Configurations;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<northwindContext>(opt =>
{
    var configuration = builder.Configuration;
    var connectionString = configuration.GetConnectionString("Default");
    opt.UseSqlServer(connectionString);
});

builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<northwindContext>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", 
        b=> b.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MapperConfig));

NLog.LogManager.Setup().LoadConfiguration(builder => {
    builder.ForLogger().FilterMinLevel(NLog.LogLevel.Info).WriteToConsole();
    builder.ForLogger().FilterMinLevel(NLog.LogLevel.Debug).WriteToFile(fileName: "file.txt");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseRouting();
app.UseAuthorization();
app.UseStaticFiles();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
