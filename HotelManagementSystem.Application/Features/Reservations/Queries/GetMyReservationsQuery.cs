using HotelManagementSystem.Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace HotelManagementSystem.Application.Features.Reservations.Queries
{
    public record GetMyReservationsQuery(int UserId) : IRequest<List<ReservationDto>>;

    public class GetMyReservationsQueryHandler : IRequestHandler<GetMyReservationsQuery, List<ReservationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMyReservationsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<List<ReservationDto>> Handle(GetMyReservationsQuery request, CancellationToken cancellationToken)
        {
            var reservations = _unitOfWork.Reservations.GetByUserId(request.UserId)
                .Select(r => new ReservationDto(
                    r.ReservationId,
                    r.DateFrom,
                    r.DateTo,
                    r.CreatedAt,
                    r.NumberOfGuests,
                    r.TotalPrice,
                    r.Note,
                    r.UserId,
                    r.User != null ? r.User.Username : "",
                    r.RoomTypeId,
                    r.RoomType.Category,
                    r.ServiceTypeId,
                    r.ServiceType.Name))
                .ToList();

            return Task.FromResult(reservations);
        }
    }
}