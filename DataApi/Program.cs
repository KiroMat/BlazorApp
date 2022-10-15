using Data.Db;
using DataApi.Extensions;
using DataApi.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;    // aby mo¿na by³o includowaæ propertki podczas zapytania EF Core, inaczej jest zapêtlenie w encjach
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValitators();

builder.Services.AddDbContextFactory<DataContext>(options =>
{
    options.UseInMemoryDatabase("DbTest");
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFront", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        //policy.WithOrigins("https://localhost:7193", "http://localhost:5193");
    });
});

builder.Services.AddScoped(sp =>
{
    var identityOptions = new IdentityOptions();
    var httpContext = sp.GetService<IHttpContextAccessor>().HttpContext;
    if (httpContext.User.Identity.IsAuthenticated)
    {
        identityOptions.UserId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        identityOptions.FirstName = httpContext.User.FindFirst(ClaimTypes.GivenName).Value;
        identityOptions.LastName = httpContext.User.FindFirst(ClaimTypes.Surname).Value;
    }
    return identityOptions;
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

CreateDbIfNotExists(app);

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


void CreateDbIfNotExists(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<DataContext>();
            context.Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }
}