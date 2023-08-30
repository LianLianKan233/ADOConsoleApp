using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

public interface ISKExecutor
{
    Task<String> generateContext();

    Task<String> generateReport(IEnumerable<WorkItem> input);

    Task<String> introduceEss(String input);
}
