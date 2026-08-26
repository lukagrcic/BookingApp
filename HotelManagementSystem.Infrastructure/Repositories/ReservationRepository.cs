using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagementSystem.Infrastructure.Repositories
{
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        public ReservationRepository(HotelDbContext context) : base(context)
        {
        }

        public IEnumerable<Reservation> GetByUserId(int userId)
        {
            return _dbSet
                .Include(r => r.RoomType)
                .Include(r => r.ServiceType)
                .Where(r => r.UserId == userId)
                .ToList();
        }

        public IEnumerable<Reservation> GetAllWithDetails()
        {
            return _dbSet
                .Include(r => r.User)
                .Include(r => r.RoomType)
                .Include(r => r.ServiceType)
                .ToList();
        }

        public Reservation? GetByIdWithDetails(int reservationId)
        {
            return _dbSet
                .Include(r => r.User)
                .Include(r => r.RoomType)
                .Include(r => r.ServiceType)
                .FirstOrDefault(r => r.ReservationId == reservationId);
        }
    }
}
