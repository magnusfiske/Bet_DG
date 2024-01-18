using System.Security.Claims;

namespace Bet.Common.DTOs;

public record LoginUserDTO(string Email, string Password);
public record RegisterUserDTO(string Email, string Name, string Password, List<string> Roles);
public record PaidCustomerDTO(string Email);
public record TokenUserDTO(string Email, bool Save = true);
public record AuthenticatedUserDTO(string? AccessToken, string? UserName);
public record SignUpUserDTO(string Email, List<Claim> Roles);
public class SignInModel { public bool IsCustomer { get; set; } };