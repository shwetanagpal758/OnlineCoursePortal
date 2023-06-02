using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Dto
{
    public class CourseBookingDTO
    {
        [Required]
        public int Id { get; set; }
        /* [Required]
         public string CourseName { get; set; }*/
       /* [Required]
        public int CourseDetailId { get; set; }*/
        [Required]
        public string CourseBooking_Name { get; set; }

        public DateTime BookingDate { get; set; }
        public int NumberOfUsers { get; set; }
        //public int CourseId { get; set; }
    }
}
