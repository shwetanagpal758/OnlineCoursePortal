using AutoMapper;
using DataAccess.Models;
using DataAccess.Models.Dto;

namespace OnlineCoursePortal
{
    public class MappingConfig : Profile
    {

        public MappingConfig()
        {
            CreateMap<CourseDetail, CourseDetailDTO>().ReverseMap();
            CreateMap<CourseBooking, CourseBookingDTO>().ReverseMap();
        }
    }
}
