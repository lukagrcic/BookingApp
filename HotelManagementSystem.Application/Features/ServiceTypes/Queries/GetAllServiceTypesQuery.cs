using HotelManagementSystem.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagementSystem.Application.Features.ServiceTypes.Queries
{
    public record GetAllServiceTypesQuery() : IRequest<List<ServiceTypeDto>>;

    public class GetAllServiceTypesQueryHandler : IRequestHandler<GetAllServiceTypesQuery, List<ServiceTypeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllServiceTypesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<List<ServiceTypeDto>> Handle(GetAllServiceTypesQuery request, CancellationToken cancellationToken)
        {
            var serviceTypes = _unitOfWork.ServiceTypes.GetAll()
                .Select(st => new ServiceTypeDto(st.ServiceTypeId, st.Name, st.Description, st.PricePerNight))
                .ToList();

            return Task.FromResult(serviceTypes);
        }
    }
}
