namespace CSC317PassManagerP2Starter.Modules.Views;

public partial class AddPasswordView : ContentPage
{
    Color baseColorHighlight;
    bool generatedPassword;
    string generated_user_pass;

    public AddPasswordView()
    {
        InitializeComponent();
        //Stores the original color of the text boxes. Used to revert a text box back
        //to its original color if it was "highlighted" during input validation.
        baseColorHighlight = (Color)Application.Current.Resources["ErrorEntryHighlightBG"];
        
        //Determines if a password was generated at least once.
        generatedPassword = false;
    }

    private void ButtonCancel(object sender, EventArgs e)
    {
        //Called when the Cancel button is clicked.
        App.Current.MainPage = new PasswordListView();
    }

    private async void ButtonSubmitExisting(object sender, EventArgs e)
    {
        //Called when the Submit button is clicked for a password manually entered.
        string platform = txtNewPlatform.Text;
        string user_name = txtNewPlatform.Text;
        string password = txtNewPassword.Text;
        string verify = txtNewPasswordVerify.Text;

        if (platform == null || platform.Length == 0)
        {
            lblErrorExistingPassword.IsVisible = true;
            lblErrorExistingPassword.Text = "Platform Field Must be Filled";
            return;
        }
        if (user_name == null || user_name.Length == 0)
        {
            lblErrorExistingPassword.IsVisible = true;
            lblErrorExistingPassword.Text = "Username Field Must be Filled";
            return;
        }
        if (password == null || password.Length == 0)
        {
            lblErrorExistingPassword.IsVisible = true;
            lblErrorExistingPassword.Text = "Password Field Must be Filled";
            return;
        }
        if (verify == null || verify.Length == 0)
        {
            lblErrorExistingPassword.IsVisible = true;
            lblErrorExistingPassword.Text = "Password Verifcation Field Must be Filled";
            return;
        }
        if (password != verify)
        {
            lblErrorExistingPassword.IsVisible = true;
            lblErrorExistingPassword.Text = "Password and Password Verification Do Not Match";
            return;
        }
            
        App.PasswordController.AddPassword(platform, user_name, password);
        App.Current.MainPage = new PasswordListView();
    }

    private void ButtonSubmitGenerated(object sender, EventArgs e)
    {
        //Called when the submit button for a Generated password is clicked.
        string platform = txtNewPlatform.Text;
        string user_name = txtNewPlatform.Text;

        if (platform == null || platform.Length == 0)
        {
            lblErrorGeneratedPassword.IsVisible = true;
            lblErrorGeneratedPassword.Text = "Platform Field Must be Filled";
            return;
        }
        if (user_name == null || user_name.Length == 0)
        {
            lblErrorGeneratedPassword.IsVisible = true;
            lblErrorGeneratedPassword.Text = "Username Field Must be Filled";
            return;
        }
        if (!generatedPassword)
        {
            lblErrorGeneratedPassword.IsVisible = true;
            lblErrorGeneratedPassword.Text = "No Password has been Generated";
            return;
        }

        App.PasswordController.AddPassword(platform, user_name, generated_user_pass);
        App.Current.MainPage = new PasswordListView();
    }

    private void ButtonGeneratePassword(object sender, EventArgs e)
    {
        string? symbols = "";
        int minLen = Convert.ToInt32(sprPassLength.Value); // hard coded min len of 6 in XAML

        if (chkSymbols.IsChecked)
        {
            symbols = txtReqSymbols.Text;
        }

        //Called when the Generate Password button is clicked.
        generated_user_pass = PasswordGeneration.BuildPassword(chkUpperLetter.IsChecked, chkDigit.IsChecked, symbols, minLen);
        lblGenPassword.Text = generated_user_pass; 
        generatedPassword = true;
    }
}