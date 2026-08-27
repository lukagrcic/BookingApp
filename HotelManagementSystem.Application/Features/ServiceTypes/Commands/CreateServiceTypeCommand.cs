using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelManagementSystem.Application.Features.ServiceTypes.Commands
{
    public record CreateServiceTypeCommand(
        [Required, StringLength(50)] string Name,
        [StringLength(300)] string? Description,
        [Range(0, 10000)] decimal PricePerNight
    ) : IRequest<int>;

    public class CreateServiceTypeCommandHandler : IRequestHandler<CreateServiceTypeCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateServiceTypeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<int> Handle(CreateServiceTypeCommand request, CancellationToken cancellationToken)
        {
            var serviceType = new ServiceType
            {
                Name = request.Name,
                Description = request.Description,
                PricePerNight = request.PricePerNight
            };

            _unitOfWork.ServiceTypes.Add(serviceType);
            _unitOfWork.SaveChanges();

            return Task.FromResult(serviceType.ServiceTypeId);
        }
    }
}
