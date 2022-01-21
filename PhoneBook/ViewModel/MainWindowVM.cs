using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PhoneBook.ViewModel
{
    public class MainWindowVM : ViewModelBase
    {
        private IList<Contact> _contactsList;

       

        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand RefreshCommand { get; set; }


        public MainWindowVM()
        {
            _contactsList = new List<Contact>
            {
                new Contact{UserId=1,FirstName="Bryant",MiddleName="Kyle",LastName="Sagun",Gender="Male",Mobile="09083483552"},
                new Contact{UserId=2,FirstName="Gian",MiddleName="Carlo",LastName="Genisera",Gender="Male",Mobile="09083483552"},
                new Contact{UserId=3,FirstName="Alaica",MiddleName="Dela Banda",LastName="Marino",Gender="Female",Mobile="09083483552"},
            };



            AddCommand = new RelayCommand(AddMethod);
            UpdateCommand = new RelayCommand(UpdateMethod);
            RefreshCommand = new RelayCommand(RefreshMethod);
        }

        public IList<Contact> Contacts
        {
            get { return _contactsList; }
            set { _contactsList = value; }
        }

        

        public void AddMethod(object message)
        {
            Contact c = new Contact();
            _contactsList.Add(new Contact { UserId=4, FirstName = c.FirstName, MiddleName = c.MiddleName, LastName = c.LastName, Gender = c.Gender, Mobile = c.Mobile });
            MessageBox.Show("A contact has been added!","Phonebook", MessageBoxButton.OK, MessageBoxImage.Information,MessageBoxResult.OK);
        }

        public void UpdateMethod(object message)
        {
            MessageBox.Show("A contact has been updated!", "Phonebook", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
        }

        public void RefreshMethod(object message)
        {
            /*Contact c = new Contact();
            c.FirstName = "test";
            c.MiddleName = "test";
            c.LastName = "test";*/
            
        }

       

       



    }
}
