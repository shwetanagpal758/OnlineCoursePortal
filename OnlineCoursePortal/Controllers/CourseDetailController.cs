
using AutoMapper;
using Azure;
using DataAccess.Data;
using DataAccess.Models;
using DataAccess.Models.Dto;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nest;
using System.Net;

namespace OnlineCoursePortal.Controllers
{
    [Route("api/CourseDetail")]
    [ApiController]

    public class CourseDetailController : ControllerBase
    {

        private readonly ICourseDetailRepository _CourseDetailRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public CourseDetailController(ICourseDetailRepository CourseDetailRepo, IMapper mapper)
        {
            _CourseDetailRepo = CourseDetailRepo;
            _mapper = mapper;
            this._response = new();

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetCourseDetails()
       
        {
            try
            {

                IEnumerable<CourseDetail> coursedetailList = await _CourseDetailRepo.GetAllAsync();

                _response.Result = _mapper.Map<List<CourseDetail>>(coursedetailList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
                


               

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };

            }
            return _response;


        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetCourseDetail(int id)
        {
            try
            {

                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                CourseDetail coursedetail = await _CourseDetailRepo.GetAsync(u => u.Id == id);
                if (coursedetail == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = coursedetail;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        

        public async Task<ActionResult<APIResponse>> CreateCourseDetail([FromBody] CourseDetailDTO CourseDetailDTO)

        {
            try
            {

                if (CourseDetailDTO == null)
                {
                    return BadRequest(CourseDetailDTO);
                }


                CourseDetail courseDetail = _mapper.Map<CourseDetail>(CourseDetailDTO);



                 await _CourseDetailRepo.CreateAsync(courseDetail);
                //_response.Result = _mapper.Map<CourseDetailDTO>(courseDetail);
                _response.Result = courseDetail;
                _response.StatusCode = HttpStatusCode.Created;
                
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        /*CourseDetail model = _mapper.Map<CourseDetail>(CourseDetailDTO);

        _CourseDetailRepo.Add(model);

        return Ok(model);*/


     //   [Authorize(Roles = "admin")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<APIResponse>> DeleteCourseDetail(int id)
        {
            try
            {


                if (id == 0)
                {
                    return BadRequest();
                }

                var coursedetail = await _CourseDetailRepo.GetAsync(u => u.Id == id);
                if (coursedetail == null)
                {
                    return NotFound();
                }
                await _CourseDetailRepo.RemoveAsync(coursedetail);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<APIResponse>> UpdateCourseDetail(int id, [FromBody] CourseDetail coursedetail)
        {
            try
            {
                if (coursedetail == null )
                {
                    return BadRequest();
                }
                CourseDetail model = _mapper.Map<CourseDetail>(coursedetail);

                await _CourseDetailRepo.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPost("{id:int}")]
        public async Task<ActionResult<APIResponse>> UpdateDataInDatabase(int id)
        {

            // Find the data from the database using the provided ID
            var data = await _CourseDetailRepo.GetAsync(u => u.Id == id);

            if (data != null)
            {

                data.status = true;


                await _CourseDetailRepo.SaveAsync();
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);


            }
            else
            {

                _response.IsSuccess = false;

            }
            return _response;

        }
       [HttpGet("GetStatusTrue/status=true")]
        public async Task<ActionResult<APIResponse>> GetAll()
        {
            try
            {
                // Retrieve all the things where status is true
                List<CourseDetail> items = await _CourseDetailRepo.GetAllAsync();
                List<CourseDetail> filteredItems = items.Where(x => x.status).ToList();
                _response.Result = filteredItems;


                _response.StatusCode = HttpStatusCode.OK;




            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);
            //return Ok(filteredItems);
        }
        [HttpDelete("DeleteExpiredCourse")]
        private async Task DeleteExpiredCourse()
        {
            // Get the current date
            DateTime currentDate = DateTime.UtcNow.Date;//

            // Get the expired course
            var expiredEvents = await _CourseDetailRepo.GetAllAsync(e => e.StartDate.HasValue && e.StartDate.Value < currentDate);

            // Delete the expired course
            foreach (var eventsss in expiredEvents)
            {
                await _CourseDetailRepo.RemoveAsync(eventsss);
            }


        }
        [HttpGet("GetStatusFalse/status=false")]
        public async Task<ActionResult<APIResponse>> Getfalse()
        {


            try
            {
                // Delete expired events
                await DeleteExpiredCourse();


                List<CourseDetail> items = await _CourseDetailRepo.GetAllAsync();
                List<CourseDetail> filteredItems = items.Where(x => !x.status).ToList();
                _response.Result = filteredItems;


                _response.StatusCode = HttpStatusCode.OK;



                return Ok(_response);
            }


            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString()
    };
            }
            return _response;
        }
    }
}





