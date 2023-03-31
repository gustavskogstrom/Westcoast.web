using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Westcoast.web.Models;

namespace Westcoast.web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View("Index");
    }
}
