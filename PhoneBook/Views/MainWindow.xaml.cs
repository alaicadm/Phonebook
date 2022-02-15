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
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using PhoneBook.Views;
using PhoneBook.Controller;

namespace PhoneBook
{
    
    public partial class MainWindow : Window 
    {
        
        public CommandController cc = new CommandController();
        public DBController dbcon = new DBController();
        public Helpers Helpers = new Helpers();
        public CollectionView view;

        public MainWindow() 
        {
            InitializeComponent();
            //this.dbcon = dbcon;
            //MessageBox.Show(dbcon.ServerName);
            //MessageBox.Show("v2: "+DBControllerV2.ServerName);
            dbcon.load();
            //view = (CollectionView)CollectionViewSource.GetDefaultView(Helpers.initLV().ItemsSource);
            view = (CollectionView)CollectionViewSource.GetDefaultView(Helpers.initLV().ItemsSource);
            view.Filter = cc.ContactFilter;
            view.SortDescriptions.Add(new SortDescription("UserId", ListSortDirection.Ascending));

        }

        //onclicks
        public void addContact(object sender, EventArgs e) { cc.addContact(sender, e); }
        public void updateContact(object sender, EventArgs e) { cc.updateContact(sender, e); }
        public void deleteContact(object sender, EventArgs e) { cc.deleteContact(sender, e); }
        public void refreshOnClick(object sender, EventArgs e) { cc.refreshOnClick(sender, e); }
        public void searchContact_txtChanged(object sender, TextChangedEventArgs e) { cc.searchContact_txtChanged(sender, e); }
        public void helpOnClick(object sender, EventArgs e) { cc.helpOnClick(sender, e); }


    }
}
