namespace SistemaDeUsuarios.DTOs;

public record RegisterRequest
(
    string? FirstName,
    string? LastName,
    string? Username,
    string? Email,
    string PhoneNumber,
    string? Password
);