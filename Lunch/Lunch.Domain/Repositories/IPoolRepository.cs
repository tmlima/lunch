using Lunch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Lunch.Domain.Repositories
{
    public interface IPoolRepository : IRepositoryBase<Pool>
    {
        int Add( DateTime closingTime );
        IEnumerable<Pool> GetWeekPools( int weekOfYear, CalendarWeekRule calendarWeekRule, DayOfWeek firstDayOfWeek );
        IEnumerable<Pool> All();
        void UpdateElectedRestaurant( int poolId, int restaurantId );
    }
}
