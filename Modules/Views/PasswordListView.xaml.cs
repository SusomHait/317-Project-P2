using System.Collections.ObjectModel;

namespace CSC317PassManagerP2Starter.Modules.Views;

public partial class PasswordListView : ContentPage
{
    private ObservableCollection<PasswordRow> _rows = new ObservableCollection<PasswordRow>();

    public PasswordListView()
    {
        InitializeComponent();

        App.PasswordController.GenTestPasswords();
        App.PasswordController.PopulatePasswordView(_rows);
        collPasswords.ItemsSource = _rows;
    }

    private void TextSearchBar(object sender, TextChangedEventArgs e)
    {
        //Is binded to the Search Bar.  Calls PopulatePasswords from the Password Controller.
        //to update the list of shown passwords.

        App.PasswordController.PopulatePasswordView(_rows, (sender as Entry).Text);
    }

    private void CopyPassToClipboard(object sender, EventArgs e)
    {
        //Is called when Copy is clicked.  Looks up the password by its ID
        //and copies it to the clipboard.

        //Example of how to get the ID of the password selected.
        int ID = Convert.ToInt32((sender as Button).CommandParameter);

        foreach (PasswordRow pass in _rows)
        {
            if (pass.PasswordID == ID)
            {
                string pass_text = pass.PlatformPassword;
                MainThread.BeginInvokeOnMainThread(() => Clipboard.Default.SetTextAsync(pass_text));
            }
        }
    }

    private void EditPassword(object sender, EventArgs e)
    {
        //Called when Edit/Save is clicked.
        int ID = Convert.ToInt32((sender as Button).CommandParameter);

        // find password row -> read its editing state -> handle event accordingly
        foreach (PasswordRow pass in _rows)
        {
            if (pass.PasswordID == ID)
            {
                if (!pass.Editing) // password in editing mode
                {
                    pass.Editing = true;
                }
                else // password not in editing mode
                {
                    pass.SavePassword();
                    pass.Editing = false;
                }
            }
        }
    }

    private void DeletePassword(object sender, EventArgs e)
    {
        int ID = Convert.ToInt32((sender as Button).CommandParameter);

        foreach (PasswordRow pass in _rows)
        {
            if (pass.PasswordID == ID)
            {
                
                _rows.Remove(pass);
                break;
            }
        }

        App.PasswordController.RemovePassword(ID);
    }

    private void ButtonAddPassword(object sender, EventArgs e)
    {
        //Called when Add Password is clicked.  
        App.Current.MainPage = new AddPasswordView();
    }
}