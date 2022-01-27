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

namespace PhoneBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {
        private string connectionString;
        public IList<Contact> _contactsList = new List<Contact>();
        private CollectionView view;
        public MainWindow() 
        {
            InitializeComponent();
            DataContext = new MainWindowVM(); 
            connectionString = @"Data Source=localhost; Initial Catalog = Phonebook; Integrated Security=True";
            view = (CollectionView)CollectionViewSource.GetDefaultView(UserList.ItemsSource);
            view.Filter = ContactFilter;
            view.SortDescriptions.Add(new SortDescription("UserId", ListSortDirection.Ascending));

        }

        
        private bool ContactFilter(object item) //filters contact
        {
            if (String.IsNullOrEmpty(searchContact.Text)) { return true; }
            else { 
                return ((item as Contact).FirstName.IndexOf(searchContact.Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || (item as Contact).MiddleName.IndexOf(searchContact.Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || (item as Contact).LastName.IndexOf(searchContact.Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || (item as Contact).Mobile.IndexOf(searchContact.Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || (item as Contact).Gender.IndexOf(searchContact.Text, StringComparison.OrdinalIgnoreCase) >= 0
                    );
            }
        }

        private void searchContact_txtChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(UserList.ItemsSource).Refresh();
        }

        public void refreshList(object sender, EventArgs e) //currently not working
        {
            try
            {
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(UserList.ItemsSource);
                view.Refresh();
                UserList.SelectedItem = null;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MessageBox.Show("refreshed");
            }
            
            

        }

        public SqlConnection Connect(string connectionString) { return new SqlConnection(connectionString); }
        public SqlCommand Command(string sp, SqlConnection conn) { return new SqlCommand(sp, conn); }

        public bool fieldChecker(object sender, EventArgs e) //checks if a textbox/combobox is null or not
        {
            bool res = true;
            TextBox[] textBoxes = { fname, mname, lname, mobile };

            foreach (TextBox i in textBoxes) { if (string.IsNullOrEmpty(i.Text)) { res = false; } }
            if (string.IsNullOrEmpty(gender.Text)) { res = false; }

            return res;

        }
        public void execute(SqlConnection conn, SqlCommand cmd, string successMessage, string failedMessage) //execute connection and command
        {
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show(successMessage, "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(failedMessage, "FAILED", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            finally { conn.Close(); }
        }

        public SqlCommand addParams(SqlCommand cmd, String[] strlist, String[] txtboxVal)
        {
            for (int i = 0; i < strlist.Length; i++) { cmd.Parameters.AddWithValue(strlist[i], txtboxVal[i]); }
            return cmd;
        }
        public void addContact(object sender, EventArgs e)
        {
            bool check = fieldChecker(sender, e);

            if (check == true)
            {
                SqlConnection conn = Connect(connectionString);
                SqlCommand cmd = Command("usp_add_contact", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                string[] strlist = { "@firstName", "@middleName", "@lastName","@gender", "@mobile" };
                string[] txtboxVal = { fname.Text, mname.Text, lname.Text, gender.Text, mobile.Text };
                addParams(cmd, strlist, txtboxVal);
                execute(conn,cmd, "A contact has been added! Refresh table.","A contact was not added!");
            }
            else { MessageBox.Show("An empty field was recognized, check your inputs!"); }
                
               
        }

        public void refreshOnClick(object sender, EventArgs e) //to refresh textboxes and unselect the select item on listview 
        {
            UserList.SelectedItem = null;
            fname.Clear();
            mname.Clear();
            lname.Clear();
            gender.SelectedIndex = -1;
            mobile.Clear();
            
        }

        public void updateContact(object sender, EventArgs e) 
        {
            SqlConnection conn = Connect(connectionString);
            SqlCommand cmd = Command("usp_update_contact",conn);
            cmd.CommandType = CommandType.StoredProcedure;
            var selectedRow = UserList.SelectedItem;
            Contact req = selectedRow as Contact;
            int id = req.UserId;

            string[] strlist = { "@firstName", "@middleName", "@lastName","@gender", "@mobile" };
            string[] txtboxVal = { fname.Text, mname.Text, lname.Text, gender.Text, mobile.Text };
            addParams(cmd, strlist, txtboxVal);
            cmd.Parameters.AddWithValue("@userId", id);

            bool check = fieldChecker(sender, e);
            if (check == true) { execute(conn, cmd, "A contact has been updated! Refresh table.", "Contact not updated!"); }

        }

        public void deleteContact(object sender, EventArgs e)
        {

            SqlConnection conn = Connect(connectionString);
            SqlCommand cmd = Command("usp_delete_contact",conn);
            cmd.CommandType = CommandType.StoredProcedure;
            var selectedRow = UserList.SelectedItem;
            Contact req = selectedRow as Contact;
            int id = req.UserId;
            cmd.Parameters.AddWithValue("@userId", id);
            execute(conn, cmd, "A contact has been removed! Refresh table.", $"Data on ID: {id} was not deleted.");

        }

       
        
    }
}
