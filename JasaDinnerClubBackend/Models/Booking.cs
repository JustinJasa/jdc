using System.ComponentModel.DataAnnotations;

namespace JasaDinnerClubBackend.Models;
public class Booking
{

    [Required]
    public int BookingId {get; set;}

    [Required]
    public int DinnerId {get; set;}
    
    public required DinnerEvent DinnerEvent { get; set; } // Navigation property

    [Required]
    public int AttendeeId {get; set;}

    public required Attendee Attendee { get; set; } // Navigation property

    public string? Request {get; set;}


}