namespace BookingSystem.DTO.DTO
{
    public class BookingDto
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
