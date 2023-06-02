﻿using DataAccess.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface ICourseBookingRepository : IRepository<CourseBooking>
    {
        Task<CourseBooking> UpdateAsync(CourseBooking entity);
    }
}
