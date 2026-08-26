using HotelManagementSystem.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagementSystem.Application.Features.Users.Commands
{
    public record DeleteUserCommand(int UserId) : IRequest<bool>;

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = _unitOfWork.Users.GetById(request.UserId);

            if (user is null) return Task.FromResult(false);

            _unitOfWork.Users.Delete(user);
            _unitOfWork.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
