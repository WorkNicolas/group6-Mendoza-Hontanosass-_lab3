/// <summary>
/// User Repository Interface
/// </summary>
/// <remarks>
/// Defines contract for user data access operations
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanosas</author>
/// <version>0.1</version>
/// <date>2025-10-25</date>
using group6_Mendoza_Hontanosass__lab3.Models;
namespace group6_Mendoza_Hontanosass__lab3.Data.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(string userId);
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetByRoleAsync(UserRole role);
        Task<int> GetTotalUsersAsync();
        Task<int> GetUserCountByRoleAsync(UserRole role);
    }
}
