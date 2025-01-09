using System.ComponentModel.DataAnnotations;

namespace JasaDinnerClubBackend.Models;

public class Attendee 
{
    public int AttendeeId {get; set;}

    [Required(ErrorMessage = "Name is required please!")]
    public string AttendeeName {get; set;} = string.Empty;

    [Required(ErrorMessage = "Your number is required please!")]
    public string AttendeeNumber {get; set;} = string.Empty;

    // Navigation property: A list of bookings for this dinner event
    public List<Booking>? Bookings { get; set; }

}

