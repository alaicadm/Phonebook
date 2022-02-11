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
        public Helpers h = new Helpers();
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
            if (String.IsNullOrEmpty(h.search().Text)) { return true; }
            else
            {
                return ((item as Contact).FirstName.IndexOf(h.search().Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || (item as Contact).MiddleName.IndexOf(h.search().Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || (item as Contact).LastName.IndexOf(h.search().Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || (item as Contact).Mobile.IndexOf(h.search().Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || (item as Contact).Gender.IndexOf(h.search().Text, StringComparison.OrdinalIgnoreCase) >= 0
                    );
            }
        }

        public void searchContact_txtChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(h.initLV().ItemsSource).Refresh();
        }

        public bool fieldChecker(object sender, EventArgs e) //checks if a textbox/combobox is null or not
        {
            bool res = true;
            
            TextBox[] textBoxes = { h.initTB()[0], h.initTB()[1], h.initTB()[2], h.initTB()[3] };

            ComboBox gender = h.initCB();
            
            foreach (TextBox i in textBoxes) { if (string.IsNullOrEmpty(i.Text)) { res = false; } }
            if (string.IsNullOrEmpty(gender.Text)) { res = false; }
            int outParse;
            if (!int.TryParse(h.initTB()[3].Text, out outParse)) { res = false; }

            return res;

        }

        public void refreshOnClick(object sender, EventArgs e) //to refresh textboxes and unselect the select item on listview 
        {
            h.initLV().SelectedItem = null;
            h.initTB()[0].Clear();
            h.initTB()[1].Clear();
            h.initTB()[2].Clear();
            h.initCB().SelectedIndex = -1;
            h.initTB()[3].Clear();

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
