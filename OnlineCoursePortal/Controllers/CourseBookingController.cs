
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
using System.Net;

namespace OnlineCoursePortal.Controllers
{
    [Route("api/CourseBooking")]
    [ApiController]

    public class CourseBookingController : ControllerBase
    {

        private readonly ICourseBookingRepository _CourseBookingRepo;
        private readonly ICourseDetailRepository _CourseDetailRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public CourseBookingController(ICourseBookingRepository CourseBookingRepo, IMapper mapper, ICourseDetailRepository CourseDetailRepo)
        {
            _CourseDetailRepo = CourseDetailRepo;
            _CourseBookingRepo = CourseBookingRepo;
            _mapper = mapper;
            this._response = new();

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetCourseBookings()

        {
            try
            {

                IEnumerable<CourseBooking> coursebookingList = await _CourseBookingRepo.GetAllAsync();

                _response.Result = _mapper.Map<List<CourseBookingDTO>>(coursebookingList);
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

        public async Task<ActionResult<APIResponse>> GetCourseBooking(int id)
        {
            try
            {

                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var coursebooking = await _CourseBookingRepo.GetAsync(u => u.Id == id);
                if (coursebooking == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result =  _mapper.Map<CourseBookingDTO>(coursebooking);
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<APIResponse>> CreateCourseBooking([FromBody] CourseBookingDTO CoursebookingDTO)

        {
            try
            {

                if (CoursebookingDTO == null)
                {
                    return BadRequest(CoursebookingDTO);
                }


                CourseBooking coursebooking = _mapper.Map<CourseBooking>(CoursebookingDTO);



                await _CourseBookingRepo.CreateAsync(coursebooking);
                //  _response.Result = _mapper.Map<CourseBookingDTO>(coursebooking);
                _response.Result = coursebooking;
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


       /* [Authorize(Roles = "admin")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<APIResponse>> DeleteCourseBooking(int id)
        {
            try
            {


                if (id == 0)
                {
                    return BadRequest();
                }

                var coursebooking = await _CourseBookingRepo.GetAsync(u => u.Id == id);
                if (coursebooking == null)
                {
                    return NotFound();
                }
                await _CourseBookingRepo.RemoveAsync(coursebooking);
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
        }*/
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<APIResponse>> UpdateCourseBooking(int id, [FromBody] CourseBookingDTO coursebookingDTO)
        {
            try
            {
                if (coursebookingDTO == null || id != coursebookingDTO.Id)
                {
                    return BadRequest();
                }
                CourseBooking model = _mapper.Map<CourseBooking>(coursebookingDTO);

                await _CourseBookingRepo.UpdateAsync(model);
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
        [HttpPost("newcreate")]
        public async Task<ActionResult<APIResponse>> Createnew([FromBody] CourseBookingDTO createDTO)
        {
            try
            {

                if (await _CourseBookingRepo.GetAsync(u => u.CourseBooking_Name == createDTO.CourseBooking_Name) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Customername already taken");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest();
                }

                if (await _CourseBookingRepo.GetAsync(u => u.Id == createDTO.Id) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Id is invalid");
                    return BadRequest(ModelState);
                }
                var Courseupdate = await _CourseDetailRepo.GetAsync(u => u.Id == createDTO.Id);
                int available = Courseupdate.AvailableSeats;
                int Coursedone = available - createDTO.NumberOfUsers;
                if (Coursedone < 0)
                {
                    ModelState.AddModelError("ErrorMessages", "Insufficient Users");
                    return BadRequest(ModelState);
                    //some exception
                }
                Courseupdate.AvailableSeats = Coursedone;
                await _CourseDetailRepo.UpdateAsync(Courseupdate);
                //save event
                CourseBooking booking = _mapper.Map<CourseBooking>(createDTO);

                await _CourseBookingRepo.CreateAsync(booking);


                _response.Result = _mapper.Map<CourseBooking>(booking);
                _response.StatusCode = HttpStatusCode.OK;
                return CreatedAtRoute("GetBooking", new { id = booking.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpDelete("{id:int}")]

        public async Task<ActionResult<APIResponse>> DeletecourseBooking(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }


                var booking = await _CourseBookingRepo.GetAsync(u => u.Id == id);
                int bookedusers = booking.NumberOfUsers;
                int courseid = booking.Id;
                var course = await _CourseDetailRepo.GetAsync(u => u.Id == id);
                int available = course.AvailableSeats;
                int updatedseats = available + bookedusers;
                course.AvailableSeats = updatedseats;
                await _CourseDetailRepo.UpdateAsync(course);

                if (booking == null)
                {
                    return NotFound();
                }


                await _CourseBookingRepo.RemoveAsync(booking);
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
    }
}





