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
    public class CourseDetailRepository : Repository<CourseDetail>, ICourseDetailRepository
    {
        private readonly ApplicationDbContext _db;
        public CourseDetailRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
      
        public async Task<CourseDetail> UpdateAsync(CourseDetail entity)
        {
           
           _db.CourseDetails.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
