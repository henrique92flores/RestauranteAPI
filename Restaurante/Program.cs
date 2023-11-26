using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Restaurante.Data;
using Restaurante.Model;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("FoodConnection");

builder.Services.AddDbContext<FoodContext>(opts =>
    opts.UseLazyLoadingProxies().UseNpgsql(connectionString));


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => builder
        .WithOrigins("*")
        .AllowAnyHeader()
        .AllowAnyMethod());
});

builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<FoodContext>()
    .AddDefaultTokenProviders();

//builder.Services.
//    AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Restaunte", Version = "v1" });
//    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
//    c.IncludeXmlComments(xmlPath);
//});
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Restaunte",
        Version = "v1"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

//app.UseAuthorization();

app.MapControllers();

app.Run();