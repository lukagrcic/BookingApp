using HotelManagementSystem.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagementSystem.Application.Features.ServiceTypes.Queries
{
    public record GetServiceTypeByIdQuery(int ServiceTypeId) : IRequest<ServiceTypeDto?>;

    public record ServiceTypeDto(int ServiceTypeId, string Name, string? Description, decimal PricePerNight);

    public class GetServiceTypeByIdQueryHandler : IRequestHandler<GetServiceTypeByIdQuery, ServiceTypeDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetServiceTypeByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<ServiceTypeDto?> Handle(GetServiceTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var serviceType = _unitOfWork.ServiceTypes.GetById(request.ServiceTypeId);

            if (serviceType is null)
            {
                return Task.FromResult<ServiceTypeDto?>(null);
            }

            var dto = new ServiceTypeDto(serviceType.ServiceTypeId, serviceType.Name, serviceType.Description, serviceType.PricePerNight);

            return Task.FromResult<ServiceTypeDto?>(dto);
        }
    }
}
