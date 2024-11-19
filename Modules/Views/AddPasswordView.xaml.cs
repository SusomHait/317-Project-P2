namespace CSC317PassManagerP2Starter.Modules.Views;

public partial class AddPasswordView : ContentPage
{
    Color baseColorHighlight;
    bool generatedPassword;

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
    }

    private void ButtonSubmitExisting(object sender, EventArgs e)
    {
       //Called when the Submit button is clicked for a password manually
       //entered.  (i.e., existing password).
    }

    private void ButtonSubmitGenerated(object sender, EventArgs e)
    {
        //Called when the submit button for a Generated password is clicked.
    }

    private void ButtonGeneratePassword(object sender, EventArgs e)
    {
       //Called when the Generate Password button is clicked.
    }
}