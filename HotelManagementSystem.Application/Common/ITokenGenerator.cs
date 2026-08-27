using HotelManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagementSystem.Application.Common
{
    public interface ITokenGenerator
    {
        string GenerateToken(User user);
    }
}
