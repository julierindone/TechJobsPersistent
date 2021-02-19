
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TechJobsPersistent.Models;

namespace TechJobsPersistent.ViewModels
{
    public class AddEmployerViewModel
    {
        private Employer theEmployer;

        [Required (ErrorMessage = "Must enter name of employer.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be 3 to 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Must enter location.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage ="Location must be 3 to 50 characters.")]

        public string Location { get; set; }

        public AddEmployerViewModel()
        {
        }

        public AddEmployerViewModel(Employer theEmployer)
        {
            this.theEmployer = theEmployer;
        }
    }

   
}
