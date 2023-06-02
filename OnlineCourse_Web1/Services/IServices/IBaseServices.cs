using OnlineCourse_Web1.Models;

namespace OnlineCourse_Web1.Services.IServices
{
    public interface IBaseServices
    {
        APIResponse responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
