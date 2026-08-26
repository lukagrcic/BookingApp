using HotelManagementSystem.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagementSystem.Application.Features.ServiceTypes.Commands
{
    public record DeleteServiceTypeCommand(int ServiceTypeId) : IRequest<bool>;

    public class DeleteServiceTypeCommandHandler : IRequestHandler<DeleteServiceTypeCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteServiceTypeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeleteServiceTypeCommand request, CancellationToken cancellationToken)
        {
            var serviceType = _unitOfWork.ServiceTypes.GetById(request.ServiceTypeId);

            if (serviceType is null) return Task.FromResult(false);

            _unitOfWork.ServiceTypes.Delete(serviceType);
            _unitOfWork.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
