

//using OnlineCourse_Web1.Models.Dto;

using DataAccess.Models;

namespace OnlineCourse_Web1.Services.IServices
{
    public interface ICourseDetailService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> DeleteAsync<T>(int id);
        Task<T> CreateAsync<T>(CourseDetail dto);

        Task<T> UpdateAsync<T>(CourseDetail dto);
        Task<T> UpdateStatusAsync<T>(int id);
        Task<T> GetAllTrueAsync<T>();
        Task<T> GetAllFalseAsync<T>();

    }
}
