using System.Collections.ObjectModel;
using CSC317PassManagerP2Starter.Modules.Models;
using CSC317PassManagerP2Starter.Modules.Views;

namespace CSC317PassManagerP2Starter.Modules.Controllers
{
    public class PasswordController
    {
        public static List<PasswordModel> _passwords = new List<PasswordModel>(); // made static
        private int counter = 0; // changed start to 0
        
        // added more variables
        private int curr_ID = 1; // keep a separate counter for ID
        private ObservableCollection<PasswordRow>? display_ref;


        public void PopulatePasswordView(ObservableCollection<PasswordRow> source, string search_criteria = "")
        {
            foreach (PasswordModel pass in _passwords) 
            { 
                source.Add(new PasswordRow(pass)); 
            }
            
            display_ref = source;
        }

        //CRUD operations for the password list.
        public void AddPassword(string platform, string username, string password)
        {
            User? curr_user = LoginController.CurrentUser;
            PasswordModel temp = new PasswordModel
            {
                ID = curr_ID++, // store current ID value then increment (postfix behavior)
                PlatformName = platform,
                UserId = username,
                PasswordText = PasswordCrypto.Encrypt(password, Tuple.Create(curr_user.Key, curr_user.IV))
            };

            display_ref.Add(new PasswordRow(temp)); // update observable collection to populate changes to front end
            _passwords.Add(temp); // also add to _passwords for update and get functions
            
            counter++;
        }

        public PasswordModel? GetPassword(int ID)
        {
            foreach (PasswordModel pass in _passwords)
            {
                if (pass.ID == ID) return pass;
            }

            return null;
        }

        public static bool UpdatePassword(PasswordModel changes)
        {
            foreach (PasswordModel pass in _passwords)
            {
                if (pass.ID == changes.ID)
                {
                    pass.PlatformName = changes.PlatformName;
                    pass.UserId = changes.UserId;
                    pass.PasswordText = changes.PasswordText;
                    
                    return true;
                }
            }

            return false;
        }

        public bool RemovePassword(int ID)
        {
            for (int i = 0; i < counter; i++)
            {
                if (display_ref[i].PasswordID == ID)
                {
                    display_ref.RemoveAt(counter); // remove from observable collection to update front end
                    _passwords.RemoveAt(counter); // remove from password list as well
                    counter--; // decrement counter for removed value
                    
                    return true;
                }
            }

            return false;
        }

        public void GenTestPasswords()
        {
            AddPassword("Google", "test@gmail.com", "1234");
            AddPassword("Facebook", "temp_user", "2345");
            AddPassword("Amazon", "frequentbuyer", "bezos1964");
            AddPassword("Apply", "fruit", "jobs21");
        }
    }
}
