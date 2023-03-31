using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Westcoast.web.ViewModels;

    public class CoursePostViewModel
    {
        [Required(ErrorMessage = "KursNamn är obligatoriskt")]
        [DisplayName("Kursnamn")]
        public string CourseName {get; set;} = "";

        [Required(ErrorMessage = "Kurs titel är obligatoriskt")]
        [DisplayName("Titel")]
        public string CourseTitle {get; set;} = "";

        [Required(ErrorMessage = "Startdatum är obligatoriskt")]
        [DisplayName("Startdatum")]
        public string StartDate {get; set;} = "";

        [Required(ErrorMessage = "Slutdatum är obligatoriskt")]
        [DisplayName("Slutdatum")]
        public string EndDate {get; set;} = "";
        
        [Required(ErrorMessage = "Kurslängd är obligatoriskt")]
        [DisplayName("Kurslängd")]
        public string CourseLenght {get; set;} = "";
    }
