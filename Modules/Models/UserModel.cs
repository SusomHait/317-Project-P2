using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC317PassManagerP2Starter.Modules.Models
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; } = "";
        public string Lastname { get; set; } = "";
        public string UserName { get; set; } = "";
        public byte[]? PasswordHash { get; set; }
        public required byte[] Key { get; set; }
        public required byte[] IV { get; set; }

    }
}
