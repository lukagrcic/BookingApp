using HotelManagementSystem.Domain.Enums;
using HotelManagementSystem.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagementSystem.Application.Features.Reservations.Queries
{
    public record GetReservationByIdQuery(int ReservationId) : IRequest<ReservationDto?>;

    public record ReservationDto(
        int ReservationId,
        DateTime DateFrom,
        DateTime DateTo,
        DateTime CreatedAt,
        int NumberOfGuests,
        decimal TotalPrice,
        string? Note,
        int UserId,
        string Username,
        int RoomTypeId,
        RoomCategory RoomTypeCategory,
        int ServiceTypeId,
        string ServiceTypeName);

    public class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, ReservationDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetReservationByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<ReservationDto?> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            var reservation = _unitOfWork.Reservations.GetByIdWithDetails(request.ReservationId);

            if (reservation is null)
            {
                return Task.FromResult<ReservationDto?>(null);
            }

            var dto = new ReservationDto(
                reservation.ReservationId,
                reservation.DateFrom,
                reservation.DateTo,
                reservation.CreatedAt,
                reservation.NumberOfGuests,
                reservation.TotalPrice,
                reservation.Note,
                reservation.UserId,
                reservation.User.Username,
                reservation.RoomTypeId,
                reservation.RoomType.Category,
                reservation.ServiceTypeId,
                reservation.ServiceType.Name);

            return Task.FromResult<ReservationDto?>(dto);
        }
    }
}
