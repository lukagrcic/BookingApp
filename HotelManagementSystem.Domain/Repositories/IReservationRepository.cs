using HotelManagementSystem.Domain.Entities;

namespace HotelManagementSystem.Domain.Repositories
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        IEnumerable<Reservation> GetByUserId(int userId);
        IEnumerable<Reservation> GetAllWithDetails();
        Reservation? GetByIdWithDetails(int reservationId);
    }
}
