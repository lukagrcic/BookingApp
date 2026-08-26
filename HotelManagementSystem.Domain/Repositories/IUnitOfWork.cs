using HotelManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagementSystem.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<RoomType> RoomTypes { get; }
        IRepository<ServiceType> ServiceTypes { get; }
        IUserRepository Users { get; }
        IReservationRepository Reservations { get; }

        void SaveChanges();
    }
}
