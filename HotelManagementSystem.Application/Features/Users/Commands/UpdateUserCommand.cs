using HotelManagementSystem.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelManagementSystem.Application.Features.Users.Commands
{
    public record UpdateUserCommand(
        [Required, StringLength(50)] string FirstName,
        [Required, StringLength(50)] string LastName,
        [Required, EmailAddress] string Email,
        [Required, Phone] string PhoneNumber,
        [Required, StringLength(30, MinimumLength = 3)] string Username
    ) : IRequest<bool>
    {
        public int UserId { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _unitOfWork.Users.GetById(request.UserId);
            if (user is null)
            {
                return Task.FromResult(false);
            }

            var existing = _unitOfWork.Users.GetByUsername(request.Username);
            if (existing is not null && existing.UserId != request.UserId)
            {
                throw new Exception("Korisničko ime je zauzeto");
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            user.Username = request.Username;

            _unitOfWork.Users.Update(user);
            _unitOfWork.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
