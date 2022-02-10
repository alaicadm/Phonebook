using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PhoneBook.Controller
{
    public class Helpers
    {
        
        //textboxes
        public TextBox[] initTB()
        {
            TextBox fname = (Application.Current.MainWindow as MainWindow).fname;
            TextBox mname = (Application.Current.MainWindow as MainWindow).mname;
            TextBox lname = (Application.Current.MainWindow as MainWindow).lname;
            TextBox mobile = (Application.Current.MainWindow as MainWindow).mobile;
            TextBox[] list = { fname, mname, lname, mobile };
            return list;
        }

        //combobox
        public ComboBox initCB()
        {
            ComboBox gender = (Application.Current.MainWindow as MainWindow).gender;
            return gender;
        }

        //listview 
        public ListView initLV()
        {
            return (Application.Current.MainWindow as MainWindow).UserList;
        }

        public TextBox search()
        {
            return (Application.Current.MainWindow as MainWindow).searchContact;
        }


    }
}
