using HotelManagementSystem.Application.Common;
using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelManagementSystem.Application.Features.Users.Commands
{
    public record CreateUserCommand(
        [Required, StringLength(50)] string FirstName,
        [Required, StringLength(50)] string LastName,
        [Required, EmailAddress] string Email,
        [Required, Phone] string PhoneNumber,
        [Required, StringLength(30, MinimumLength = 3)] string Username,
        [Required, MinLength(6)] string Password
    ) : IRequest<int>;

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existing = _unitOfWork.Users.GetByUsername(request.Username);
            if (existing is not null)
            {
                throw new Exception("Korisničko ime je zauzeto");
            }

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Username = request.Username,
                PasswordHash = PasswordHasher.Hash(request.Password)
            };

            _unitOfWork.Users.Add(user);
            _unitOfWork.SaveChanges();

            return Task.FromResult(user.UserId);
        }
    }
}
