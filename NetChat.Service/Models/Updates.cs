using System;
using Newtonsoft.Json;

/// <summary>
/// Summary description for Class1
/// </summary>
[Serializable]
public class Updates
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("object")]
    public object Object { get; set; }

    [JsonProperty("group_id")]
    public long GroupId { get; set; }
}
