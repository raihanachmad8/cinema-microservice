using System.Text.Json.Serialization;

namespace TransactionService.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentStatus
    {
        Pending = 1,    
        Successful = 2, 
        Failed = 3      
    }
}