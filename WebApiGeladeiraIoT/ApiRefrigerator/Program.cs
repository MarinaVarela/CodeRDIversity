using Application.Mapping;
using Infrastructure.Config;
using Infrastructure.Repositories;
using IoC;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ApiRefrigerator.Repository;
using Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

DependencyInjection.RegisterServices(builder.Services);

builder.Services.AddDbContext<RefrigeratorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddSwaggerGen(options =>
{
    SwaggerConfig.ConfigureSwagger(options);
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddScoped<RefrigeratorRepository>();
builder.Services.AddScoped<RefrigeratorService>();

builder.Services.AddDbContext<RefrigeratorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
