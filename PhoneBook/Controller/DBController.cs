using PhoneBook.Models;
using PhoneBook.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PhoneBook.Controller
{
    
    public class DBController : Contact
    {

        private static string sname;
        private static string dbname;
        private static string dbuname;
        private static string dbpass;

        public static string ServerName { get { return sname; } set { sname = value; } }
        public static string DbName { get { return dbname; } set { dbname = value; } }
        public static string UserName { get { return dbuname; } set { dbuname = value; } }
        public static string Password { get { return dbpass; } set { dbpass = value; } }

        public IList<Contact> _contactsList = new List<Contact>();
        public static Helpers Helpers = new Helpers();

        
        public DBController()
        {

        }


        public static string ConnString()
        {
            ServerName = "localhost";
            DbName = "Phonebook";
            UserName = "sa_pb";
            Password = "alaicadm";
            return @"Data Source=" + ServerName + "; Initial Catalog =" + DbName + "; User Id = " + UserName + ";Password = " + Password + "; Integrated Security=True";
        }

        public SqlConnection Connect(string connectionString) { return new SqlConnection(connectionString); } 
        public SqlCommand Command(string sp, SqlConnection conn) { return new SqlCommand(sp, conn); }


        public void load()
        {
            
            SqlConnection conn = Connect(ConnString());
            SqlCommand cmd = Command("usp_read_contact", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Contacts");

            try
            {
                if (_contactsList != null)
                {
                    _contactsList.Clear();
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



            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            finally { conn.Close(); }

            //fill the listview with contacts list
            Helpers.initLV().ItemsSource = _contactsList;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Helpers.initLV().ItemsSource);
            view.Refresh();

        }



        public void insert()
        {
           
            SqlConnection conn = Connect(ConnString());
            SqlCommand cmd = Command("usp_add_contact", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            string[] strlist = { "@firstName", "@middleName", "@lastName", "@gender", "@mobile" };
            string[] txtboxVal = { Helpers.initTB()[0].Text, Helpers.initTB()[1].Text, Helpers.initTB()[2].Text, Helpers.initCB().Text, Helpers.initTB()[3].Text };
            addParams(cmd, strlist, txtboxVal);
            execute(conn, cmd, "A contact has been added!", "A contact was not added!");
            load();
        }

        public void update()
        {
           
            SqlConnection conn = Connect(ConnString());
            SqlCommand cmd = Command("usp_update_contact", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            var selectedRow = Helpers.initLV().SelectedItem;
            Contact req = selectedRow as Contact;
            int id = req.UserId;
            string[] strlist = { "@firstName", "@middleName", "@lastName", "@gender", "@mobile" };
            string[] txtboxVal = { Helpers.initTB()[0].Text, Helpers.initTB()[1].Text, Helpers.initTB()[2].Text, Helpers.initCB().Text, Helpers.initTB()[3].Text };
            addParams(cmd, strlist, txtboxVal);
            cmd.Parameters.AddWithValue("@userId", id);
            execute(conn, cmd, "A contact has been updated!", "A contact was not apdated!");
            load();

        }

        public void delete()
        {
           
            SqlConnection conn = Connect(ConnString());
            SqlCommand cmd = Command("usp_delete_contact", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            var selectedRow = Helpers.initLV().SelectedItem;
            Contact req = selectedRow as Contact;
            int id = req.UserId;
            cmd.Parameters.AddWithValue("@userId", id);
            execute(conn, cmd, "A contact has been removed!", $"Data on ID: {id} was not deleted.");
            load();
        }

        //to add parameters
        public SqlCommand addParams(SqlCommand cmd, String[] strlist, String[] txtboxVal)
        {
            for (int i = 0; i < strlist.Length; i++) { cmd.Parameters.AddWithValue(strlist[i], txtboxVal[i]); }
            return cmd;
        }

        //to execute connection and commands
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

    }
}
