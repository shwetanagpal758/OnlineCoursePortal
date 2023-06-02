using System;
using System.Collections.Generic;
using DataAccess.Data;
using DataAccess.Models;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CourseBookingRepository : Repository<CourseBooking>, ICourseBookingRepository
    {
        private readonly ApplicationDbContext _db;
        public CourseBookingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<CourseBooking> UpdateAsync(CourseBooking entity)
        {

            _db.CourseBookings.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}

