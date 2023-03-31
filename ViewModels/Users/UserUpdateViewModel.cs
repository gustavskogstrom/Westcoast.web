using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Westcoast.web.ViewModels
{
    public class UserUpdateViewModel
    {
        [Required(ErrorMessage = "Student/Lärare är obligatoriskt")]
        [DisplayName("Student/Lärare")]
        public int UserID {get; set;}
        [Required(ErrorMessage = "Förnamn är obligatoriskt")]
        [DisplayName("Förnamn")]
        public string FirstName {get; set;} = "";
        [Required(ErrorMessage = "Student/Lärare är obligatoriskt")]
        [DisplayName("Student/Lärare")]
        public string UserTitle {get; set;} = "";
        [Required(ErrorMessage = "Efternamn är obligatoriskt")]
        [DisplayName("Efternamn")]
        public string LastName {get; set;} = "";
        [Required(ErrorMessage = "Email är obligatoriskt")]
        [DisplayName("Email")]
        public string Email {get; set;} = "";
        [Required(ErrorMessage = "Telefonnummer är obligatoriskt")]
        [DisplayName("Telefonnummer")]
        public string PhoneNumber {get; set;} = "";
        [Required(ErrorMessage = "Gatuadress är obligatoriskt")]
        [DisplayName("Gatuadress")]
        public string StreetAdress {get; set;} = "";
        [Required(ErrorMessage = "Gatuadress är obligatoriskt")]
        [DisplayName("Gatuadress")]
        public string PostalCode {get; set;} = "";
        
    }
}