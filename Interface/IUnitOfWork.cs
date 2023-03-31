
namespace Westcoast.web.Interface;

    public interface IUnitOfWork
    {
        ICourseRepository CourseRepository {get; }
        IUserRepository UserRepository {get; }
        Task<bool> Complete();
    }
