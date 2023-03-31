using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Westcoast.web.Data;
using Westcoast.web.Models;

namespace Westcoast.web.Controllers;

    [Route("Courses")]
    public class CoursesController : Controller
    {
        
        private readonly WestcoastContext _context;
        public CoursesController(WestcoastContext context)
        {
            _context = context;

        }

        public async  Task<IActionResult> Index() 
        {
            var courses = await  _context.Courses.ToListAsync();


            return View("Index", courses);
        }
        [Route("Details")]
        public IActionResult Details(string course)
        {
            ViewBag.CourseName = course;
            return View("Details");
        }
    }
