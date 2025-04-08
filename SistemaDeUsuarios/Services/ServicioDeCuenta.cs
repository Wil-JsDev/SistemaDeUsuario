using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SistemaDeUsuarios.DTOs;
using SistemaDeUsuarios.Enum;
using SistemaDeUsuarios.Interfaces;
using SistemaDeUsuarios.JWT;
using SistemaDeUsuarios.Models;
using SistemaDeUsuarios.Settings;
using SistemaDeUsuarios.Utils;

namespace SistemaDeUsuarios.Services;

public class ServicioDeCuenta(
    UserManager<Users> userManager, 
    RoleManager<IdentityRole> roleManager,
    SignInManager<Users> signInManager,
    IOptions<JwtSettings> jwtSettings) : IServicioDeCuentas
{
     private JwtSettings _JwtSettings {get;} = jwtSettings.Value;
     
     public async Task<ApiResponse<RegisterResponse>> RegisterAccountAsync(RegisterRequest request, Roles roles)
     {
         RegisterResponse response = new();
         var username = await userManager.FindByNameAsync(request.Username);
         if (username != null)
         {
             return ApiResponse<RegisterResponse>.ErrorResponse($"Este username {request.Username} esta tomado");
         }

         var userWithEmail = await userManager.FindByEmailAsync(request.Email);
         if (userWithEmail != null)
         {
             return ApiResponse<RegisterResponse>.ErrorResponse($"Este email {request.Email} ya esta tomado");
         }

         Users user = new()
         {
             FirstName = request.FirstName,
             LastName = request.LastName,
             UserName = request.Username,
             Email = request.Email,
             EmailConfirmed = true,
             PhoneNumber = request.PhoneNumber,
         };

         var result = await userManager.CreateAsync(user, request.Password);
         if (result.Succeeded)
         {
             response.Email = request.Email;
             response.Username = request.Username;
             response.UserId = user.Id;

             await userManager.AddToRoleAsync(user, roles.ToString());
         }else
         {
             return ApiResponse<RegisterResponse>.ErrorResponse($"A ocurrido un error al intentar Registrar al usuario {request.Username}");
         }
         return ApiResponse<RegisterResponse>.SuccessResponse(response);
     }
     
     public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest request)
     {
         AuthenticateResponse response = new();

         var user = await userManager.FindByEmailAsync(request.Email);
         if (user == null)
         {
             response.StatusCodes = 404;
             return response;
         }
         var result = await signInManager.PasswordSignInAsync(user.UserName, request.Password, false, false);

         if (!result.Succeeded)
         {
             response.StatusCodes = 401;
             return response;    
         }

         if (!user.EmailConfirmed)
         {
             response.StatusCodes = 400;
             return response;
         }
        
         JwtSecurityToken jwtSecurityToken = await GenerateTokenAsync(user);
        
         response.Id = user.Id;
         response.Username = user.UserName;
         response.FirstName = user.FirstName;
         response.LastName = user.LastName;
         response.Email = user.Email;

         var rolesList = await userManager.GetRolesAsync(user).ConfigureAwait(false);
        
         response.Roles = rolesList.ToList();
         response.IsVerified = user.EmailConfirmed;
         response.PhoneNumber = user.PhoneNumber;
         response.JwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
         var refreshToken = GenerateRefreshToken();
         response.RefreshToken = refreshToken.Token;
        
         return response;
     }
     
     #region private methods
     
     private async Task<JwtSecurityToken> GenerateTokenAsync(Users user)
     {
         var userClaims = await userManager.GetClaimsAsync(user);
         var roles = await userManager.GetRolesAsync(user);
     
         List<Claim> rolesClaims = new List<Claim>();
     
         foreach (var role in roles)
         {
             rolesClaims.Add(new Claim("roles", role));
         }

         var claim = new[]
             {
                 new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 new Claim(JwtRegisteredClaimNames.Email, user.Email),
                 new Claim("Id", user.Id)
             }
             .Union(userClaims)
             .Union(rolesClaims);
         
         var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JwtSettings.Key));
         var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

         var jwtSecurityToken = new JwtSecurityToken
         (
             issuer: _JwtSettings.Issuer,
             audience: _JwtSettings.Audience,
             claims: claim,
             expires: DateTime.Now.AddMinutes(_JwtSettings.DurationInMinutes),
             signingCredentials: signingCredentials
         );
         return jwtSecurityToken;
     }
     
     private RefreshToken GenerateRefreshToken()
     {
         return new RefreshToken
         {
             Token = RandomTokenString(),
             Expired = DateTime.UtcNow.AddDays(7),
             Created = DateTime.UtcNow
         };
     }
     
     private string RandomTokenString()
     {
         using var rng = RandomNumberGenerator.Create();
         var randomBytes = new Byte[40];
         rng.GetBytes(randomBytes);
         return BitConverter.ToString(randomBytes);
     }
     
     
     #endregion
    
}