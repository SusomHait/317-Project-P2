using System.ComponentModel;
using CSC317PassManagerP2Starter.Modules.Controllers;
using CSC317PassManagerP2Starter.Modules.Models;

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

        // like the password, changes to platform and user name shoudn't be stored until save is pressed
        private string temp_platform, temp_userName, temp_pass;

        public string Platform
        {
            get 
            {
                if (!_editing)
                {
                    temp_platform = _pass.PlatformName;
                }
                return temp_platform;
            }
            set
            {
                temp_platform = value;
                RefreshRow();
            }
        }

        public string PlatformUserName
        {
            get 
            {
                if (!_editing)
                {
                    temp_userName = _pass.UserId;
                }
                return temp_userName;
            }
            set
            {
                temp_userName = value;
                RefreshRow();
            }
        }

        public string PlatformPassword
        {
            get
            {
                if (_isVisible)
                {
                    if (!_editing)
                    {
                        User? curr_user = LoginController.CurrentUser;
                        temp_pass = PasswordCrypto.Decrypt(_pass.PasswordText, Tuple.Create(curr_user.Key, curr_user.IV));
                    }
                    return temp_pass;
                }
                return "<hidden>";
            }
            set
            {
                // Note that this ONLY changes the password stored in the row.
                // The password should not be committed to the model data until save is clicked.
                
                // due to the above constraint, encrption has been moved to SavePassword()
                temp_pass = value;
                RefreshRow();
            }
        }

        public int PasswordID { get { return _pass.ID; } }

        public bool IsShown
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                
                // change editing stage when visibility set false
                if (!_isVisible)
                {
                    _editing = false;
                }

                RefreshRow();
            }
        }

        public bool Editing
        {
            get { return _editing; }
            set
            {
                _editing = value;
                RefreshRow();
            }
        }

        // added an extra parameter to change save button label
        // needed to fix display bug (caused when search is used while password editing is active)
        public string edit_save_label
        {
            get 
            {
                if (_editing) return "Save";
                else return "Edit";
            }
        }

        private void RefreshRow()
        {
            OnPropertyChanged(nameof(Platform));
            OnPropertyChanged(nameof(PlatformUserName));
            OnPropertyChanged(nameof(PlatformPassword));
            OnPropertyChanged(nameof(IsShown));
            OnPropertyChanged(nameof(Editing));

            // added extra parameter edit_save_lable
            OnPropertyChanged(nameof(edit_save_label));
        }

        // called when save is pressed 
        public void SavePassword() {
            User? curr_user = LoginController.CurrentUser;

            PasswordController.UpdatePassword(new PasswordModel
            {
                ID = _pass.ID,
                PlatformName = temp_platform,
                UserId = temp_userName,
                // encrypt pass for storage
                PasswordText = PasswordCrypto.Encrypt(temp_pass, Tuple.Create(curr_user.Key, curr_user.IV))
            }); 
        }
    }
}
