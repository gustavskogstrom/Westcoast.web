using Microsoft.EntityFrameworkCore;
using Westcoast.web.Models;

namespace Westcoast.web.Data;

    public class WestcoastContext: DbContext
    {
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<User> Users => Set<User>();
        public WestcoastContext(DbContextOptions options) :base(options) { }
    }
