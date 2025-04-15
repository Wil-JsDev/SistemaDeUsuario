using SistemaDeUsuarios.DTOs;
using SistemaDeUsuarios.Enum;
using SistemaDeUsuarios.Utils;

namespace SistemaDeUsuarios.Interfaces;

public interface IServicioDeCuentas
{
    Task<ApiResponse<RegisterResponse>> RegisterAccountAsync(RegisterRequest request, Roles roles);

    Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest request);
}