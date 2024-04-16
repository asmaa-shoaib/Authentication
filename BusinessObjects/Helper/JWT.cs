using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Helper
{
    public class JWT
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double TokenExpiryTimeInHour { get; set; }
        public string key { get; set; }
       
    }
}
