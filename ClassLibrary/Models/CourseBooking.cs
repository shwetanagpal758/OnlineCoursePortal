using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class CourseBooking
    {
        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int Id { get; set; }
/*
        [ForeignKey("CourseDetail")]
        public int CourseId { get; set; }
        public CourseDetail CourseDetail { get; set; }*/
        public string CourseBooking_Name { get; set; }
        
        public DateTime BookingDate { get; set; }
        public int NumberOfUsers { get; set; }


    }
}
