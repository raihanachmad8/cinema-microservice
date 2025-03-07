namespace TicketService.Appication.Events.User;

public class TransactionCreatedPaymentEvent
{
    public int Id { get; set; }
    public int SeatId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; }
}