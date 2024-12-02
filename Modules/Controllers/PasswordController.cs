using System.Collections.ObjectModel;
using CSC317PassManagerP2Starter.Modules.Models;
using CSC317PassManagerP2Starter.Modules.Views;

namespace CSC317PassManagerP2Starter.Modules.Controllers
{
    public class PasswordController
    {
        public static List<PasswordModel> _passwords = new List<PasswordModel>(); // made static
        private int counter = 0; // changed start to 0
        private int curr_ID = 1; // keep a separate counter for ID


        public void PopulatePasswordView(ObservableCollection<PasswordRow> source, string search_criteria = "")
        {
            source.Clear();

            foreach (PasswordModel pass in _passwords) 
            { 
                // check for filter
                if (search_criteria.Length > 0)
                {
                    if (!pass.PlatformName.ToLower().Contains(search_criteria) && !pass.UserId.ToLower().Contains(search_criteria))
                    {
                        continue;
                    }
                }
                source.Add(new PasswordRow(pass)); 
            }
        }

        //CRUD operations for the password list.
        public void AddPassword(string platform, string username, string password)
        {
            User? curr_user = LoginController.CurrentUser;

            _passwords.Add(new PasswordModel
            {
                ID = curr_ID++, // store current ID value then increment (postfix behavior)
                PlatformName = platform,
                UserId = username,
                PasswordText = PasswordCrypto.Encrypt(password, Tuple.Create(curr_user.Key, curr_user.IV))
            });
            
            counter++;
        }

        // changed to static
        public static PasswordModel? GetPassword(int ID)
        {
            foreach (PasswordModel pass in _passwords)
            {
                if (pass.ID == ID) return pass;
            }

            return null;
        }

        public static bool UpdatePassword(PasswordModel changes)
        {
            PasswordModel pass = GetPassword(changes.ID);
            if (pass is not null)
            {
                pass.PlatformName = changes.PlatformName;
                pass.UserId = changes.UserId;
                pass.PasswordText = changes.PasswordText;

                return true;
            }

            return false;
        }

        public bool RemovePassword(int ID)
        {
            PasswordModel pass = GetPassword(ID);
            if (pass is not null)
            {
                _passwords.Remove(pass);
                return true;
            }

            counter--;
            return false;
        }

        // track if dummy data has been added to main list
        // needed to prevent dummy data duplication on page reload
        bool addedTestPasswords = false;
        public void GenTestPasswords()
        {
            // add dummy data to main password list once
            if (!addedTestPasswords)
            {
                AddPassword("Google", "test@gmail.com", "1234");
                AddPassword("Facebook", "temp_user", "2345");
                AddPassword("Amazon", "frequentbuyer", "bezos1964");
                AddPassword("Apple", "fruit", "jobs21");

                addedTestPasswords = true;
            }
        }
    }
}
