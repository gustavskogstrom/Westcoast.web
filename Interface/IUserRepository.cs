using Westcoast.web.Models;

namespace Westcoast.web.Interface;

    public interface IUserRepository
    {
        Task<IList<User>> ListAllAsync();

        Task<User?> FindByUserIDAsync(int id);

        Task<bool> AddAsync(User user);

        Task<bool> UpdateAsync(User user);

        Task<bool> DeleteAsync(User user);
        Task<bool> SaveAsync();
    }