using System.Text.Json;
using Westcoast.web.Models;

namespace Westcoast.web.Data;

    public static class SeedData
    {
        public static async Task LoadCoursesData(WestcoastContext context)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (context.Courses.Any()) return;
            
            var json = System.IO.File.ReadAllText("Data/Json/Courses.json");
            var courses = JsonSerializer.Deserialize<List<Course>>(json, options);
            if(courses is not null && courses.Count > 0)
            {
                await context.Courses.AddRangeAsync(courses);
                await context.SaveChangesAsync();
            }
        }

    public static async Task LoadUsersData(WestcoastContext context)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (context.Users.Any()) return;
            
            var json = System.IO.File.ReadAllText("Data/Json/Users.json");
            var users = JsonSerializer.Deserialize<List<User>>(json, options);
            if(users is not null && users.Count > 0)
            {
                await context.Users.AddRangeAsync(users);
                await context.SaveChangesAsync();
            }
        }

}
