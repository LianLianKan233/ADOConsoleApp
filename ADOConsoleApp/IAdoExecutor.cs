// nuget:Microsoft.TeamFoundationServer.Client
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

public interface IAdoExecutor
{
    /// <summary>
    ///     Execute a WIQL (Work Item Query Language) query to return a list of open bugs.
    /// </summary>
    /// <param name="project">The name of your project within your organization.</param>
    /// <returns>A list of <see cref="WorkItem"/> objects representing all the open bugs.</returns>
    Task<IList<WorkItem>> QueryOpenBugs(string project);

    /// <summary>
    ///     Execute a WIQL (Work Item Query Language) query to print a list of open bugs.
    /// </summary>
    /// <param name="project">The name of your project within your organization.</param>
    /// <returns>An async task.</returns>
    Task PrintOpenBugsAsync(string project);
}