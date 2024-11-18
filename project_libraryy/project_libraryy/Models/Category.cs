using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_libraryy.Models
{
    public class Category
    {
        [Key]
        public int C_ID { get; set; }

        [Required]
        public string C_Name { get; set; }

        [Required]
        public int C_NumberOfBooks { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
