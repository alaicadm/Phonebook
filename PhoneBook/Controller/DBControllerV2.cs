using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Controller
{
    public static class DBControllerV2
    {
        private static string sname;
        
        public static string ServerName { get { return sname; } set { sname = value; } }
    }
}
