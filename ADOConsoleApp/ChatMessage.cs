﻿using Newtonsoft.Json;

public class ChatMessage
{
    /// <summary>
    /// Message body
    /// </summary>
    [JsonProperty("body")]
    public ItemBody Body { get; set; }

    /// <summary>
    /// The tenant id
    /// </summary>
    [JsonProperty("tenantId")]
    public string TenantId { get; set; }
}
