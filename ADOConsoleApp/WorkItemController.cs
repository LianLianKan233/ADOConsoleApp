using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace AspNetCoreAPI.Controllers
{
    [ApiController]
    public class WorkItemController : ControllerBase
    {
        private readonly IAdoExecutor executor;

        private readonly ITeamsExecutor teamsExecutor;

        private readonly ILogger<WorkItemController> logger;

        private static readonly string[] WorkStatus = new[]
        {
            "New", "Active", "Resolved"
        };

        public WorkItemController(
        ILogger<WorkItemController> logger,
        IAdoExecutor executor,
        ITeamsExecutor teamsExecutor)
        {
            this.logger = logger;
            this.executor = executor;
            this.teamsExecutor = teamsExecutor;
        }

        [Route("openBugs")]
        [HttpGet]
        public async Task<IEnumerable<WorkItem>> QueryOpenBugs()
        {
            // TODO:adding settings file
            this.logger.LogInformation("Query Open Bugs");
            return await this.executor.QueryOpenBugs("projectName");
        }

        [Route("sendToChannel")]
        [HttpGet]
        public async Task SendToChannel()
        {
            // POST /teams/{team-id}/channels/{channel-id}/ messages /{ message - id
            this.logger.LogInformation("Send To Channel");
            await this.teamsExecutor.SendToChannelAuth("randomCannel");
        }
    }
}