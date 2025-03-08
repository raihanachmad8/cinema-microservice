namespace TransactionService.Application.DTOs.Responses
{
    public record TransactionResponse
    {
        public int Id { get; set; }  
        public int TicketId { get; set; }  
        public int UserId { get; set; }  
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; } 
        public DateTime TransactionDate { get; set; }  
        public TicketResponse? Ticket { get; set; }
        public decimal TotalAmount { get; set; }  
    }
}
