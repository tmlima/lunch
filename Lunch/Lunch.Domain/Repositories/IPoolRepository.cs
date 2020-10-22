using Lunch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Lunch.Domain.Repositories
{
    public interface IPoolRepository : IRepositoryBase<Pool>
    {
        Task<int> Add( DateTime closingTime );
        Task<IEnumerable<Pool>> GetWeekPools( int weekOfYear, CalendarWeekRule calendarWeekRule, DayOfWeek firstDayOfWeek );
        Task<IEnumerable<Pool>> All();
        Task UpdateElectedRestaurant( int poolId, int restaurantId );
    }
}
