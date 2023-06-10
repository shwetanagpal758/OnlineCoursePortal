using DataAccess.Models;
using DataAccess.Models.Dto;

namespace OnlineCourse_Web1.Services.IServices
{
    public interface IBookingService
    {

        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
       
        Task<T> UpdateAsync<T>(CourseBooking dto);
        Task<T> DeleteAsync<T>(int id);
        Task<T> CreateAsync<T>(CourseBookingDTO dto);
    }
}
