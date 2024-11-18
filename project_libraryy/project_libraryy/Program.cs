using project_libraryy.Models;
using project_libraryy.Repositories;

namespace project_libraryy
{
    internal class Program
    {
        static void Main(string[] args)
        {

            bool ExitFlag = false;


            using var context = new ApplicationDbContext();
            var adminRepository = new AdminRepository(context);
            var userRepository = new UserRepository(context);
            var bookRepository = new BookRepository(context);
            var categoryRepository = new CategoryRepository(context);
            var borrowingRepository = new BorrowingRepository(context);


            do
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("      Welcome to the Library System     ");
                Console.WriteLine("========================================\n");

                Console.WriteLine("Please choose an option :");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine(" A - Admin");
                Console.WriteLine(" B - User");
                Console.WriteLine(" C - Exit");
                Console.WriteLine("----------------------------------------");
                Console.Write("\nYour choice: ");

                string choice = Console.ReadLine().ToUpper();

                try
                {
                    switch (choice)
                    {
                        case "A":
                            Console.Clear();
                            Console.WriteLine("Admin Menu");
                            AdminMenu(adminRepository, bookRepository, categoryRepository, userRepository, borrowingRepository);
                            break;

                        case "B":
                            Console.Clear();
                            Console.WriteLine("User Menu");
                            UserMenu(userRepository, bookRepository, borrowingRepository, categoryRepository);
                            break;

                        case "C":
                            Console.WriteLine("\nExiting the system...");
                            ExitFlag = true;
                            break;

                        default:
                            Console.WriteLine("\nInvalid choice. Please select a valid option.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while processing your choice: " + ex.Message);
                }

                if (!ExitFlag)
                {
                    Console.WriteLine("\nPress any key to return to the menu...");
                    Console.ReadKey();
                }

            } while (!ExitFlag);

            Console.Clear();
            Console.WriteLine("Thank you , Goodbye!");
        }


        static void AdminMenu(AdminRepository adminRepository, BookRepository bookRepository, CategoryRepository categoryRepository, UserRepository userRepository, BorrowingRepository borrowingRepository)
        {
            Console.WriteLine("\n=========================================");
            Console.WriteLine("               Admin Menu");
            Console.WriteLine("=========================================");
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.Write("enter your choice: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("error : invalid input, please enter number.");
                return;
            }

            switch (choice)
            {
                case 1:
                    AdminRegister(adminRepository);
                    break;

                case 2:
                    AdminLogin(adminRepository, bookRepository, categoryRepository, userRepository, borrowingRepository);
                    break;

                default:
                    Console.WriteLine(" please try again.");
                    break;
            }
        }



        static void AdminRegister(AdminRepository adminRepository)
        {
            Console.Write("\nEnter Your Name: ");

            string admin_name = Console.ReadLine();


            if (adminRepository.GetAll().Any(a => a.A_Name == admin_name))
            {
                Console.WriteLine("admin name already exists, Enter different name.");

                return;
            }

            Console.Write("Enter Your Email: ");

            string Admin_email = Console.ReadLine();


            if (!Validate_Email(Admin_email))
            {
                Console.WriteLine("error : email must include '@' and end with '.com' or '.edu'.");
                return;
            }


            if (adminRepository.GetAll().Any(a => a.A_Email == Admin_email))

            {
                Console.WriteLine("error : email already exists enter  different email.");

                return;
            }

            Console.Write("Enter  your Password: ");

            string Admin_password = Console.ReadLine();


            if (!Validate_Password(Admin_password))
            {
                Console.WriteLine("Password must be at least 8 characters and include uppercase, lowercase, digit, and symbol.");
                return;
            }


            adminRepository.Add(new Admin { A_Name = admin_name, A_Email = Admin_email, A_Password = Admin_password });

            Console.WriteLine("Admin registered successfully!");
        }


        static void AdminLogin(AdminRepository adminRepository, BookRepository bookRepository, CategoryRepository categoryRepository, UserRepository userRepository, BorrowingRepository borrowingRepository)
        {
            Console.Write("\nEnter Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            var admin = adminRepository.GetByEmail(email);
            if (admin != null && admin.A_Password == password)
            {
                Console.WriteLine("\nWelcome, Admin!");
                AdminOperations(adminRepository, bookRepository, categoryRepository, userRepository, borrowingRepository);
            }
            else
            {
                Console.WriteLine("Invalid credentials.");
            }
        }

        static void AdminOperations(AdminRepository adminRepository, BookRepository bookRepository, CategoryRepository categoryRepository, UserRepository userRepository, BorrowingRepository borrowingRepository)
        {
            while (true)
            {
                Console.WriteLine("\n=========================================");
                Console.WriteLine("            Admin Operations");
                Console.WriteLine("=========================================");

                Console.WriteLine("1. View Books");

                Console.WriteLine("2. Add Book");

                Console.WriteLine("3. Update Book");

                Console.WriteLine("4. Delete Book");

                Console.WriteLine("5. View Categories");

                Console.WriteLine("6. Add Category");

                Console.WriteLine("7. Delete Category");

                Console.WriteLine("8. Update Category");

                Console.WriteLine("9. View Reports");

                Console.WriteLine("10. Add User");

                Console.WriteLine("11. Delete User");

                Console.WriteLine("12. Update User");

                Console.WriteLine("13. View All Users");

                Console.WriteLine("14. Logout");

                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        DisplayAllBooks(bookRepository, categoryRepository);

                        break;

                    case 2:
                        AddNewBook(bookRepository, categoryRepository);
                        break;

                    case 3:
                        UpdateBook(bookRepository, categoryRepository);
                        break;

                    case 4:
                        DeleteBook(bookRepository);
                        break;

                    case 5:
                        DisplayAllCategories(categoryRepository);
                        break;
                    case 6:
                        CreateCategory(categoryRepository);
                        break;

                    case 7:
                        RemoveCategory(categoryRepository);
                        break;

                    case 8:
                        ModifyCategory(categoryRepository);
                        break;
                    case 9:
                        CreateLibraryReport(bookRepository, categoryRepository, borrowingRepository);
                        break;
                    case 10:
                        RegisterNewUser(adminRepository);
                        break;

                    case 11:
                        DeleteUser(adminRepository);
                        break;

                    case 12:
                        UpdateUser(adminRepository);
                        break;


                    case 13: // View All Users
                        ViewAllUsers(userRepository);
                        break;

                    case 14:
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        static void UserMenu(UserRepository userRepository, BookRepository bookRepository, BorrowingRepository borrowingRepository, CategoryRepository categoryRepository)
        {
            Console.WriteLine("\n=========================================");
            Console.WriteLine("                User  ");
            Console.WriteLine("=========================================");
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.Write("Enter your choice: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                return;
            }

            switch (choice)
            {
                case 1:
                    User_Register(userRepository);
                    break;

                case 2:
                    UserLogin(userRepository, bookRepository, borrowingRepository, categoryRepository);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }


        static void User_Register(UserRepository userRepository)
        {
            Console.Write("\nEnter Your Name: ");

            string User_name = Console.ReadLine();


            if (userRepository.GetAll().Any(u => u.U_Name == User_name))
            {

                Console.WriteLine("error : user name already exists enter a different name.");

                return;
            }

            Console.Write("Enter your Email: ");
            string User_email = Console.ReadLine();


            if (!Validate_Email(User_email))
            {

                Console.WriteLine("error : email must include '@' and end with '.com' or '.edu'.");

                return;
            }


            if (userRepository.GetAll().Any(u => u.U_Email == User_email))
            {
                Console.WriteLine("error : email already exists. enter different email.");

                return;
            }

            Console.Write("Enter Gender 0 for F, 1 for M : ");

            if (!int.TryParse(Console.ReadLine(), out int gender_input) || (gender_input != 0 && gender_input != 1))
            {

                Console.WriteLine("Invalid gender input. Please enter 0 for F or 1 for M.");

                return;
            }

            Gender gender = (Gender)gender_input;

            Console.Write("please enter your passcode: ");

            string User_passcode = Console.ReadLine();

            Console.Write("please enter your Password: ");

            string User_password = Console.ReadLine();


            if (!Validate_Password(User_password))
            {
                Console.WriteLine("Password must be at least 8 characters and include uppercase, lowercase, digit, and symbol.");
                return;
            }


            userRepository.Add(new User { U_Name = User_name, U_Email = User_email, U_Gender = gender, U_Passcode = User_passcode, U_Password = User_password });

            Console.WriteLine("User registered successfully!");
        }

        static bool Validate_Email(string User_email)
        {

            return User_email.Contains("@") && (User_email.EndsWith(".com") || User_email.EndsWith(".edu"));

        }

        static bool Validate_Password(string User_password)
        {
            if (User_password.Length < 8) return false;

            bool hasUpper = User_password.Any(char.IsUpper);

            bool hasLower = User_password.Any(char.IsLower);

            bool hasDigit = User_password.Any(char.IsDigit);

            bool hasSymbol = User_password.Any(ch => !char.IsLetterOrDigit(ch));

            return hasUpper && hasLower && hasDigit && hasSymbol;
        }

        static void UserLogin(UserRepository userRepository, BookRepository bookRepository, BorrowingRepository borrowingRepository, CategoryRepository categoryRepository)
        {
            Console.Write("\n please enter  your Email: ");

            string User_email = Console.ReadLine();

            Console.Write("please enter  your Password: ");

            string User_password = Console.ReadLine();


            var user = userRepository.GetAll().FirstOrDefault(u => u.U_Email == User_email && u.U_Password == User_password);

            if (user != null)
            {



                bool hasOverdueBooks = borrowingRepository.GetByUserId(user.U_ID).Any(b => !b.IsReturned && b.B_PredictedReturnDate < DateTime.Now);

                if (hasOverdueBooks)
                {
                    Console.WriteLine("\n You have overdue books. Please return them ");

                    HandleOverdueBooks(user, bookRepository, borrowingRepository, categoryRepository);
                }
                else
                {
                    HandleUserActions(user, bookRepository, borrowingRepository, categoryRepository);
                }
            }
            else
            {
                Console.WriteLine("error : invalid email or password.");
            }
        }


        static void HandleOverdueBooks(User user, BookRepository bookRepository, BorrowingRepository borrowingRepository, CategoryRepository categoryRepository)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n=========================================");
                Console.WriteLine("        User          ");
                Console.WriteLine("=========================================");
                Console.WriteLine("1. Return Book");

                Console.WriteLine("2. Logout");

                Console.Write("please enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("error : Please enter a number.");

                    continue;
                }

                switch (choice)
                {
                    case 1:
                        ReturnBook(user.U_ID, borrowingRepository, bookRepository, categoryRepository);
                        break;

                    case 2:
                        running = false;
                        Console.WriteLine("Logging out...");
                        break;

                    default:
                        Console.WriteLine("error your choice incoorect  Please try again.");
                        break;
                }
            }
        }



        static void HandleUserActions(User user, BookRepository bookRepository, BorrowingRepository borrowingRepository, CategoryRepository categoryRepository)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n=========================================");
                Console.WriteLine("             User Operations");
                Console.WriteLine("=========================================");
                Console.WriteLine("1. View Books");

                Console.WriteLine("2. Borrow Book");

                Console.WriteLine("3. Return Book");

                Console.WriteLine("4. Search Books");

                Console.WriteLine("5. Search Books");



                Console.WriteLine("6. Logout");

                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        DisplayAllBooks(bookRepository, categoryRepository);
                        break;

                    case 2:
                        BorrowBook(user, bookRepository, borrowingRepository, categoryRepository);
                        break;

                    case 3:
                        ReturnBook(user.U_ID, borrowingRepository, bookRepository, categoryRepository);
                        break;
                    case 4:

                        SearchBooks(bookRepository, categoryRepository, user, borrowingRepository);
                        break;

                    case 5:
                        running = false;
                        Console.WriteLine("Logging out...");
                        break;



                    default:
                        Console.WriteLine("error : inccorect  choice. please try again.");
                        break;
                }
            }
        }


        static void DisplayAllBooks(BookRepository bookRepo, CategoryRepository categoryRepo)
        {
            Console.WriteLine("{0,-5} | {1,-30} | {2,-20} | {3,-10} | {4,-10} | {5,-10} | {6,-15} | {7,-5}",
                              "ID", "Title", "Author", "Copies", "Borrowed", "Price", "Category", "Period");
            Console.WriteLine(new string('-', 110));

            foreach (var b in bookRepo.GetAll())
            {

                var category = categoryRepo.GetById(b.C_ID);
                string categoryName = category != null ? category.C_Name : "N/A";

                Console.WriteLine("{0,-5} | {1,-30} | {2,-20} | {3,-10} | {4,-10} | {5,-10:F2} | {6,-15} | {7,-5} days",
                    b.Book_ID, b.B_Name, b.B_Author, b.B_TotalCopies, b.B_BorrowedCopies, b.B_Price, categoryName, b.B_BorrowingPeriod);
            }
        }



        static void AddNewBook(BookRepository bookRepository, CategoryRepository categoryRepository)
        {
            Console.Write("\nEnter Book Name: ");

            string Book_Name = Console.ReadLine();



            if (bookRepository.GetAll().Any(book => book.B_Name.Equals(Book_Name, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("error: A book with this title already exists. Please choose a different title.");

                return;
            }

            Console.Write("enter Author: ");

            string Author_Name = Console.ReadLine();

            Console.Write("enter Total Copies: ");
            if (!int.TryParse(Console.ReadLine(), out int totalCopies))
            {
                Console.WriteLine("error : invalid input. Please enter a numeric value for total copies.");

                return;
            }

            Console.Write(" enter Price: ");

            if (!decimal.TryParse(Console.ReadLine(), out decimal Book_price))
            {
                Console.WriteLine("error : invalid input. Please enter a valid decimal value for the price.");

                return;
            }


            Console.Write("Enter the borrowing period in days: ");

            if (!int.TryParse(Console.ReadLine(), out int day))
            {
                Console.WriteLine(" Borrowing period must be a number.");
                return;
            }


            Console.WriteLine("\n Available Categories:");

            foreach (var c in categoryRepository.GetAll())
            {
                Console.WriteLine($"ID: {c.C_ID}, Name: {c.C_Name}");
            }

            Console.Write("Enter the Category ID or type 0 to add a new category : ");

            if (!int.TryParse(Console.ReadLine(), out int c_Id))
            {
                Console.WriteLine("Invalid input. Category ID must be a number.");
                return;
            }


            var category = categoryRepository.GetById(c_Id);
            if (category == null)
            {
                if (c_Id == 0)
                {
                    CreateCategory(categoryRepository);


                    Console.WriteLine("Please enter the new Category ID again:");

                    if (!int.TryParse(Console.ReadLine(), out c_Id) || categoryRepository.GetById(c_Id) == null)
                    {
                        Console.WriteLine("error  : incorrect Category ID. Book creation canceled.");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("error : inncorect  Category ID. Please enter a valid ID or create a new category.");
                    return;
                }
            }


            bookRepository.Add(new Book
            {
                B_Name = Book_Name,
                B_Author = Author_Name,
                B_TotalCopies = totalCopies,
                B_Price = Book_price,
                B_BorrowingPeriod = day,
                C_ID = c_Id
            });

            Console.WriteLine("Book added successfully!");
        }




        static void DisplayAllCategories(CategoryRepository categoryRepository)
        {
            Console.WriteLine("\nCategories:");

            foreach (var c in categoryRepository.GetAll())
            {
                Console.WriteLine($"ID: {c.C_ID}, Name: {c.C_Name}, Number of Books: {c.C_NumberOfBooks}");
            }
        }

        static void DeleteBook(BookRepository bookRepository)
        {
            Console.Write("\nEnter Book Name to Delete: ");

            string Book_Name = Console.ReadLine();

            var b = bookRepository.GetByName(Book_Name);
            if (b == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }


            if (b.B_BorrowedCopies > 0)
            {
                Console.WriteLine($"error: The book '{Book_Name}' cannot be deleted as it currently has {b.B_BorrowedCopies} copies borrowed.");

                return;
            }

            try
            {
                bookRepository.DeleteById(b.Book_ID);
                Console.WriteLine($"Book '{Book_Name}' deleted successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }



        static void UpdateBook(BookRepository bookRepository, CategoryRepository categoryRepository)
        {
            Console.WriteLine("\n Available Books:");
            DisplayAllBooks(bookRepository, categoryRepository);

            Console.Write("\nEnter the name of the book you want to update: ");


            string Book_Name = Console.ReadLine();

            var b = bookRepository.GetByName(Book_Name);
            if (b == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            Console.WriteLine("\nUpdating Book Details:");

            Console.Write("enter a new name (or press Enter to retain the current name): ");

            string New_BookName = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(New_BookName))
            {

                if (bookRepository.GetAll().Any(b => b.B_Name.Equals(New_BookName, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("error: A book with this name already exists. Update canceled.");
                    return;
                }

                b.B_Name = New_BookName;
            }

            Console.Write("please enter new Author (or press Enter to retain the current name): ");

            string New_Author = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(New_Author))
            {
                b.B_Author = New_Author;
            }

            Console.Write("please enter new Total Copies (or press Enter to retain the current name): ");

            string TotalCopies = Console.ReadLine();

            if (int.TryParse(TotalCopies, out int New_TotalCopies))
            {

                if (New_TotalCopies < b.B_BorrowedCopies)
                {
                    Console.WriteLine($"error: Total copies cannot be reduced below the number of borrowed copies ({b.B_BorrowedCopies}). Update failed.");

                    return;
                }

                b.B_TotalCopies = New_TotalCopies;
            }

            try
            {
                bookRepository.Update(b);

                Console.WriteLine("Book updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void CreateCategory(CategoryRepository categoryRepository)
        {
            Console.WriteLine("\n--- Add a New Category ---");


            Console.Write("Category Name: ");

            string categoryName = Console.ReadLine();

            Console.Write("Number of Books in the Category: ");

            if (!int.TryParse(Console.ReadLine(), out int bookCount))
            {
                Console.WriteLine("Error: Please provide a valid numeric value for the number of books.");
                return;
            }

            try
            {

                var newCategory = new Category
                {
                    C_Name = categoryName,
                    C_NumberOfBooks = bookCount
                };


                categoryRepository.Add(newCategory);

                Console.WriteLine($"Success: The category '{categoryName}' has been added!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the category: {ex.Message}");
            }
        }


        static void RemoveCategory(CategoryRepository categoryRepository)
        {
            Console.Write("\n Enter the name of the category you wish to delete: ");

            string inputCategoryName = Console.ReadLine();


            var categoryToDelete = categoryRepository.GetByName(inputCategoryName);

            if (categoryToDelete == null)
            {
                Console.WriteLine("error: The specified category does not exist.");
                return;
            }

            try
            {

                categoryRepository.Delete(categoryToDelete.C_ID);

                Console.WriteLine($"Success: The category '{inputCategoryName}' has been removed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while attempting to delete the category: {ex.Message}");
            }
        }




        static void ModifyCategory(CategoryRepository categoryRepository)
        {
            Console.Write("\nEnter the name of the category you want to modify: ");

            string existingCategoryName = Console.ReadLine();


            var categoryToModify = categoryRepository.GetByName(existingCategoryName);

            if (categoryToModify == null)
            {
                Console.WriteLine("error: The specified category does not exist.");
                return;
            }

            Console.WriteLine("\n--- Modify Category Details ---");


            Console.Write("Enter a new name (or press Enter to retain the current name): ");

            string updatedName = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(updatedName))
            {
                categoryToModify.C_Name = updatedName;
            }


            Console.Write("Enter a new number of books (or press Enter to retain the current name): ");

            string updatedBookCountInput = Console.ReadLine();

            if (int.TryParse(updatedBookCountInput, out int updatedBookCount))
            {
                categoryToModify.C_NumberOfBooks = updatedBookCount;
            }

            try
            {

                categoryRepository.Update(categoryToModify);

                Console.WriteLine($"Success: The category '{existingCategoryName}' has been updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating the category: {ex.Message}");
            }
        }




        static void BorrowBook(User user, BookRepository bookRepo, BorrowingRepository borrowingRepo, CategoryRepository categoryRepo)
        {
            Console.WriteLine("\n=========================================");
            Console.WriteLine("         Available Books for Borrowing");
            Console.WriteLine("=========================================\n");


            var availableBooks = bookRepo.GetAll().Where(b => b.B_TotalCopies > b.B_BorrowedCopies).ToList();

            if (!availableBooks.Any())
            {
                Console.WriteLine("No books are currently available for borrowing.");
                return;
            }

            Console.WriteLine("{0,-5} | {1,-30} | {2,-20} | {3,-10} | {4,-15}",

                              "ID", "Name", "Author", "Copies", "Category");

            Console.WriteLine(new string('-', 80));

            foreach (var b in availableBooks)
            {
                var category = categoryRepo.GetById(b.C_ID);

                string categoryName = category != null ? category.C_Name : "N/A";

                Console.WriteLine("{0,-5} | {1,-30} | {2,-20} | {3,-10} | {4,-15}",
                                  b.Book_ID, b.B_Name, b.B_Author, b.B_TotalCopies - b.B_BorrowedCopies, categoryName);
            }

            Console.Write("\n Enter the Book ID to borrow: ");

            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                Console.WriteLine("error :invalid input. Please enter a valid numeric Book ID.");

                return;
            }

            var book = bookRepo.GetById(bookId);

            if (book == null)
            {
                Console.WriteLine("\nBook not found. Please select a valid Book ID from the list above.");
                return;
            }


            if (borrowingRepo.GetByUserId(user.U_ID).Any(b => b.Book_ID == bookId && !b.IsReturned))
            {
                Console.WriteLine("\nError: You are already borrowing this book and have not returned it.");
                return;
            }


            if ((book.B_TotalCopies - book.B_BorrowedCopies) <= 0)
            {
                Console.WriteLine("\nError: No available copies of this book for borrowing.");
                return;
            }

            try
            {
                var category = categoryRepo.GetById(book.C_ID);
                if (category == null)
                {
                    Console.WriteLine("\nError: Book does not have a valid category. Cannot proceed with borrowing.");
                    return;
                }

                var borrowing = new Borrowing
                {
                    U_ID = user.U_ID,
                    Book_ID = book.Book_ID,
                    B_BorrowingDate = DateTime.Now,
                    B_PredictedReturnDate = DateTime.Now.AddDays(book.B_BorrowingPeriod),
                    IsReturned = false,
                    B_ActualReturnDate = null,
                    Rating = null
                };

                borrowingRepo.Add(borrowing);


                book.B_BorrowedCopies++;
                bookRepo.Update(book);

                Console.WriteLine("\n=========================================");
                Console.WriteLine($"    Book '{book.B_Name}' Borrowed Successfully");
                Console.WriteLine("=========================================");
                Console.WriteLine($"Book ID   : {book.Book_ID}");
                Console.WriteLine($"Name      : {book.B_Name}");
                Console.WriteLine($"Author    : {book.B_Author}");
                Console.WriteLine($"Category  : {category.C_Name}");
                Console.WriteLine($"Borrow Date: {DateTime.Now:yyyy-MM-dd}");
                Console.WriteLine($"Return By : {DateTime.Now.AddDays(book.B_BorrowingPeriod):yyyy-MM-dd}");
                Console.WriteLine("=========================================");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
        }




        static void ReturnBook(int userId, BorrowingRepository borrowingRepo, BookRepository bookRepo, CategoryRepository categoryRepo)
        {
            Console.WriteLine("\n=========================================");
            Console.WriteLine("       Books You Are Currently Borrowing");
            Console.WriteLine("=========================================\n");


            var borrowedBooks = borrowingRepo.GetByUserId(userId)

                .Where(b => !b.IsReturned).Select(b => bookRepo.GetById(b.Book_ID)).Where(b => b != null).ToList();

            if (!borrowedBooks.Any())
            {
                Console.WriteLine("You are not currently borrowing any books.");

                return;
            }

            Console.WriteLine("{0,-5} | {1,-30} | {2,-20} | {3,-15}",
                              "ID", "Name", "Author", "Category");
            Console.WriteLine(new string('-', 80));

            foreach (var book in borrowedBooks)
            {
                var category = categoryRepo.GetById(book.C_ID);

                string categoryName = category != null ? category.C_Name : "N/A";

                Console.WriteLine("{0,-5} | {1,-30} | {2,-20} | {3,-15}",

                                  book.Book_ID, book.B_Name, book.B_Author, categoryName);
            }

            Console.Write("\nEnter Book ID to Return: ");

            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                Console.WriteLine("Invalid input. Please enter a valid numeric Book ID.");

                return;
            }

            try
            {
                var borrowing = borrowingRepo.GetByUserId(userId).FirstOrDefault(b => b.Book_ID == bookId && !b.IsReturned);

                if (borrowing == null)
                {
                    Console.WriteLine("\nNo active borrowing record found for this book.");

                    return;
                }

                borrowing.IsReturned = true;

                borrowing.B_ActualReturnDate = DateTime.Now;


                Console.Write("\n Enter your rating for this book (1 - 5): ");
                if (int.TryParse(Console.ReadLine(), out int rating) && rating >= 1 && rating <= 5)
                {
                    borrowing.Rating = rating;
                }
                else
                {
                    Console.WriteLine("Invalid rating. No rating recorded.");
                    borrowing.Rating = null;
                }

                borrowingRepo.Update(borrowing);

                var book = bookRepo.GetById(bookId);
                if (book != null)
                {
                    var category = categoryRepo.GetById(book.C_ID);

                    string categoryName = category != null ? category.C_Name : "N/A";


                    book.B_TotalCopies++;
                    book.B_BorrowedCopies--;
                    bookRepo.Update(book);

                    Console.WriteLine("\n=========================================");
                    Console.WriteLine($"    Book '{book.B_Name}' Returned Successfully");
                    Console.WriteLine("=========================================");
                    Console.WriteLine($"Book ID: {book.Book_ID}");
                    Console.WriteLine($"Name   : {book.B_Name}");
                    Console.WriteLine($"Author : {book.B_Author}");
                    Console.WriteLine($"Category: {categoryName}");
                    Console.WriteLine($"Return Date: {borrowing.B_ActualReturnDate:yyyy-MM-dd}");
                    if (borrowing.Rating != null)
                    {
                        Console.WriteLine($"Rating: {borrowing.Rating}/5");
                    }
                    Console.WriteLine("=========================================");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
        }



        static void SearchBooks(BookRepository bookRepository, CategoryRepository categoryRepository, User user, BorrowingRepository borrowingRepository)
        {
            Console.Write("\nSearch for a word or phrase in book titles: ");

            string searchQuery = Console.ReadLine()?.ToLower();

            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                Console.WriteLine("Error: Please enter a valid search term.");
                return;
            }


            var matchingBooks = bookRepository.GetAll().Where(book => book.B_Name.ToLower().Contains(searchQuery)).ToList();

            if (matchingBooks.Count == 0)
            {
                Console.WriteLine("No books found matching your search.");
            }
            else
            {
                Console.WriteLine("\n--- Search Results ---");
                Console.WriteLine("{0,-5} | {1,-30} | {2,-20} | {3,-10} | {4,-10} | {5,-10} | {6,-15} | {7,-5}",
                                  "ID", "Title", "Author", "Copies", "Borrowed", "Price", "Category", "Period");

                Console.WriteLine(new string('-', 110));

                foreach (var book in matchingBooks)
                {
                    var category = categoryRepository.GetById(book.C_ID);

                    string categoryName = category != null ? category.C_Name : "N/A";

                    Console.WriteLine("{0,-5} | {1,-30} | {2,-20} | {3,-10} | {4,-10} | {5,-10:F2} | {6,-15} | {7,-5} days",
                                      book.Book_ID, book.B_Name, book.B_Author, book.B_TotalCopies, book.B_BorrowedCopies, book.B_Price, categoryName, book.B_BorrowingPeriod);
                }

                Console.Write("\nWould you like to borrow a book from the results? (yes/no): ");

                string borrowChoice = Console.ReadLine()?.ToLower();

                if (borrowChoice == "yes")
                {
                    Console.Write("\nEnter the Book ID you wish to borrow: ");

                    if (!int.TryParse(Console.ReadLine(), out int selectedBookId))
                    {
                        Console.WriteLine("Error: Please enter a valid numeric Book ID.");

                        return;
                    }

                    var selectedBook = bookRepository.GetById(selectedBookId);

                    if (selectedBook != null && selectedBook.B_Name.ToLower().Contains(searchQuery))
                    {
                        if (selectedBook.B_TotalCopies > selectedBook.B_BorrowedCopies)
                        {
                            if (borrowingRepository.GetByUserId(user.U_ID).Any(b => b.Book_ID == selectedBookId && !b.IsReturned))
                            {

                                Console.WriteLine("error: You are already borrowing this book and must return it before borrowing again.");
                            }

                            else
                            {
                                try
                                {

                                    var newBorrowing = new Borrowing
                                    {
                                        U_ID = user.U_ID,
                                        Book_ID = selectedBook.Book_ID,

                                        B_BorrowingDate = DateTime.Now,

                                        B_PredictedReturnDate = DateTime.Now.AddDays(selectedBook.B_BorrowingPeriod),

                                        IsReturned = false,
                                        B_ActualReturnDate = null,
                                        Rating = null
                                    };

                                    borrowingRepository.Add(newBorrowing);


                                    selectedBook.B_TotalCopies--;

                                    selectedBook.B_BorrowedCopies++;

                                    bookRepository.Update(selectedBook);

                                    Console.WriteLine($"\nSuccess: The book '{selectedBook.B_Name}' has been borrowed.");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"An error occurred while borrowing the book: {ex.Message}");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("error: No available copies of the selected book.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("error: The selected Book ID is invalid or does not match the search results.");
                    }
                }
                else
                {
                    Console.WriteLine("No books were borrowed.");
                }
            }
        }







        static void CreateLibraryReport(BookRepository bookRepo, CategoryRepository categoryRepo, BorrowingRepository borrowingRepo)
        {

            var allBooks = bookRepo.GetAll();

            var allCategories = categoryRepo.GetAll();

            var allBorrowings = borrowingRepo.GetAll();


            int totalBooksCount = allBooks.Count();

            int totalCategoriesCount = allCategories.Count();

            int totalCopiesCount = allBooks.Sum(book => book.B_TotalCopies + book.B_BorrowedCopies);

            int borrowedBooksCount = allBooks.Sum(book => book.B_BorrowedCopies);

            int returnedBooksCount = allBorrowings.Count(borrowing => borrowing.IsReturned);


            Console.WriteLine("\n--- Library Report ---");



            Console.WriteLine($"- Total Books: {totalBooksCount}");

            Console.WriteLine($"- Total Categories: {totalCategoriesCount}");

            Console.WriteLine("\nCategory Details:");

            foreach (var category in allCategories)
            {
                int booksPerCategory = allBooks.Count(book => book.C_ID == category.C_ID);

                Console.WriteLine($"  - {category.C_Name}: {booksPerCategory} books");
            }

            Console.WriteLine("\n Overall Statistics:");

            Console.WriteLine($"- Total Copies (including borrowed): {totalCopiesCount}");

            Console.WriteLine($"- Total Borrowed Books: {borrowedBooksCount}");

            Console.WriteLine($"- Total Returned Books: {returnedBooksCount}");
        }


        static void ViewAllUsers(UserRepository userRepository)
        {
            Console.WriteLine("\n=========================================");
            Console.WriteLine("            List of All Users");
            Console.WriteLine("=========================================");

            var allUsers = userRepository.GetAll().ToList();
            if (!allUsers.Any())
            {
                Console.WriteLine("No users found in the system.");
            }
            else
            {
                Console.WriteLine("{0,-5} | {1,-20} | {2,-30} | {3,-10} | {4,-10}",
                                  "ID", "Name", "Email", "Gender", "Passcode");
                Console.WriteLine(new string('-', 80));

                foreach (var user in allUsers)
                {
                    Console.WriteLine("{0,-5} | {1,-20} | {2,-30} | {3,-10} | {4,-10}",
                                      user.U_ID, user.U_Name, user.U_Email,
                                      user.U_Gender.ToString(), user.U_Passcode);
                }
            }
            Console.WriteLine("=========================================");
        }




        static void RegisterNewUser(AdminRepository adminRepo)
        {
            Console.WriteLine("\n=========================================");
            Console.WriteLine("             Adding a New User");
            Console.WriteLine("=========================================");

            Console.Write("Enter Full Name: ");

            string userName = Console.ReadLine();

            Console.Write("Enter Email Address: ");

            string userEmail = Console.ReadLine();

            Console.Write("Enter Gender 0 for Female, 1 for Male: ");

            if (!int.TryParse(Console.ReadLine(), out int genderSelection) || (genderSelection != 0 && genderSelection != 1))
            {
                Console.WriteLine("Error: Please select 0 for Female or 1 for Male.");
                return;
            }
            Gender userGender = (Gender)genderSelection;

            Console.Write("Set a Password (at least 8 characters, include uppercase, lowercase, number, and symbol): ");
            string userPassword = Console.ReadLine();


            if (userPassword.Length < 8 || !userPassword.Any(char.IsUpper) || !userPassword.Any(char.IsLower) || !userPassword.Any(char.IsDigit) || !userPassword.Any(ch => "!@#$%^&*()-_=+[]{}".Contains(ch)))
            {
                Console.WriteLine("Error: Password must meet complexity requirements.");
                return;
            }

            try
            {

                var newUser = new User
                {
                    U_Name = userName,
                    U_Email = userEmail,
                    U_Gender = userGender,
                    U_Password = userPassword
                };


                adminRepo.AddUser(newUser);

                Console.WriteLine("\nSuccess: New user has been registered.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during registration: {ex.Message}");
            }
        }


        static void DeleteUser(AdminRepository adminRepository)
        {
            Console.WriteLine("\n=========================================");
            Console.WriteLine("            Deleting a User");
            Console.WriteLine("=========================================");

            Console.Write("Enter User ID to Delete: ");
            if (int.TryParse(Console.ReadLine(), out int userIdToDelete))
            {
                try
                {
                    adminRepository.DeleteUserById(userIdToDelete);
                    Console.WriteLine("User deleted successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid User ID.");
            }
        }
        static void UpdateUser(AdminRepository adminRepository)
        {
            Console.WriteLine("\n=========================================");
            Console.WriteLine("            Updating a User");
            Console.WriteLine("=========================================");

            Console.Write("Enter User Name to Update: ");
            string userNameToUpdate = Console.ReadLine();

            Console.WriteLine("Enter New User Details (leave blank to keep current values):");

            Console.Write("New Name: ");
            string updatedName = Console.ReadLine();

            Console.Write("New Email: ");
            string updatedEmail = Console.ReadLine();

            Console.Write("New Gender (0 for Female, 1 for Male): ");
            Gender? updatedGender = null;
            string genderUpdateInput = Console.ReadLine();
            if (int.TryParse(genderUpdateInput, out int genderUpdate) && (genderUpdate == 0 || genderUpdate == 1))
            {
                updatedGender = (Gender)genderUpdate;
            }

            Console.Write("New Passcode: ");
            string updatedPasscode = Console.ReadLine();

            Console.Write("New Password (leave blank to keep current): ");
            string updatedPassword = Console.ReadLine();

            // Validate new password complexity if provided
            if (!string.IsNullOrWhiteSpace(updatedPassword) &&
                (updatedPassword.Length < 8 ||
                !updatedPassword.Any(char.IsUpper) ||
                !updatedPassword.Any(char.IsLower) ||
                !updatedPassword.Any(char.IsDigit) ||
                !updatedPassword.Any(ch => "!@#$%^&*()-_=+[]{}".Contains(ch))))
            {
                Console.WriteLine("Password does not meet complexity requirements.");
                return;
            }

            try
            {
                var updatedUser = new User
                {
                    U_Name = string.IsNullOrWhiteSpace(updatedName) ? null : updatedName,
                    U_Email = string.IsNullOrWhiteSpace(updatedEmail) ? null : updatedEmail,
                    U_Gender = updatedGender ?? default,
                    U_Passcode = string.IsNullOrWhiteSpace(updatedPasscode) ? null : updatedPasscode,
                    U_Password = string.IsNullOrWhiteSpace(updatedPassword) ? null : updatedPassword
                };

                adminRepository.UpdateUser(userNameToUpdate, updatedUser);
                Console.WriteLine("User updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    
    }
}
