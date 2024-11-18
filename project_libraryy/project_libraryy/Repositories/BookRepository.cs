using project_libraryy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_libraryy.Repositories
{
    public class BookRepository
    {

        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        public void Add(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public Book GetByName(string name)
        {
            return _context.Books.FirstOrDefault(b => b.B_Name == name);
        }

        public void UpdateByName(string name, Book updatedBook)
        {
            var book = GetByName(name);
            if (book != null)
            {
                book.B_Name = updatedBook.B_Name;
                book.B_Author = updatedBook.B_Author;
                book.B_TotalCopies = updatedBook.B_TotalCopies;
                book.B_BorrowedCopies = updatedBook.B_BorrowedCopies;
                book.B_Price = updatedBook.B_Price;
                book.B_BorrowingPeriod = updatedBook.B_BorrowingPeriod;
                book.C_ID = updatedBook.C_ID;

                _context.SaveChanges();
            }
        }


        public void DeleteById(int bookId)
        {
            var book = _context.Books.Find(bookId);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }


        public decimal GetTotalPrice()
        {
            return _context.Books.Sum(b => b.B_Price);
        }


        public decimal GetMaxPrice()
        {
            return _context.Books.Max(b => b.B_Price);
        }


        public int GetTotalBorrowedBooks()
        {
            return _context.Books.Sum(b => b.B_BorrowedCopies);
        }


        public int GetTotalBooksPerCategoryName(string categoryName)
        {
            return _context.Books
                .Where(b => b.Category.C_Name == categoryName)
                .Sum(b => b.B_TotalCopies);
        }

        public Book GetById(int bookId)
        {
            return _context.Books.FirstOrDefault(b => b.Book_ID == bookId);
        }



        public void Update(Book updatedBook)
        {
            var existingBook = _context.Books.FirstOrDefault(b => b.Book_ID == updatedBook.Book_ID);
            if (existingBook != null)
            {
                existingBook.B_TotalCopies = updatedBook.B_TotalCopies;
                existingBook.B_BorrowedCopies = updatedBook.B_BorrowedCopies;
                _context.SaveChanges();
            }
        }


    }
}
