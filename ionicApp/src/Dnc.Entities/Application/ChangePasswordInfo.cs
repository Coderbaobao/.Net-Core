using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities.Application
{
    public class ChangePasswordInfo
    {
        public Guid userid { get; set; }
        public string PasswordOld { get; set; }
        public string PasswordNew { get; set; }
    }
}
