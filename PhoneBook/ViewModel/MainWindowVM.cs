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
        private SqlCommand cmd;
        


        public MainWindowVM()
        {

            //for connection
            connectionString = @"Data Source=localhost; Initial Catalog = Phonebook; Integrated Security=True";
            conn = new SqlConnection(connectionString);
            conn.Open();

            cmd = new SqlCommand("usp_read_contact", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
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
            { MessageBox.Show(ex.Message);}
            finally { conn.Close(); }
            
      

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
            //execute someting 
        }

        public void UpdateMethod(object message)
        {
            //execute something   
        }

       
        
        public void RefreshMethod(object message)
        {
            //execute something   
        }



    }
}
