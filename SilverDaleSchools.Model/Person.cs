using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Remotion.Data.Linq;
//using LinqToExcel;
//using LinqToExcel.Attributes;

namespace SilverDaleSchools.Model
{
    public class Person
    {
       // public int PersonID { get; set; }

        [Display(Name = "First Name")]
        //[ExcelColumn("Firstname")]
        [Required]
        public string FirstName { get; set; }

        [Required]
        
       // [ExcelColumn("Student Number")]
        [Display(Name = "Staff ID")]
        public string UserID { get; set; }

       //  [ExcelColumn("Surname")]
        [Display(Name = "Middle Name")]
        [Required]
        public string Middle { get; set; }
        
        [Display(Name = "Last Name")]
        [Required]
       // [ExcelColumn("Lastname")]
        public string LastName { get; set; }


       // [Required]
       // [ExcelColumn("sex")]
        public string Sex { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
