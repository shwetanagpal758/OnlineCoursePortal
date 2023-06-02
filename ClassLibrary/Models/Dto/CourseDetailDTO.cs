using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Dto
{
    public class CourseDetailDTO
    {
        [Required]
        public int Id { get; set; }
        //public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int AvailableSeats { get; set; }
    }
}
