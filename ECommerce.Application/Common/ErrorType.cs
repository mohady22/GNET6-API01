using System.Text.Json.Serialization;

namespace ECommerce.Application.Common
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ErrorType
    {
        Failure = 0,
        Validation = 1,
        NotFound = 2,
        Conflict = 3,
        UnAuthorized = 4,
        Forbidden = 5,
        InvalidCredentails = 6,
    }
}