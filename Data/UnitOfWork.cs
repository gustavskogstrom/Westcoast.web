using Westcoast.web.Interface;
using Westcoast.web.Repository;

namespace Westcoast.web.Data;

public class UnitOfWork : IUnitOfWork
{
        private readonly WestcoastContext _context;
    public UnitOfWork(WestcoastContext context)
    {
            _context = context;

    }

    public ICourseRepository CourseRepository => new CourseRepository(_context);

    public IUserRepository UserRepository => new UserRepository(_context);

    public async Task<bool> Complete()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
