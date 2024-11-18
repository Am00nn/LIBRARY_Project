using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_libraryy.Models
{
    public class Borrowing
    {
        [Key]
        public int Borrowing_ID { get; set; }

        [Required]
        public DateTime B_BorrowingDate { get; set; }

        [Required]
        public DateTime B_PredictedReturnDate { get; set; }

        public DateTime? B_ActualReturnDate { get; set; }

        public int? Rating { get; set; }

        [Required]
        public bool IsReturned { get; set; }

        [ForeignKey("User")]
        public int U_ID { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Book")]
        public int Book_ID { get; set; }
        public virtual Book Book { get; set; }
    }
}
