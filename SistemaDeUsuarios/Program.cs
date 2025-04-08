using Microsoft.AspNetCore.Identity;
using SistemaDeUsuarios.Extension;
using SistemaDeUsuarios.Interfaces;
using SistemaDeUsuarios.Models;
using SistemaDeUsuarios.Seeds;
using SistemaDeUsuarios.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentityMethod(config);
builder.Services.AddScoped<IServicioDeCuentas, ServicioDeCuenta>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {   
        var userManager = services.GetRequiredService<UserManager<Users>>();
        var rolManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        
        await RolesPorDefecto.SeedAsync(userManager, rolManager);
    }
    catch (Exception)
    {
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();