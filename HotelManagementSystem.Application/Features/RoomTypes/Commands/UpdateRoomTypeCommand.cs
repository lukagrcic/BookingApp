using HotelManagementSystem.Domain.Enums;
using HotelManagementSystem.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelManagementSystem.Application.Features.RoomTypes.Commands
{
    public record UpdateRoomTypeCommand(
        RoomCategory Category,
        [Range(0, 10000)] decimal PricePerNight,
        [StringLength(300)] string? Description,
        [Range(1, 20)] int Capacity
    ) : IRequest<bool>
    {
        public int RoomTypeId { get; set; }
    }

    public class UpdateRoomTypeCommandHandler : IRequestHandler<UpdateRoomTypeCommand, bool>
    {

        private readonly IUnitOfWork _unitOfWork;

        public UpdateRoomTypeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(UpdateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var roomType = _unitOfWork.RoomTypes.GetById(request.RoomTypeId);
            if(roomType is null)
            {
                return Task.FromResult(false);
            }

            roomType.Category = request.Category;
            roomType.PricePerNight = request.PricePerNight;
            roomType.Description = request.Description;
            roomType.Capacity = request.Capacity;

            _unitOfWork.RoomTypes.Update(roomType);
            _unitOfWork.SaveChanges();

            return Task.FromResult(true);                
        }
    }
}
