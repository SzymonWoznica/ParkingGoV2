using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.JWT
{
    public class JwtSettings : IJwtSettings
    {
        public string SecurityKey { get; set; }
    }
}
