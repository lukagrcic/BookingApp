using System;
using HotelManagementSystem.Application.Common;
using HotelManagementSystem.Domain.Repositories;
using MediatR;

namespace HotelManagementSystem.Application.Features.Auth.Commands
{
    public record LoginCommand(string Username, string Password) : IRequest<string>;

    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenGenerator _tokenGenerator;

        public LoginCommandHandler(IUnitOfWork unitOfWork, ITokenGenerator tokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _tokenGenerator = tokenGenerator;
        }

        public Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = _unitOfWork.Users.GetByUsername(request.Username);

            if (user is null || !PasswordHasher.Verify(request.Password, user.PasswordHash))
            {
                throw new Exception("Pogrešno korisničko ime ili lozinka");
            }

            return Task.FromResult(_tokenGenerator.GenerateToken(user));
        }
    }
}
