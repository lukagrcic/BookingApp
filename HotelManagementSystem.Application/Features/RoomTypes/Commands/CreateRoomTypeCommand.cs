using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Domain.Enums;
using HotelManagementSystem.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelManagementSystem.Application.Features.RoomTypes.Commands
{
    public record CreateRoomTypeCommand(
        RoomCategory Category,
        [Range(0, 10000)] decimal PricePerNight,
        [StringLength(300)] string? Description,
        [Range(1, 20)] int Capacity
    ) : IRequest<int>;


    public class CreateRoomTypeCommandHandler : IRequestHandler<CreateRoomTypeCommand, int>
    {

        private readonly IUnitOfWork _unitOfWork;

        public CreateRoomTypeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<int> Handle(CreateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var roomType = new RoomType
            {
                Category = request.Category,
                PricePerNight = request.PricePerNight,
                Description = request.Description,
                Capacity = request.Capacity
            };

            _unitOfWork.RoomTypes.Add(roomType);
            _unitOfWork.SaveChanges();

            return Task.FromResult(roomType.RoomTypeId);

        }
    }

}
