

using DataAccess;
using DataAccess.Models;
using Newtonsoft.Json.Linq;
using OnlineCourse_Web1.Models;

using OnlineCourse_Web1.Services.IServices;

namespace OnlineCourse_Web1.Services
{
    public class CourseDetailService : BaseService, ICourseDetailService
    {

        private readonly IHttpClientFactory _clientFactory;
        private string coursedetailUrl;
        public CourseDetailService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)

        {
            _clientFactory = clientFactory;
            coursedetailUrl = configuration.GetValue<string>("ServiceUrls:OnlineCourseAPI");
        }



        public Task<T> CreateAsync<T>(CourseDetail dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = coursedetailUrl + "/api/CourseDetail"
               

            });
        }



        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
               
                Url = coursedetailUrl + "/api/CourseDetail/"+id
             

            });
        }





        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,

                Url = coursedetailUrl + "/api/CourseDetail"


            });
        }



        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,

                Url = coursedetailUrl + "/api/CourseDetail/" + id
             

            });
        }

        public Task<T> UpdateAsync<T>(CourseDetail dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = coursedetailUrl + "/api/CourseDetail/" + dto.Id
              

            });
        }
        public Task<T> UpdateStatusAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = coursedetailUrl + "/api/CourseDetail/" + id

            });
        }
        public Task<T> GetAllTrueAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = coursedetailUrl + "/api/CourseDetail/GetStatusTrue/status=true"

            });
        }
        public Task<T> GetAllFalseAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = coursedetailUrl + "/api/CourseDetail/GetStatusFalse/status=false"

            });
        }


    }
}
