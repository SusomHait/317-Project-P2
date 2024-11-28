namespace CSC317PassManagerP2Starter.Modules.Views;

public partial class LoginView : ContentPage
{
    public LoginView()
    {
        InitializeComponent();
    }

    private void ProcessLogin(object sender, EventArgs e)
    {
        //Complete Process Login Functionality. Called by Submit Button
        Controllers.AuthenticationError return_err;
        Models.User return_usr;

        (return_err, return_usr) = App.LoginController.Authenticate(txtUserName.Text, txtPassword.Text);

        switch (return_err)
        {
            case Controllers.AuthenticationError.NONE:
                App.Current.MainPage = new PasswordListView();
                break;
            case Controllers.AuthenticationError.INVALIDUSERNAME:
                ShowErrorMessage("Invalid User Name");
                break;
            case Controllers.AuthenticationError.INVALIDPASSWORD:
                ShowErrorMessage("Invalid Password");
                break;
            default: break;
        }
    }

    private void ShowErrorMessage(string message)
    {
        //Complete ShowError Message functionality.  Supports ProcessLogin.
        lblError.IsVisible = true;
        lblError.Text = message;
    }
}