using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SistemaDeUsuarios.Context;
using SistemaDeUsuarios.JWT;
using SistemaDeUsuarios.Models;
using SistemaDeUsuarios.Settings;

namespace SistemaDeUsuarios.Extension;

public static class Extensions
{
    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SistemaDbContext>(x =>
        {
            x.UseNpgsql(configuration.GetConnectionString("SistemaUsuario"));
        });
    }
    public static void AddIdentityMethod(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<SistemaDeUsuarioContext>(x =>
        {
            x.UseNpgsql(configuration.GetConnectionString("IdentityConnection"));
        });
        
        services.AddIdentity<Users, IdentityRole>()
            .AddEntityFrameworkStores<SistemaDeUsuarioContext>()
            .AddDefaultTokenProviders();
        
        
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        
        services.AddAuthentication(options =>
                              {
                                  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                  
                              }).AddJwtBearer(options =>
                              {
                                  options.RequireHttpsMetadata = false;
                                  options.SaveToken = false;
                                  options.TokenValidationParameters = new TokenValidationParameters
                                  {
                                      ValidateIssuerSigningKey = true,
                                      ValidateIssuer = true,
                                      ValidateAudience = true,
                                      ValidateLifetime = true,
                                      ClockSkew = TimeSpan.Zero,
                                      ValidIssuer = configuration["JwtSettings:Issuer"],
                                      ValidAudience = configuration["JwtSettings:Audience"],
                                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                                  };
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    },

                    OnChallenge = c =>
                    {
                        c.HandleResponse();
                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, Error = "No estas autorizado" });
                        return c.Response.WriteAsync(result);
                    },
                    OnForbidden = c =>
                    {
                        c.Response.StatusCode = 403;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, Error = "No estas autorizado a este recurso" });
                        return c.Response.WriteAsync(result);
                    }
                };
            });
        
    }
}