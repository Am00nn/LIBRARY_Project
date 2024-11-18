using project_libraryy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_libraryy.Repositories
{
    public class BorrowingRepository
    {

        private readonly ApplicationDbContext _context;


        public BorrowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Borrowing borrowing)
        {
            if (borrowing == null)
                throw new ArgumentNullException(nameof(borrowing));

            _context.Borrowings.Add(borrowing);
            _context.SaveChanges();
        }


        public void Update(Borrowing borrowing)
        {
            var existing = _context.Borrowings.FirstOrDefault(b => b.Borrowing_ID == borrowing.Borrowing_ID);
            if (existing != null)
            {
                existing.B_BorrowingDate = borrowing.B_BorrowingDate;
                existing.B_PredictedReturnDate = borrowing.B_PredictedReturnDate;
                existing.B_ActualReturnDate = borrowing.B_ActualReturnDate;
                existing.Rating = borrowing.Rating;
                existing.IsReturned = borrowing.IsReturned;
                existing.U_ID = borrowing.U_ID;
                existing.Book_ID = borrowing.Book_ID;

                _context.Borrowings.Update(existing);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Borrowing record not found.");
            }
        }


        public List<Borrowing> GetAll()
        {
            return _context.Borrowings.ToList();
        }


        public List<Borrowing> GetByUserId(int userId)
        {
            return _context.Borrowings.Where(b => b.U_ID == userId).ToList();
        }


        public List<Borrowing> GetByBookId(int bookId)
        {
            return _context.Borrowings.Where(b => b.Book_ID == bookId).ToList();
        }


        public Borrowing GetById(int borrowingId)
        {
            return _context.Borrowings.FirstOrDefault(b => b.Borrowing_ID == borrowingId);
        }


        public void Delete(int borrowingId)
        {
            var borrowing = _context.Borrowings.Find(borrowingId);
            if (borrowing != null)
            {
                _context.Borrowings.Remove(borrowing);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Borrowing record not found.");
            }
        }


    }
}
