using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhoneBook.ViewModel
{
    public class MainWindowVM : ViewModelBase
    {
        public IList<Contact> _contactsList = new List<Contact>();

        public Contact c = new Contact();

        public RelayCommand ConnectCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand RefreshCommand { get; set; }

        private string connectionString;
        private SqlConnection conn;
        private SqlCommand cmd_fill;
        


        public MainWindowVM()
        {

            //for connection
            connectionString = @"Data Source=localhost; Initial Catalog = Phonebook; Integrated Security=True";
            conn = new SqlConnection(connectionString);

            conn.Open();
            cmd_fill = new SqlCommand("usp_read_contact", conn);
            //SqlDataReader dr = cmd_fill.ExecuteReader();
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
                            UserId = (int) dataRow["UserId"],
                            FirstName = dataRow["FirstName"].ToString(),
                            MiddleName = dataRow["MiddleName"].ToString(),
                            LastName = dataRow["LastName"].ToString(),
                            Gender = dataRow["Gender"].ToString(),
                            Mobile = dataRow["Mobile"].ToString()

                        });
                    }
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                
                conn.Close();
                
            }
            
      

            //list of commands 
           
            AddCommand = new RelayCommand(AddMethod, CanSave=>true);
            UpdateCommand = new RelayCommand(UpdateMethod);
            RefreshCommand = new RelayCommand(RefreshMethod);

        }

        
        public IList<Contact> Contacts
        {
            get { return _contactsList; }
            set 
            { 
                _contactsList = value;
                OnPropertyChanged("Contacts");
            }
        }

        public bool CanSave
        {
            get { return !string.IsNullOrEmpty(c.UserId.ToString()) && !string.IsNullOrEmpty(c.FirstName); }
        }

        
        public void AddMethod(object message)
        {


            //Contact c = new Contact();
            // _contactsList.Add(new Contact { UserId=4, FirstName = c.FirstName, MiddleName = c.MiddleName, LastName = c.LastName, Gender = c.Gender, Mobile = c.Mobile });
            //Contact contact = new Contact();

            //MessageBox.Show("A contact has been added!", "Phonebook", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
            string connectionString;
            SqlConnection conn;
            connectionString = @"Data Source=localhost; Initial Catalog = Phonebook; Integrated Security=True";
            conn = new SqlConnection(connectionString);


            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO " +
                                "Contacts (FirstName, MiddleName, LastName, Gender, Mobile)" +
                                "VALUES (@FirstName, @MiddleName, @LastName, @Gender, @Mobile)";
            cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
            cmd.Parameters.AddWithValue("@MiddleName", c.MiddleName);
            cmd.Parameters.AddWithValue("@LastName", c.LastName);
            cmd.Parameters.AddWithValue("@Gender", c.Gender);
            cmd.Parameters.AddWithValue("@Mobile", c.Mobile);



            try
            {
                conn.Open();
                //MessageBox.Show("Connected");
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data saved successfully");
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

        public void UpdateMethod(object message)
        {
            MessageBox.Show("A contact has been updated!", "Phonebook", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
        }

       
        
        public void RefreshMethod(object message)
        {

            //(message as TextBox).Text = string.Empty;
            /*Contact c = new Contact();
            c.FirstName = "test";
            c.MiddleName = "test";
            c.LastName = "test";*/

           

            
        }

       

       



    }
}
