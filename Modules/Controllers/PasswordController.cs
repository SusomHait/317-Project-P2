using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSC317PassManagerP2Starter.Modules.Controllers;
using CSC317PassManagerP2Starter.Modules.Models;
using CSC317PassManagerP2Starter.Modules.Views;

namespace CSC317PassManagerP2Starter.Modules.Controllers
{
    public class PasswordController
    {
        //Stores a list of sample passwords for the test user.
        public List<PasswordModel> _passwords = new List<PasswordModel>();
        private int counter = 1;


        /*
         * The following functions need to be completed.
         */
        //Used to copy the passwords over to the Row Binders.
        public void PopulatePasswordView(ObservableCollection<PasswordRow> source, string search_criteria = "")
        {
            //Complete definition of PopulatePasswordView here.
        }

        //CRUD operations for the password list.
        public void AddPassword(string platform, string username, string password)
        {
            //Complete definition of AddPassword here.
        }

        public PasswordModel? GetPassword(int ID)
        {
           //Complete definition of GetPassword here.

            return null;
        }

        public bool UpdatePassword(PasswordModel changes)
        {
           //Complete definition of Update Password here.

            return false;
        }

        public bool RemovePassword(int ID)
        {
           //Complete definition of Remove Password here.

            return false;
        }

        public void GenTestPasswords()
        {
            //Generate a set of random passwords for the test user.
            //Called in Password List Page.
        }
    }
}
