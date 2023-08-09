public interface ITeamsExecutor
{
    /// <summary>
    /// Bla
    /// </summary>
    /// <param name="channel">The name of the teams channel</param>
    /// <param name="teamId">The name of the teamId</param>
    /// <returns>An async task.</returns>
    Task SendToChannel(string channelId, string teamId);

    Task SendToChannelAuth(string channelId);
}