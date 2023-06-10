using AutoMapper;
using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineCourse_Web1.Models;
using OnlineCourse_Web1.Services;
using OnlineCourse_Web1.Services.IServices;
using System.Collections.Generic;

namespace OnlineCourse_Web1.Controllers
{
    public class CourseDetailController : Controller
    {
        private readonly ICourseDetailService _coursedetailService;
        private readonly IBookingService _coursebookingService;

        public CourseDetailController(ICourseDetailService coursedetailService, IBookingService coursebookingService)
        {
            _coursedetailService = coursedetailService;
            _coursebookingService = coursebookingService;
        }
        [Authorize(Roles = Roles.Role_Admin)]
        public async Task<ActionResult<APIResponse>> Approve(int Id)
        {



            if (ModelState.IsValid)
            {
                var response = await _coursedetailService.UpdateStatusAsync<APIResponse>(Id);


            }
            TempData["success"] = "Approved";
            return RedirectToAction(nameof(ListFalse));

        }
        [Authorize(Roles = Roles.Role_Admin)]
        public async Task<ActionResult<APIResponse>> Reject(int Id)
        {

            var response = await _coursedetailService.DeleteAsync<APIResponse>(Id);

            return RedirectToAction(nameof(ListFalse));


        }

        public async Task<IActionResult> IndexCourseDetail()
        {
            List<CourseDetail> list = new();
            var response = await _coursedetailService.GetAllTrueAsync<APIResponse>();
            if (response != null )
            {
                list = JsonConvert.DeserializeObject<List<CourseDetail>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        public async Task<IActionResult> ListAll()
        {
            List<CourseDetail> list = new();
            var response = await _coursedetailService.GetAllAsync<APIResponse>();
            if (response != null)
            {
                list = JsonConvert.DeserializeObject<List<CourseDetail>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        [Authorize(Roles = Roles.Role_Admin)]
        public async Task<IActionResult> ListFalse()
        {
            List<CourseDetail> list = new();
            var response = await _coursedetailService.GetAllFalseAsync<APIResponse>();
            if (response != null)
            {
                list = JsonConvert.DeserializeObject<List<CourseDetail>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> CreateCourseDetail()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCourseDetail(CourseDetail model)
        {
           if(ModelState.IsValid)
            {
                var response = await _coursedetailService.CreateAsync<APIResponse>(model);
                if (response != null)
                {
                    TempData["success"] = "Created Successfull";
                    return RedirectToAction(nameof(IndexCourseDetail));
                }

            }
            TempData["error"] = "Error encountered";
            return View(model);
        }
        public async Task<IActionResult> UpdateCourseDetail(int coursedetailId)
        {
            var response = await _coursedetailService.GetAsync<APIResponse>(coursedetailId);
            if (response != null)
            {
                
                CourseDetail model = JsonConvert.DeserializeObject<CourseDetail>(Convert.ToString(response.Result));
                //return View(_mapper.Map<CourseDetailDTO>(model));   
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCourseDetail(CourseDetail model)
        {
            if (ModelState.IsValid)
            {
                TempData["success"] = "Updated Successfull";
                var response = await _coursedetailService.UpdateAsync<APIResponse>(model);
                if (response != null)
                {
                    return RedirectToAction(nameof(IndexCourseDetail));
                }

            }
            TempData["error"] = "Error encountered";
            return View(model);
        }
        public async Task<IActionResult> DeleteCourseDetail(int coursedetailId)
        {
            var response = await _coursedetailService.GetAsync<APIResponse>(coursedetailId);
            if (response != null)
            {
                CourseDetail model = JsonConvert.DeserializeObject<CourseDetail>(Convert.ToString(response.Result));
                //return View(_mapper.Map<CourseDetailDTO>(model));   
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCourseDetail(CourseDetail model)
        {
           
                var response = await _coursedetailService.DeleteAsync<APIResponse>(model.Id);
                if (response != null)
                {
                TempData["success"] = "deleted Successfull";
                return RedirectToAction(nameof(IndexCourseDetail));
                }
            TempData["error"] = "Error encountered";

            return View(model);
        }
    }
}
