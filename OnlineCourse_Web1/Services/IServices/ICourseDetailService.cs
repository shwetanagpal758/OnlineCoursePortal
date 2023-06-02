

//using OnlineCourse_Web1.Models.Dto;

using DataAccess.Models.Dto;

namespace OnlineCourse_Web1.Services.IServices
{
    public interface ICourseDetailService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> DeleteAsync<T>(int id);
        Task<T> CreateAsync<T>(CourseDetailDTO dto);

        Task<T> UpdateAsync<T>(CourseDetailDTO dto);
       
    }
}
