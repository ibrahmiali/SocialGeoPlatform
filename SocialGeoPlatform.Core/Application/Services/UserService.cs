using SocialGeoPlatform.Core.Domain.Entities;
using SocialGeoPlatform.Core.Domain.Interfaces;

namespace SocialGeoPlatform.Core.Application.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<List<User>> GetAllUsersAsync() => await _userRepository.GetAllUsersAsync();
    public async Task<User> GetUserByIdAsync(Guid id) => await _userRepository.GetUserByIdAsync(id);
    public async Task<User> CreateUserAsync(User user) => await _userRepository.CreateUserAsync(user);
    public async Task<User> UpdateUserAsync(User user) => await _userRepository.UpdateUserAsync(user);
    public async Task DeleteUserAsync(Guid id) => await _userRepository.DeleteUserAsync(id);
}