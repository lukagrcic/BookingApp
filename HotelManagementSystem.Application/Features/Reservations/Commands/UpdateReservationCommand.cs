using HotelManagementSystem.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelManagementSystem.Application.Features.Reservations.Commands
{
    public record UpdateReservationCommand(
        int RoomTypeId,
        int ServiceTypeId,
        [Required] DateTime DateFrom,
        [Required] DateTime DateTo,
        [Range(1, 20)] int NumberOfGuests,
        [StringLength(500)] string? Note
    ) : IRequest<bool>
    {
        public int ReservationId { get; set; }
    }

    public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateReservationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = _unitOfWork.Reservations.GetById(request.ReservationId);
            if (reservation is null)
            {
                return Task.FromResult(false);
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

            reservation.RoomTypeId = request.RoomTypeId;
            reservation.ServiceTypeId = request.ServiceTypeId;
            reservation.DateFrom = request.DateFrom;
            reservation.DateTo = request.DateTo;
            reservation.NumberOfGuests = request.NumberOfGuests;
            reservation.Note = request.Note;
            reservation.TotalPrice = totalPrice;

            _unitOfWork.Reservations.Update(reservation);
            _unitOfWork.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
