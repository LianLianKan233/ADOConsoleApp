using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace AspNetCoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkItemController : ControllerBase
    {
        private readonly IQueryExecutor executor;

        private readonly ILogger<WorkItemController> logger;

        private static readonly string[] WorkStatus = new[]
        {
            "New", "Active", "Resolved"
        };

        public WorkItemController(
        ILogger<WorkItemController> logger,
        IQueryExecutor executor)
        {
            this.logger = logger;
            this.executor = executor;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WorkItem>> Get()
        {
            // TODO:adding settings file
            this.logger.LogInformation("Query Open Bugs");
            return await this.executor.QueryOpenBugs("projectName");
        }
    }
}