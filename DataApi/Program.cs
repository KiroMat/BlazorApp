using Data.Db;
using DataApi.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup => 
{
    setup.EnableAnnotations();
});

builder.Services.AddValitators();

builder.Services.AddDbContextFactory<DataContext>(options =>
{
    options.UseInMemoryDatabase("DbTest");
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFront", policy =>
    {
        policy.WithOrigins("https://localhost:7193", "http://localhost:5193");
    });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => 
    {
        //options.RoutePrefix = string.Empty;  // swagger as root
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowFront");
app.UseAuthorization();

app.MapControllers();

app.Run();
