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

        private readonly ISKExecutor skExecutor;

        private readonly ILogger<WorkItemController> logger;

        private static readonly string[] WorkStatus = new[]
        {
            "New", "Active", "Resolved"
        };

        public WorkItemController(
        ILogger<WorkItemController> logger,
        IAdoExecutor executor,
        ITeamsExecutor teamsExecutor,
        ISKExecutor skExecutor)
        {
            this.logger = logger;
            this.executor = executor;
            this.teamsExecutor = teamsExecutor;
            this.skExecutor = skExecutor;
        }

        [Route("openBugs")]
        [HttpGet]
        public async Task<IEnumerable<WorkItem>> QueryOpenBugs()
        {
            // TODO:adding settings file
            this.logger.LogInformation("Query Open Bugs");
            return await this.executor.QueryOpenBugs("projectName");
        }

        [Route("runSK")]
        [HttpGet]
        public async Task<String> RunSK()
        {
            this.logger.LogInformation("Run SK");
            return await this.skExecutor.generateContext();
        }

        [Route("summarize")]
        [HttpGet]
        public async Task<String> Summarize()
        {
            // How to break the task in steps?
            // Get work item. Filter out status. Summarize. Format.
            this.logger.LogInformation("Get work items and summarize");
            var bugs = await QueryOpenBugs().ConfigureAwait(false);
            this.logger.LogInformation("How many bugs?");

            return await this.skExecutor.generateReport(bugs);
        }

        [Route("getEssAnswer")]
        [HttpGet]
        public async Task<String> IntroduceEss()
        {
            this.logger.LogInformation("Answer ess related questions");
            // Let frontend post /IntroduceEss with question text in payload
            // Let SK kernel see the payload, generate answers, and returns
            // Related skills are:
            // 1. Collect info (of throughput, capacity, concurrency level)
            // 2. Introduce
            // 3. Suggest desired partition count
            this.logger.LogInformation("How many bugs?");

            return await this.skExecutor.introduceEss("User questions");
        }

        // Deprecated
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