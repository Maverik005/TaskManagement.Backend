
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Interfaces.IServices
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
