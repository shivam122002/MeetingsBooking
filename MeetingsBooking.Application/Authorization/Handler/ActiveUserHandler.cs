using MeetingsBooking.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsBooking.Application.Authorization.Handler
{
    public class ActiveUserHandler
    {
        private readonly IUserRepository _userRepository;
        public ActiveUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    }
}
