using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Westcoast.web.ViewModels
{
    public class UserListViewModel
    {
        public int UserID {get; set;}
        public string FirstName {get; set;} = "";
        public string UserTitle {get; set;} = "";
        public string LastName {get; set;} = "";
        public string Email {get; set;} = "";
        public string PhoneNumber {get; set;} = "";
        public string StreetAdress {get; set;} = "";
        public string PostalCode {get; set;} = "";
    }
}