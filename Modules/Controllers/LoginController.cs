using CSC317PassManagerP2Starter.Modules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC317PassManagerP2Starter.Modules.Controllers
{
    public enum AuthenticationError { NONE, INVALIDUSERNAME, INVALIDPASSWORD }
    public class LoginController
    {

        /*
         * This class is incomplete.  Fill in the method definitions below.
         */
        private User _user = new User();
        private bool _loggedIn = false;

        public User? CurrentUser
        {
            get
            {
                //Returns a copy of the user data.  Currently returning null.
                return null;
            }
        }

        public AuthenticationError Authenticate(string username, string password)
        {
            //determines whether the inputted username/password matches the stored
            //username/password.  currently returns a NONE error status.
            return AuthenticationError.NONE;
        }
    }

}
