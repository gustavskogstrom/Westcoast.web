using Westcoast.web.Models;

namespace Westcoast.web.Interface;

    public interface ICourseRepository
    {
        Task<IList<Course>> ListAllAsync();
        Task<Course?> FindByCourseIDAsync(int courseID);

        Task<Course?> FindByCourseNameAsync(string courseName);

        Task<bool> AddAsync(Course course);

        Task<bool> UpdateAsync(Course course);

        Task<bool> DeleteAsync(Course course);

        Task<bool> SaveAsync();
    }
