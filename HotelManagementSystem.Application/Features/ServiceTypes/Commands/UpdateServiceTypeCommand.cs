using HotelManagementSystem.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagementSystem.Application.Features.ServiceTypes.Commands
{
    public record UpdateServiceTypeCommand(string Name, string? Description, decimal PricePerNight) : IRequest<bool>
    {
        public int ServiceTypeId { get; set; }
    }

    public class UpdateServiceTypeCommandHandler : IRequestHandler<UpdateServiceTypeCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateServiceTypeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(UpdateServiceTypeCommand request, CancellationToken cancellationToken)
        {
            var serviceType = _unitOfWork.ServiceTypes.GetById(request.ServiceTypeId);
            if (serviceType is null)
            {
                return Task.FromResult(false);
            }

            serviceType.Name = request.Name;
            serviceType.Description = request.Description;
            serviceType.PricePerNight = request.PricePerNight;

            _unitOfWork.ServiceTypes.Update(serviceType);
            _unitOfWork.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
