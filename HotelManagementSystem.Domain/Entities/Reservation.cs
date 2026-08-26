using System;

namespace HotelManagementSystem.Domain.Entities
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int NumberOfGuests { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Note { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; } = null!;

        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; } = null!;
    }
}
