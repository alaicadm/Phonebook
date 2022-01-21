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
            fname.Clear();
            mname.Clear();
            lname.Clear();
            gender.SelectedIndex=-1;
            mobile.Clear();

        }

   
        

       
        public void addOnClick(object sender, EventArgs e)
        {


            Contact c = new Contact();
            c.FirstName = fname.Text;
            c.MiddleName = mname.Text;
            c.LastName = lname.Text;
            c.Gender = gender.Text;
            c.Mobile= mobile.Text;


            
        }

    }
}
