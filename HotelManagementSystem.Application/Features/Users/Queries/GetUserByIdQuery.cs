using HotelManagementSystem.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagementSystem.Application.Features.Users.Queries
{
    public record GetUserByIdQuery(int UserId) : IRequest<UserDto?>;

    public record UserDto(int UserId, string FirstName, string LastName, string Email, string PhoneNumber, string Username);

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = _unitOfWork.Users.GetById(request.UserId);

            if (user is null)
            {
                return Task.FromResult<UserDto?>(null);
            }

            var dto = new UserDto(user.UserId, user.FirstName, user.LastName, user.Email, user.PhoneNumber, user.Username);

            return Task.FromResult<UserDto?>(dto);
        }
    }
}
