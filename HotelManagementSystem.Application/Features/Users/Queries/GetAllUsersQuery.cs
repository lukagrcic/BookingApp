using HotelManagementSystem.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagementSystem.Application.Features.Users.Queries
{
    public record GetAllUsersQuery() : IRequest<List<UserDto>>;

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUsersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _unitOfWork.Users.GetAll()
                .Select(u => new UserDto(u.UserId, u.FirstName, u.LastName, u.Email, u.PhoneNumber, u.Username))
                .ToList();

            return Task.FromResult(users);
        }
    }
}
