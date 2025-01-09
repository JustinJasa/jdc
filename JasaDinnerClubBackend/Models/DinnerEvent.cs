using System.ComponentModel.DataAnnotations;

namespace JasaDinnerClubBackend.Models;

public class DinnerEvent
{
    public int DinnerId { get; set; }

    [Required(ErrorMessage = "Dinner Name is Needed")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Date is Needed")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Time is Needed")]
    public TimeSpan Time { get; set; }

    [Required(ErrorMessage = "Please Set Capacity")]
    public int Capacity { get; set; }

    [Required(ErrorMessage = "Please set a short and fun description of the dinner")]
    public string? Description { get; set; }

    // Navigation property: A list of bookings for this dinner event
    public List<Booking>? Bookings { get; set; }
}
