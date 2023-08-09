using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Newtonsoft.Json.Linq;
using System.Net.Http.Formatting;
using System.Text.Json;

public class TeamsExecutor : ITeamsExecutor
{
    private readonly Uri uri;
    private readonly string personalAccessToken;
    static readonly HttpClient client = new HttpClient();

    public TeamsExecutor()
    {
    }

    /// <summary>
    /// SendToChannel
    /// </summary>
    /// <param name="project">The name of your project within your organization.</param>
    /// <returns>A list of <see cref="WorkItem"/> objects representing all the open bugs.</returns>
    public async Task SendToChannel(string channelId, string teamId)
    {
        //  POST https://graph.microsoft.com/v1.0/teams/fbe2bf47-16c8-47cf-b4a5-4b9b187c508b/channels/19:4a95f7d8db4c4e7fae857bcebe0623e6@thread.tacv2/messages
        //  Content - type: application / json
        var requestBody = new ChatMessage()
        {
            Body = new ItemBody
            {
                Content = "Hello World",
            },
        };

        var targetUri = $"https://graph.microsoft.com/v1.0/teams/{teamId}/channels/{channelId}/messages";
        using HttpResponseMessage response = await client.PostAsync(
            targetUri,
            JsonSerializer.Serialize(requestBody));
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        // Above three lines can be replaced with new helper method below
        // string responseBody = await client.GetStringAsync(uri);

        Console.WriteLine(responseBody);
    }

    /// <summary>
    ///     Execute a WIQL (Work Item Query Language) query to return a list of open bugs.
    /// </summary>
    /// <param name="project">The name of your project within your organization.</param>
    /// <returns>A list of <see cref="WorkItem"/> objects representing all the open bugs.</returns>
    public async Task SendToChannelSDK(string channelName)
    {
                //    // Code snippets are only available for the latest version. Current version is 5.x
                //    var graphClient = new GraphService(requestAdapter);

                //    var requestBody = new ChatMessage
                //    {
                //        Body = new ItemBody
                //        {
                //            Content = "Hello World",
                //        },
                //    };
                //    var result = await graphClient.Teams["{team-id}"].Channels["{channel-id}"].Messages.PostAsync(requestBody);
                //}
    }
}