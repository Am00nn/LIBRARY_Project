using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_libraryy.Models
{
    public class Admin
    {
        [Key]
        public int A_ID { get; set; }

        [Required]
        public string A_Name { get; set; }

        [Required]
        [EmailAddress]
        public string A_Email { get; set; }


        [Required]
        [MinLength(8)]

        public string A_Password { get; set; }
    }
}
