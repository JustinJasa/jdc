public class DinnerEventDto
{
    public int DinnerId { get; set; }
    public string Name { get; set; } = string.Empty;
    // public DateTime Date { get; set; }
    // public TimeSpan Time { get; set; }
    public int Capacity { get; set; }
    public string? Description { get; set; }
    // public List<BookingDto>? Bookings { get; set; }
}