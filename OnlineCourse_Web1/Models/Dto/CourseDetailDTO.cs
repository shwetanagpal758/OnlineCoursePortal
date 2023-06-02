using System.ComponentModel.DataAnnotations;

namespace OnlineCourse_Web1.Models.Dto
{
    public class CourseDetailDTO
    {
        [Required]
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int AvailableSeats { get; set; }
    }
}
