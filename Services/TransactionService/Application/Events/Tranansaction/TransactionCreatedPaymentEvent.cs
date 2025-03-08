namespace TransactionService.Appication.Events.User;

public class TransactionCreatedPaymentEvent
{

    public int Id { get; set; }
    public int TicketId { get; set; }
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; }
}