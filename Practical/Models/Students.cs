using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Practical.Models
{
    public class Students
    {
        [Display(Name = "Id")]
        public int Stud_ID { get; set; }


        [Required(ErrorMessage = "Name is required.")]
        public string Stud_name { get; set; }


        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Mobile Number is required.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        public string Dept_id { get; set; }

        [Required(ErrorMessage = "Course is required.")]
        public string Course_id { get; set; }

    }
}