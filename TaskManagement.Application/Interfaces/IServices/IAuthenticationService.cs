using TaskManagement.Application.DTOs.Authentication;

namespace TaskManagement.Application.Interfaces.IServices
{
    public interface IAuthenticationService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
    }
}
