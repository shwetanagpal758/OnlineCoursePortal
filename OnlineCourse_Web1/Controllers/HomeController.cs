using DataAccess.Models;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Nest;
using OnlineCourse_Web1.Models;
using OnlineCourse_Web1.Services;
using OnlineCourse_Web1.Services.IServices;
using System.Diagnostics;

namespace OnlineCourse_Web1.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

       


        public HomeController(ILogger<HomeController> logger, ICourseDetailRepository courseDetail)
        {
            _logger = logger;
           
        }

        public async Task<IActionResult> Index()
        {

         //   IEnumerable<CourseDetail> Coursedetaillist = _courseDetail.GetAllAsync(includeProperties: "CourseDetail");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}