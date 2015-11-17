using Postgaarden.Connection.Sqlite;
using Postgaarden.Crud.Users;
using Postgaarden.Model.Users;
using PostgaardenGui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PostgaardenLogin
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private LoginHandler handler;

        public Login()
        {
            InitializeComponent();

            //null will be SqliteDatabaseConnection when the database is used.
            var crud = new SqliteUserCrud(null);
            handler = new LoginHandler(crud);
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            User user = new User(usernameTextbox.Text, passwordTextbox.Password);
            if (handler.Login(user))
            {
                new MainWindow().Show();
                this.Close();
            }
            else
            {
                failTextBlock.Text = "Username or Password is not correct.";
            }
        }
    }
}
