using CollegeApp.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Models
{
    public class StudentDto
    {
        [ValidateNever]
        public int Id { get; set; }
        [Required(ErrorMessage ="student name is required")]
        public string StudentName { get; set; }
        [EmailAddress(ErrorMessage ="enter correct emailaddress")]
        [Remote(action:"VarifyEmail",controller:"Student")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Enter correct address")]
        public string Address { get; set; }
        public int DepartmentId { get; set; }


    }
}
