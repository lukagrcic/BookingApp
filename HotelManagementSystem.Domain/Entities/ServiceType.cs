using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Domain.Entities
{
    public class ServiceType
    {
        public int ServiceTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal PricePerNight { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
