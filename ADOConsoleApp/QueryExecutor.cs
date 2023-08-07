// nuget:Microsoft.TeamFoundationServer.Client
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

public class QueryExecutor: IQueryExecutor
{
    private readonly Uri uri;
    private readonly string personalAccessToken;

    /// <summary>
    ///     Initializes a new instance of the <see cref="QueryExecutor" /> class.
    /// </summary>
    /// <param name="orgName">
    ///     An organization in Azure DevOps Services. If you don't have one, you can create one for free:
    ///     <see href="https://go.microsoft.com/fwlink/?LinkId=307137" />.
    /// </param>
    /// <param name="personalAccessToken">
    ///     A Personal Access Token, find out how to create one:
    ///     <see href="/azure/devops/organizations/accounts/use-personal-access-tokens-to-authenticate?view=azure-devops" />.
    /// </param>
    public QueryExecutor(string orgName, string personalAccessToken)
    {
        this.uri = new Uri("https://o365exchange.visualstudio.com/");
        this.personalAccessToken = personalAccessToken;
    }

    /// <summary>
    ///     Execute a WIQL (Work Item Query Language) query to return a list of open bugs.
    /// </summary>
    /// <param name="project">The name of your project within your organization.</param>
    /// <returns>A list of <see cref="WorkItem"/> objects representing all the open bugs.</returns>
    public async Task<IList<WorkItem>> QueryOpenBugs(string project)
    {
        Console.WriteLine(this.uri);

        VssConnection vssConnection = new VssConnection(
            this.uri,
            new VssBasicCredential(string.Empty, this.personalAccessToken));

        WorkItemTrackingHttpClient witClient = vssConnection.GetClient<WorkItemTrackingHttpClient>();
        List<WorkItemField> onlineFieldsInAllProcesses = await witClient.GetFieldsAsync().ConfigureAwait(false);

        Console.WriteLine("Items retrieved?");

        // create a wiql object and build our query
        var wiql = new Wiql()
        {
            // NOTE: Even if other columns are specified, only the ID & URL are available in the WorkItemReference
            Query = "Select [Id] " +
                    "From WorkItems " +
                    "Where [Work Item Type] = 'Bug' " +
                    "And [System.AssignedTo] = @Me " +
                    "And [System.AreaPath] = 'O365 Core\\ESS' "+
                    "And [System.State] <> 'Closed' " +
                    "Order By [State] Asc, [Changed Date] Desc",
        };

        // execute the query to get the list of work items in the results
        WorkItemQueryResult result = await witClient.QueryByWiqlAsync(wiql).ConfigureAwait(false);
        var ids = result.WorkItems.Select(item => item.Id).ToArray();

        Console.WriteLine($"{ids.Length} result retrieved");

        // some error handling
        if (ids.Length == 0)
        {
            return Array.Empty<WorkItem>();
        }

        // build a list of the fields we want to see
        var fields = new[] { "url", "System.Id", "System.Description", "System.Title", "System.State" };

        // get work items for the ids found in query
        return await witClient.GetWorkItemsAsync(ids, fields: fields, asOf: result.AsOf).ConfigureAwait(false);
    }

    /// <summary>
    ///     Execute a WIQL (Work Item Query Language) query to print a list of open bugs.
    /// </summary>
    /// <param name="project">The name of your project within your organization.</param>
    /// <returns>An async task.</returns>
    public async Task PrintOpenBugsAsync(string project)
    {
        var workItems = await this.QueryOpenBugs(project).ConfigureAwait(false);

        Console.WriteLine("Query Results: {0} items found", workItems.Count);

        // loop though work items and write to console
        foreach (var workItem in workItems)
        {
            Console.WriteLine(
                "{0}\t{1}\t{2}",
                workItem.Id,
                workItem.Fields["System.Title"],
                workItem.Fields["System.State"]);
        }
    }
}