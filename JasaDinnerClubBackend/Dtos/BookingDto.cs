public class BookingDto
{
    public int BookingId { get; set; }
    public int DinnerId { get; set; }
    public int AttendeeId { get; set; }
    public string? Request { get; set; }
    public required string AttendeeName { get; set; }
    public required string AttendeeNumber { get; set; }
    public string? DinnerEventName { get; set; }
}