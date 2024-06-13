using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ApplicationLayer.DTOs.Auth.Login
{
    public class LoginRequestDto
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
