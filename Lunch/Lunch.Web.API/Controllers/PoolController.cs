using Lunch.Domain.Entities;
using Lunch.Domain.Interfaces;
using Lunch.Web.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Lunch.Web.API.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class PoolController : ControllerBase
    {
        IPoolService poolService;

        public PoolController( IPoolService poolService )
        {
            this.poolService = poolService;
        }

        [HttpGet]
        public IList<PoolReportModel> Get()
        {
            IEnumerable<Pool> pools = poolService.GetAllAsync().Result;
            IList<PoolReportModel> poolReports = new List<PoolReportModel>();
            foreach ( Pool p in pools )
            {
                poolReports.Add( new PoolReportModel()
                {
                    Id = p.Id,
                    ClosingTime = p.ClosingTime,
                    RestaurantElected = p.RestaurantElected.Name
                } );
            }

            return poolReports;
        }
    }
}
