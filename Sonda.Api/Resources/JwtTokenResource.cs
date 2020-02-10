using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sonda.Api.Resources
{
    public class JwtTokenResource
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
