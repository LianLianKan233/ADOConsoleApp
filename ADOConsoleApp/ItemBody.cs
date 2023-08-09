using Newtonsoft.Json;

public class ItemBody
{
    /// <summary>
    /// Message body
    /// </summary>
    [JsonProperty("Content")]
    public string Content { get; set; }

    /// <summary>
    /// The tenant id
    /// </summary>
    [JsonProperty("tenantId")]
    public string Sth { get; set; }
}
