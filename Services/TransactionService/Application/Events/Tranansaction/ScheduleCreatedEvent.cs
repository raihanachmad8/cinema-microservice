namespace TransactionService.Appication.Events.User
{
    public class ScheduleCreatedEvent
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int StudioId { get; set; }
        public decimal TicketPrice { get; set; }
        public DateTime StartDatetime { get; set; }
        public DateTime EndDatetime { get; set; }
    }
}