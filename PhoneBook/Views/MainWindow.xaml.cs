using PhoneBook.ViewModel;
using PhoneBook.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhoneBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowVM();

            
        }

        public void refreshOnClick(object sender, EventArgs e)
        {
            UserList.SelectedItem = null;
            fname.Clear();
            mname.Clear();
            lname.Clear();
            gender.SelectedIndex = -1;
            mobile.Clear();
            

        }





        public void addOnClick(object sender, EventArgs e)
        {


            Contact contact = new Contact();
            contact.FirstName = fname.Text;
            contact.MiddleName = mname.Text;
            contact.LastName = lname.Text;
            contact.Gender = gender.Text;
            contact.Mobile = mobile.Text;
            
            
            MessageBox.Show("A contact has been added!", "Phonebook", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
            


        }

    }
}
