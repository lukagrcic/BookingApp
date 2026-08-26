using HotelManagementSystem.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagementSystem.Application.Features.Reservations.Queries
{
    public record GetAllReservationsQuery() : IRequest<List<ReservationDto>>;

    public class GetAllReservationsQueryHandler : IRequestHandler<GetAllReservationsQuery, List<ReservationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllReservationsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<List<ReservationDto>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
        {
            var reservations = _unitOfWork.Reservations.GetAllWithDetails()
                .Select(r => new ReservationDto(
                    r.ReservationId,
                    r.DateFrom,
                    r.DateTo,
                    r.CreatedAt,
                    r.NumberOfGuests,
                    r.TotalPrice,
                    r.Note,
                    r.UserId,
                    r.User.Username,
                    r.RoomTypeId,
                    r.RoomType.Category,
                    r.ServiceTypeId,
                    r.ServiceType.Name))
                .ToList();

            return Task.FromResult(reservations);
        }
    }
}
