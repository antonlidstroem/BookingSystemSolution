public class BookingDto
{
    public int BookingId { get; set; }
    public int RoomId { get; set; }
    public int CustomerId { get; set; }

    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}
