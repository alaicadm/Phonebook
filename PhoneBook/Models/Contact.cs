using PhoneBook.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Models
{
    public class Contact : ViewModelBase
    {

        private int userId;
        private string firstName;
        private string middleName;
        private string lastName;
        private string gender;
        private string mobile;

        public int UserId { get { return userId; } set { userId = value; OnPropertyChanged("UserId"); } }
        public string FirstName { get { return firstName; } set { firstName = value; OnPropertyChanged("FirstName"); } }
        public string LastName { get { return lastName; } set { lastName = value; OnPropertyChanged("LastName"); } }
        public string MiddleName { get { return middleName; } set { middleName = value; OnPropertyChanged("MiddleName"); } }
        public string Gender { get { return gender; } set { gender = value; OnPropertyChanged("Gender"); } }
        public string Mobile { get { return mobile; } set { mobile = value; OnPropertyChanged("Mobile"); } }
    }
}
