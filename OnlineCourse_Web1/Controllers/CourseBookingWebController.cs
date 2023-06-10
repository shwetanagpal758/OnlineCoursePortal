using DataAccess;
using DataAccess.Models;
using DataAccess.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nest;
using Newtonsoft.Json;
using OnlineCourse_Web1.Services;
using OnlineCourse_Web1.Services.IServices;

namespace OnlineCourse_Web1.Controllers
{
    public class CourseBookingWebController : Controller
    {
        private readonly ICourseDetailService _coursedetailService;
        private readonly IBookingService _coursebookingService;


        public CourseBookingWebController(ICourseDetailService coursedetailService, IBookingService coursebookingService)
        {
            _coursedetailService = coursedetailService;
            _coursebookingService = coursebookingService;
        }
        [Authorize(Roles = Roles.Role_Customer)]
        public async Task<ActionResult<APIResponse>> DeleteBooking(int Id)
        {


           // var bookinglist = await _coursebookingService.GetAsync<APIResponse>(Id);


          //  CourseBooking bookinglst = JsonConvert.DeserializeObject<CourseBooking>(Convert.ToString(bookinglist.Result));


            var response = await _coursebookingService.DeleteAsync<APIResponse>(Id);



            return RedirectToAction(nameof(Index));



        }
        public async Task<IActionResult> Index()
        {
            List<CourseBooking> list = new();


            var response = await _coursebookingService.GetAllAsync<APIResponse>();

            if (response != null)
            {
                list = JsonConvert.DeserializeObject<List<CourseBooking>>(Convert.ToString(response.Result));

            }

            List<CourseBooking> myCourses = new List<CourseBooking>();

            string UserName = User.Identity.Name;
            var list1 = list.Where(u => u.CourseBooking_Name == UserName);


            return View(list1);

        }
        [Authorize(Roles = Roles.Role_Customer)]
        public async Task<IActionResult> CreateBooking(int CourseId)
        {

            TempData["Id"] = CourseId;
            return View();

        }
        public async Task<IActionResult> Book(CourseBookingDTO createDTO)
        {

            createDTO.CourseId = Convert.ToInt32(TempData["Id"]);

            createDTO.CourseBooking_Name = User.Identity.Name;

            var courselst = await _coursedetailService.GetAsync<APIResponse>(createDTO.CourseId);


            CourseDetail courselist = JsonConvert.DeserializeObject<CourseDetail>(Convert.ToString(courselst.Result));


            if (createDTO.NumberOfUsers > courselist.AvailableSeats)
            {
                TempData["Error"] = "Not enough Courses";
                return RedirectToAction(nameof(Index));
            }
            else
            {

                await _coursebookingService.CreateAsync<APIResponse>(createDTO);


                TempData["success"] = "Course booked successfully";
                return RedirectToAction(nameof(Index));
            }


        }
    }
}
