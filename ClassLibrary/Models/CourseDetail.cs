
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace DataAccess.Models
{
    public class CourseDetail
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
               public int Id { get; set; }

             // public int CourseId {get; set;}  
            [Required]
            public string CourseName { get; set; }
            public string CourseDescription { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public int AvailableSeats { get; set; }
            public bool status { get; set; }




    }
}
