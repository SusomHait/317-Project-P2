using CSC317PassManagerP2Starter.Modules.Controllers;
using CSC317PassManagerP2Starter.Modules.Views;

namespace CSC317PassManagerP2Starter
{
    public partial class App : Application
    {
        public static LoginController LoginController = new LoginController();
        public static PasswordController PasswordController = new PasswordController();

        public App()
        {
            InitializeComponent();

            MainPage = new LoginView();
        }
    }
}
