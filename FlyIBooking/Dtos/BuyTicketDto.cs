using System;
using System.Text.Json.Serialization;

namespace FlyIBooking.Dtos
{
    public sealed class BuyTicketDto
    {
        [JsonPropertyName("accountId")]
        public Guid AccountId { get; set; }
        
        [JsonPropertyName("ticketId")]
        public Guid TicketId { get; set; }
    }
}