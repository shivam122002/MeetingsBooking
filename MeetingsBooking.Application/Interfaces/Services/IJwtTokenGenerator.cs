using MeetingsBooking.Domain;
using MeetingsBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsBooking.Application.Interfaces.Services
{
    public interface IJwtTokenGenerator
    {
        AccessTokenResult GenerateAccessToken(User user);

        RefreshToken GenerateRefreshToken();
    }
}
