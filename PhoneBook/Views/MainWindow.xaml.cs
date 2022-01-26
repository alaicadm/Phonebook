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
        //private SqlConnection conn;
        private string connectionString;
        private SqlCommand cmd_fill;
        public IList<Contact> _contactsList = new List<Contact>();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowVM();

            connectionString = @"Data Source=localhost; Initial Catalog = Phonebook; Integrated Security=True";
            //SqlConnection conn = new SqlConnection(connectionString);

        }

        public void refreshList(object sender, EventArgs e)
        {
            try
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(UserList.ItemsSource);
                view.Refresh();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MessageBox.Show("refreshed");
            }
            
            
            /*SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            cmd_fill = new SqlCommand("SELECT * FROM Contacts", conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd_fill);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Contacts");

            try
            {
                if (_contactsList != null)
                {
                    foreach (DataRow dataRow in ds.Tables[0].Rows)
                    {
                        _contactsList.Add(new Contact
                        {
                            UserId = (int)dataRow["UserId"],
                            FirstName = dataRow["FirstName"].ToString(),
                            MiddleName = dataRow["MiddleName"].ToString(),
                            LastName = dataRow["LastName"].ToString(),
                            Gender = dataRow["Gender"].ToString(),
                            Mobile = dataRow["Mobile"].ToString()

                        });
                    }

                }
                MessageBox.Show("refreshed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                adapter.Dispose();
                conn.Close();
                conn.Dispose();
            }*/

        }

        public bool fieldChecker(object sender, EventArgs e)
        {
            bool res = true;
            if (string.IsNullOrEmpty(fname.Text)) { res = false; }
            if (string.IsNullOrEmpty(mname.Text)) { res = false; }
            if (string.IsNullOrEmpty(lname.Text)) { res = false; }
            if (string.IsNullOrEmpty(gender.Text)) { res = false; }
            if (string.IsNullOrEmpty(mobile.Text)) { res = false; }
            return res;

        }
        public void addContact(object sender, EventArgs e)
        {
            bool check = fieldChecker(sender, e);

            if (check == true)
            {

                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO " +
                                    "Contacts (FirstName, MiddleName, LastName, Gender, Mobile)" +
                                    "VALUES (@FirstName, @MiddleName, @LastName, @Gender, @Mobile)";
                cmd.Parameters.AddWithValue("@FirstName", fname.Text);
                cmd.Parameters.AddWithValue("@MiddleName", mname.Text);
                cmd.Parameters.AddWithValue("@LastName", lname.Text);
                cmd.Parameters.AddWithValue("@Gender", gender.Text);
                cmd.Parameters.AddWithValue("@Mobile", mobile.Text);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("A contact has been added! Refresh table.", "Phonebook", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data not added");
                }
                finally
                {
                    conn.Close();

                }
            }
            else
            {
                MessageBox.Show("An empty field was recognized, check your inputs!");
            }
                
               
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

        public void updateContact(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = conn.CreateCommand();
            var selectedRow = UserList.SelectedItem;
            Contact req = selectedRow as Contact;
            long id = req.UserId;
            cmd.CommandText = $"UPDATE Contacts SET firstName=@FirstName, middleName=@MiddleName, lastName=@LastName, gender=@Gender, mobile=@Mobile WHERE userId= {id} ";
            cmd.Parameters.AddWithValue("@FirstName",fname.Text);
            cmd.Parameters.AddWithValue("@MiddleName", mname.Text);
            cmd.Parameters.AddWithValue("@LastName", lname.Text);
            cmd.Parameters.AddWithValue("@Gender", gender.Text);
            cmd.Parameters.AddWithValue("@Mobile", mobile.Text);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("A contact has been updated! Refresh table.", "Phonebook", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Data not updated");
            }
            finally
            {
                conn.Close();

            }

        }

        public void deleteContact(object sender, EventArgs e)
        {

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = conn.CreateCommand();
            var selectedRow = UserList.SelectedItem;
            Contact req = selectedRow as Contact;
            long id = req.UserId;
            cmd.CommandText = $"DELETE FROM Contacts WHERE userId = {id}";
           
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("A contact has been removed! Refresh table.", "Phonebook", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Data on ID: {id} was not deleted.", "Phonebook", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            finally
            {
                conn.Close();
            }



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
