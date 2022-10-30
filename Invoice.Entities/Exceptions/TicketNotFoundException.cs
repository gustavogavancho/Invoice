namespace Invoice.Entities.Exceptions;

[Serializable]
public class TicketNotFoundException : NotFoundException
{
    public TicketNotFoundException(string ticketNumber) : base($"The ticket with number: {ticketNumber} doesn't exist in the database.")
    {
    }
}