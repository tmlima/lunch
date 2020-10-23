using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lunch.Web.API.Models
{
    public class PoolReportModel
    {
        public int Id { get; set; }
        public DateTime ClosingTime { get; set; }
        public string RestaurantElected { get; set; }

    }
}
