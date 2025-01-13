public class AttendeeDto
{
    public int AttendeeId {get; set;}
    public string AttendeeName { get; set; } = string.Empty;
    public string AttendeeNumber { get; set; } = string.Empty;
    // Only include booking information that's relevant for the client
    public List<BookingDto>? Bookings { get; set; }
}