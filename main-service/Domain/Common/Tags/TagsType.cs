using System.Text.Json.Serialization;

namespace Domain.Common.Tags;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TagsType
{
    Hard,
    Soft
}