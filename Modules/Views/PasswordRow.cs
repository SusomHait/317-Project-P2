using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSC317PassManagerP2Starter.Modules.Models;



/* This module contains the class definition for Password Row.
 * 
 * The methods are missing their bodies.  Fill in the method definitions below.
 * 
 */
namespace CSC317PassManagerP2Starter.Modules.Views
{
    public class PasswordRow : BindableObject, INotifyPropertyChanged
    {
        private PasswordModel _pass;
        private bool _isVisible = false;
        private bool _editing = false;

        public PasswordRow(PasswordModel source)
        {
            _pass = source;
        }

        //Create your Binding Properties here, which should reflect the front-end bindings.
        //See the example of "Platform" below.
        public string Platform
        {
            get
            {
                //complete getter for Platform.  currenly returns an empty string. 
                return "";
            }
            set
            {
                //complete setter for Platform.

                //This needs to be called for updating the binding when
                //the platform name is edited.  Leave here.
                RefreshRow();
            }
        }

        public string PlatformUserName
        {
            get
            {
                //Complete getter for User Name.
                return "";
            }
            set
            {
                //complete setter for User Name.
                RefreshRow();
            }
        }

        public string PlatformPassword
        {
            get
            {
               //complete getter for Password.  Currenly returns "hidden."
               //This should return the actual password is the Show toggle
               //is true.
               //note that the password should be decrypted using the user's
               //encryption key before being shown.
               return "<hidden>";
            }
            set
            {
                //complete setter for password.  Note that this ONLY changes the password
                //stored in the row.  The password should not be committed to the model
                //data until save is clicked.


                RefreshRow();
            }
        }

        public int PasswordID
        {
            get
            {
                //complete getter for the pass ID.  Is binded to the edit/save/copy/delete buttons.
                //currently returns -1;
                return -1;
            }
        }

        public bool IsShown
        {
            get
            {
                //complete getter for IsShown, which is binded to the Show Password
                //toggle/switch.
                return false;
            }
            set
            {
                //complete setter for IsShown.
                RefreshRow();
            }
        }

        public bool Editing
        {
            get
            {
                //Complete getter for Editing, which is toggled when the "edit/save" button
                //is clicked.  
                return false;
            }
            set
            {
                //Complete setter for Editing.
                RefreshRow();
            }
        }


        //This is called when a bound property is changed on the front-end.  Causes the 
        //front-end to update the collection view.
        private void RefreshRow()
        {
            OnPropertyChanged(nameof(Platform));
            OnPropertyChanged(nameof(PlatformUserName));
            OnPropertyChanged(nameof(PlatformPassword));
            OnPropertyChanged(nameof(IsShown));
            OnPropertyChanged(nameof(Editing));
        }

        public void SavePassword()
        {
            //Is called when the "save" button is clicked.  Saves the changes to the
            //password to the model data.
        }
    }

}
