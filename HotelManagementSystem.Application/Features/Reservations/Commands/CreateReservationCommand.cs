using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagementSystem.Application.Features.Reservations.Commands
{
    public record CreateReservationCommand(int UserId, int RoomTypeId, int ServiceTypeId, DateTime DateFrom, DateTime DateTo, int NumberOfGuests, string? Note) : IRequest<int>;

    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateReservationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<int> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var user = _unitOfWork.Users.GetById(request.UserId);
            if (user is null)
            {
                throw new Exception("Korisnik ne postoji");
            }

            var roomType = _unitOfWork.RoomTypes.GetById(request.RoomTypeId);
            if (roomType is null)
            {
                throw new Exception("Tip sobe ne postoji");
            }

            var serviceType = _unitOfWork.ServiceTypes.GetById(request.ServiceTypeId);
            if (serviceType is null)
            {
                throw new Exception("Tip usluge ne postoji");
            }

            if (request.DateTo <= request.DateFrom)
            {
                throw new Exception("Datum odjave mora biti posle datuma prijave");
            }

            if (request.NumberOfGuests > roomType.Capacity)
            {
                throw new Exception($"Izabrani tip sobe prima najviše {roomType.Capacity} gostiju");
            }

            var nights = (request.DateTo.Date - request.DateFrom.Date).Days;
            var totalPrice = (roomType.PricePerNight + serviceType.PricePerNight) * nights;

            var reservation = new Reservation
            {
                UserId = request.UserId,
                RoomTypeId = request.RoomTypeId,
                ServiceTypeId = request.ServiceTypeId,
                DateFrom = request.DateFrom,
                DateTo = request.DateTo,
                NumberOfGuests = request.NumberOfGuests,
                Note = request.Note,
                TotalPrice = totalPrice
            };

            _unitOfWork.Reservations.Add(reservation);
            _unitOfWork.SaveChanges();

            return Task.FromResult(reservation.ReservationId);
        }
    }
}
