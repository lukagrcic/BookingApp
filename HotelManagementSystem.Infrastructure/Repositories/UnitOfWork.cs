using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagementSystem.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly HotelDbContext _context;


        public IRepository<RoomType>? _roomTypes;

        public IRepository<ServiceType>? _serviceTypes;

        public IUserRepository? _users;

        public IReservationRepository? _reservations;

        public UnitOfWork(HotelDbContext context)
        {
            _context = context;
        }

        public IRepository<RoomType> RoomTypes => _roomTypes ??= new Repository<RoomType>(_context);

        public IRepository<ServiceType> ServiceTypes => _serviceTypes ??= new Repository<ServiceType>(_context);

        public IUserRepository Users => _users ??= new UserRepository(_context);

        public IReservationRepository Reservations => _reservations ??= new ReservationRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
