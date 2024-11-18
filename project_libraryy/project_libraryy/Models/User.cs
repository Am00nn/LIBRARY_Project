using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace project_libraryy.Models
{
    public enum Gender
    {
        Female,
        Male
    }
    public class User
    {
        [Key]
        public int U_ID { get; set; }

        [Required]
        public string U_Name { get; set; }

        [Required]
        [EmailAddress]
        public string U_Email { get; set; }

        public Gender U_Gender { get; set; }

        [Required]

        public string U_Passcode { get; set; }

        [Required]
        [MinLength(8)]
        public string U_Password { get; set; }

        public virtual ICollection<Borrowing> Borrowings { get; set; }
    }
}
