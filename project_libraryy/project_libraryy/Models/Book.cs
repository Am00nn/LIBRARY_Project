using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_libraryy.Models
{
    public class Book
    {
        [Key]
        public int Book_ID { get; set; }

        [Required]
        public string B_Name { get; set; }

        [Required]
        public string B_Author { get; set; }

        [Required]
        public int B_TotalCopies { get; set; }

        [Required]
        public int B_BorrowedCopies { get; set; }

        [Required]
        public decimal B_Price { get; set; }

        [Required]
        public int B_BorrowingPeriod { get; set; }

        [ForeignKey("Category")]
        public int C_ID { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Borrowing> Borrowings { get; set; }


    }
}
