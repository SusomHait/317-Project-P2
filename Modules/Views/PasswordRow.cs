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
            loadPassData();
        }

        // like the password, changes to platform and user name shoudn't be stored until save is pressed
        private string temp_platform;
        private string temp_userName;
        private string temp_pass;

        private void loadPassData()
        {
            temp_platform = _pass.PlatformName;
            temp_userName = _pass.UserId;

            User? curr_user = LoginController.CurrentUser;
            temp_pass = PasswordCrypto.Decrypt(_pass.PasswordText, Tuple.Create(curr_user.Key, curr_user.IV));
        }

        public string Platform
        {
            get { return temp_platform; }
            set
            {
                temp_platform = value;
                RefreshRow();
            }
        }

        public string PlatformUserName
        {
            get { return temp_userName; }
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
                // decryption moved to utility function loadPassData()
                if (_isVisible) return temp_pass;
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
                RefreshRow();
            }
        }

        public bool Editing
        {
            get { return _editing; }
            set
            {
                _editing = value;

                // if editing mode is left, revert changes to default
                if (_editing == false) loadPassData();

                RefreshRow();
            }
        }

        private void RefreshRow()
        {
            OnPropertyChanged(nameof(Platform));
            OnPropertyChanged(nameof(PlatformUserName));
            OnPropertyChanged(nameof(PlatformPassword));
            OnPropertyChanged(nameof(IsShown));
            OnPropertyChanged(nameof(Editing));
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
