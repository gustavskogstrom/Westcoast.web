using Microsoft.EntityFrameworkCore;
using Westcoast.web.Data;
using Westcoast.web.Interface;
using Westcoast.web.Models;

namespace Westcoast.web.Repository;

public class CourseRepository : Repository<Course>, ICourseRepository
{
    //private readonly WestcoastContext _context;
    public CourseRepository(WestcoastContext context) : base (context){}

    public async Task<Course?> FindByCourseIDAsync(int courseID)
    {
        return await _context.Courses.SingleOrDefaultAsync(c => c.CourseID== courseID);
    }

    public async Task<Course?> FindByCourseNameAsync(string courseName)
    {
        return await _context.Courses.SingleOrDefaultAsync(c => c.CourseName.Trim().ToLower() == courseName.Trim().ToLower());
    }
}

