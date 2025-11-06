using TaskManagement.Application.DTOs.Authentication;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Interfaces.IRepositories;
using TaskManagement.Application.Interfaces.IServices;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Exceptions;
using BCrypt.Net;

namespace TaskManagement.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthenticationService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            // Check if email already exists
            if (await _userRepository.EmailExistsAsync(registerDto.Email))
            {
                throw new ValidationException("Email", "Email is already registered.");
            }

            // Create new user
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Role = registerDto.Role,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _userRepository.CreateAsync(user);

            // Generate token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role.ToString(),
                ExpiresAt = DateTime.UtcNow.AddHours(1)
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            // Find user by email
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);

            if (user == null)
            {
                throw new UnauthorizedException("Invalid email or password.");
            }

            // Verify password
            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                throw new UnauthorizedException("Invalid email or password.");
            }

            // Generate token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role.ToString(),
                ExpiresAt = DateTime.UtcNow.AddHours(1)
            };
        }


    }
}
