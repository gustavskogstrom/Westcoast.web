using Microsoft.EntityFrameworkCore;
using Westcoast.web.Data;
using Westcoast.web.Interface;
using Westcoast.web.Models;

namespace Westcoast.web.Repository;

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(WestcoastContext context): base(context){}

        public async Task<User?> FindByUserIDAsync(int userID)
        {
            return await _context.Users.SingleOrDefaultAsync(c => c.UserID== userID);
        }
    }
