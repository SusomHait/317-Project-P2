using CSC317PassManagerP2Starter.Modules.Models;

namespace CSC317PassManagerP2Starter.Modules.Controllers
{
    public enum AuthenticationError { NONE, INVALIDUSERNAME, INVALIDPASSWORD }
    public class LoginController
    {
        // overide _user with dummy data
        private static User _user = new User
        {
            ID = -1,
            FirstName = "John",
            Lastname = "Doe",
            UserName = "test",
            PasswordHash = PasswordCrypto.GetHash("ab1234"),
            Key = PasswordCrypto.GenKey().Item1,
            IV = PasswordCrypto.GenKey().Item2
        };
        private static bool _loggedIn = false;

        public static User? CurrentUser
        {
            get //Returns a copy of the user data.
            {
                if (_loggedIn == true)
                {
                    return new User { 
                        FirstName = _user.FirstName,
                        Lastname = _user.Lastname,
                        Key = _user.Key,
                        IV = _user.IV
                    };
                }
                else return null; 
            }
        }

        public (AuthenticationError, User?) Authenticate(string username, string password)
        {
            if (username == _user.UserName)
            {
                if (PasswordCrypto.CompareBytes(PasswordCrypto.GetHash(password), _user.PasswordHash))
                {
                    _loggedIn = true;
                    return (AuthenticationError.NONE, CurrentUser);
                }
                else return (AuthenticationError.INVALIDPASSWORD, CurrentUser);
            }
            else return (AuthenticationError.INVALIDUSERNAME, CurrentUser);
        }
    }

}
