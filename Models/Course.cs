using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Westcoast.web.Models
{
    public class Course
    {
        [Key]
        public int CourseID {get; set;}
        public string CourseName {get; set;} = "";
        public string CourseTitle {get; set;} = "";
        public string StartDate {get; set;} = "";
        public string EndDate {get; set;} = "";
        public string CourseLenght {get; set;} = "";
    }
}