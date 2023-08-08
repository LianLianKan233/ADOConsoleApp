public interface ITeamsExecutor
{
    /// <summary>
    /// Bla
    /// </summary>
    /// <param name="channel">The name of the teams channel</param>
    /// <returns>An async task.</returns>
    Task SendToChannel(string channel);
}