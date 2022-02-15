using PhoneBook.Models;
using PhoneBook.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PhoneBook.Controller
{
    public class CommandController
    {
        public DBController dbcon = new DBController();
        public Helpers Helpers = new Helpers();
        public CollectionView view;
        public CommandController()
        {
            //MainWindow mainWindow = new MainWindow();
            //mainWindow.Show();
        }

        /*public void OpenWindow(object sender, EventArgs e)
        {
            //ConnectDB cndb = new ConnectDB();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }*/


        public bool ContactFilter(object item) //filters contact
        {
            if (String.IsNullOrEmpty(Helpers.search().Text)) { return true; }
            else
            {
                return ((item as Contact).FirstName.IndexOf(Helpers.search().Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || (item as Contact).MiddleName.IndexOf(Helpers.search().Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || (item as Contact).LastName.IndexOf(Helpers.search().Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || (item as Contact).Mobile.IndexOf(Helpers.search().Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || (item as Contact).Gender.IndexOf(Helpers.search().Text, StringComparison.OrdinalIgnoreCase) >= 0
                    );
            }
        }

        public void searchContact_txtChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Helpers.initLV().ItemsSource).Refresh();
        }

        public bool fieldChecker(object sender, EventArgs e) //checks if a textbox/combobox is null or not
        {
            bool res = true;
            
            TextBox[] textBoxes = { Helpers.initTB()[0], Helpers.initTB()[1], Helpers.initTB()[2], Helpers.initTB()[3] };

            ComboBox gender = Helpers.initCB();
            
            foreach (TextBox i in textBoxes) { if (string.IsNullOrEmpty(i.Text)) { res = false; } }
            if (string.IsNullOrEmpty(gender.Text)) { res = false; }
            int outParse;
            if (!int.TryParse(Helpers.initTB()[3].Text, out outParse)) { res = false; }

            return res;

        }

        public void refreshOnClick(object sender, EventArgs e) //to refresh textboxes and unselect the select item on listview 
        {
            Helpers.initLV().SelectedItem = null;
            Helpers.initTB()[0].Clear();
            Helpers.initTB()[1].Clear();
            Helpers.initTB()[2].Clear();
            Helpers.initCB().SelectedIndex = -1;
            Helpers.initTB()[3].Clear();

        }

        public void addContact(object sender, EventArgs e)
        {
            bool check = fieldChecker(sender, e);

            if (check == true)
            {
                dbcon.insert();
                refreshOnClick(sender, e);
            }
            else { MessageBox.Show("An empty/incorrect field was recognized, check your inputs!", "FAILED", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK); }

        }

        public void updateContact(object sender, EventArgs e)
        {
            bool check = fieldChecker(sender, e);

            if (check == true)
            {
                dbcon.update();
                refreshOnClick(sender, e);
            }
            else { MessageBox.Show("An empty/incorrect field was recognized, check your inputs!", "FAILED", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK); }

        }

        public void deleteContact(object sender, EventArgs e)
        {
            dbcon.delete();
            refreshOnClick(sender, e);
        }

        public void helpOnClick(object sender, EventArgs e)
        {
            FAQs helpWindow = new FAQs();
            helpWindow.Show();

        }


    }
}
