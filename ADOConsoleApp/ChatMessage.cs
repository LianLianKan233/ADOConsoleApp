using Newtonsoft.Json;

public class ChatMessage
{
    /// <summary>
    /// The group id
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }

    /// <summary>
    /// The tenant id
    /// </summary>
    [JsonProperty("tenantId")]
    public string TenantId { get; set; }
}
