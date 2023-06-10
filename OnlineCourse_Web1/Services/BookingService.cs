using DataAccess;
using DataAccess.Models;
using DataAccess.Models.Dto;
using OnlineCourse_Web1.Models;
using OnlineCourse_Web1.Services.IServices;

namespace OnlineCourse_Web1.Services
{
   
        public class BookingService : BaseService, IBookingService

        {
            private readonly IHttpClientFactory _clientFactory;
            private string CourseUrl;

            public BookingService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
            {
                _clientFactory = clientFactory;
            CourseUrl = configuration.GetValue<string>("ServiceUrls:OnlineCourseAPI");

            }

           

            public Task<T> DeleteAsync<T>(int id)
            {
                return SendAsync<T>(new APIRequest()
                {
                    ApiType = SD.ApiType.DELETE,
                    Url = CourseUrl + "/api/CourseBooking/" + id

                });
            }

            public Task<T> GetAllAsync<T>()
            {
                return SendAsync<T>(new APIRequest()
                {
                    ApiType = SD.ApiType.GET,
                    Url = CourseUrl + "/api/CourseBooking"
                });
            }

            public Task<T> GetAsync<T>(int id)
            {
                return SendAsync<T>(new APIRequest()
                {
                    ApiType = SD.ApiType.GET,
                    Url = CourseUrl + "/api/CourseBooking/" + id

                });
            }

            public Task<T> UpdateAsync<T>(CourseBooking dto)
            {
                return SendAsync<T>(new APIRequest()
                {
                    ApiType = SD.ApiType.PUT,
                    Data = dto,
                    Url = CourseUrl + "/api/CourseBooking/" + dto.Id

                });
            }

            public Task<T> CreateAsync<T>(CourseBookingDTO dto)
            {
                return SendAsync<T>(new APIRequest()
                {
                    ApiType = SD.ApiType.POST,
                    Data = dto,
                    Url = CourseUrl + "/api/CourseBooking"


                });
            }
        }
    }

