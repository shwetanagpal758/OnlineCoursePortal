using AutoMapper;
using DataAccess.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineCourse_Web1.Models;
using OnlineCourse_Web1.Services.IServices;
using System.Collections.Generic;

namespace OnlineCourse_Web1.Controllers
{
    public class CourseDetailController : Controller
    {
        private readonly ICourseDetailService _coursedetailService;
       
     
        public CourseDetailController(ICourseDetailService coursedetailService)
        {
            _coursedetailService = coursedetailService;
           
        }
        public async Task<IActionResult> IndexCourseDetail()
        {
            List<CourseDetailDTO> list = new();
            var response = await _coursedetailService.GetAllAsync<APIResponse>();
            if (response != null )
            {
                list = JsonConvert.DeserializeObject<List<CourseDetailDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        public async Task<IActionResult> CreateCourseDetail()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCourseDetail(CourseDetailDTO model)
        {
           if(ModelState.IsValid)
            {
                var response = await _coursedetailService.CreateAsync<APIResponse>(model);
                if (response != null)
                {
                    return RedirectToAction(nameof(IndexCourseDetail));
                }

            }
            return View(model);
        }
        public async Task<IActionResult> UpdateCourseDetail(int coursedetailId)
        {
            var response = await _coursedetailService.GetAsync<APIResponse>(coursedetailId);
            if (response != null)
            {
                CourseDetailDTO model = JsonConvert.DeserializeObject<CourseDetailDTO>(Convert.ToString(response.Result));
                //return View(_mapper.Map<CourseDetailDTO>(model));   
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCourseDetail(CourseDetailDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _coursedetailService.UpdateAsync<APIResponse>(model);
                if (response != null)
                {
                    return RedirectToAction(nameof(IndexCourseDetail));
                }

            }
            return View(model);
        }
        public async Task<IActionResult> DeleteCourseDetail(int coursedetailId)
        {
            var response = await _coursedetailService.GetAsync<APIResponse>(coursedetailId);
            if (response != null)
            {
                CourseDetailDTO model = JsonConvert.DeserializeObject<CourseDetailDTO>(Convert.ToString(response.Result));
                //return View(_mapper.Map<CourseDetailDTO>(model));   
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCourseDetail(CourseDetailDTO model)
        {
           
                var response = await _coursedetailService.DeleteAsync<APIResponse>(model.Id);
                if (response != null)
                {
                    return RedirectToAction(nameof(IndexCourseDetail));
                }

            
            return View(model);
        }
    }
}
