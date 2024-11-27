using CSC317PassManagerP2Starter.Modules.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC317PassManagerP2Starter.Modules.Models
{
    public class PasswordModel
    {
        public int ID { get; set; }
        public string UserId { get; set; } = "";
        public string PlatformName { get; set; } = "";

        public byte[]? PasswordText;

    }
}
