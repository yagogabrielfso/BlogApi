
using ApiBlog.DTOs;

namespace ApiBlog.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);
        Task<UserDto> AuthUserAsync(string username, string password);
    }
}
