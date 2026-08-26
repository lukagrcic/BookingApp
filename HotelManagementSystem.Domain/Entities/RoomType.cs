using HotelManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagementSystem.Domain.Entities
{
    public class RoomType
    {
        public int RoomTypeId { get; set; }
        public RoomCategory Category { get; set; }
        public decimal PricePerNight { get; set; }
        public string? Description { get; set; }
        public int Capacity { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
