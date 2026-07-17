using MeetingsBooking.Shared.Dtos.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsBooking.Application.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request,CancellationToken cancellationToken);

        Task<LoginResponseDto> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken);

        Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request, CancellationToken cancellationToken);  
    }
}
