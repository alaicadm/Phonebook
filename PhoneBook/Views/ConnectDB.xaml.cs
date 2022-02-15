using PhoneBook.Controller;
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

namespace PhoneBook.Views
{
    /// <summary>
    /// Interaction logic for ConnectDB.xaml
    /// </summary>
    public partial class ConnectDB : Window
    {
        //public CommandController cmc = new CommandController();
        //public DBController dbc = new DBController();
        //DBController dbc = new DBController();  //runs directly if instantiated here.. if on the method there's something wrong
        //MainWindow mainWindow = new MainWindow();
        //Helpers h = new Helpers();

        public ConnectDB()
        {

            InitializeComponent();
            
        }

  
        public void onClickConnect(object item, EventArgs e)
        {         
           
            DBController.ServerName = sname.Text;
            DBController.DbName = dbname.Text;
            DBController.UserName = dbuname.Text;
            DBController.Password = dbpass.Password;

           
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
       
    }
}
