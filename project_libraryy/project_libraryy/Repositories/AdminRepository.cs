using project_libraryy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_libraryy.Repositories
{
    public class AdminRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Admin> GetAll()
        {
            return _context.Admins.ToList();
        }


        public Admin GetByName(string name)
        {
            return _context.Admins.FirstOrDefault(a => a.A_Name == name);
        }


        public Admin GetByEmail(string email)
        {
            return _context.Admins.FirstOrDefault(a => a.A_Email == email);
        }


        public void Add(Admin admin)
        {
            _context.Admins.Add(admin);
            _context.SaveChanges();
        }


        public void UpdateByName(string name, Admin updatedAdmin)
        {
            var admin = GetByName(name);
            if (admin != null)
            {
                admin.A_Name = updatedAdmin.A_Name;
                admin.A_Email = updatedAdmin.A_Email;
                admin.A_Password = updatedAdmin.A_Password;
                _context.SaveChanges();
            }
        }


        public void DeleteById(int adminId)
        {
            var admin = _context.Admins.Find(adminId);
            if (admin != null)
            {
                _context.Admins.Remove(admin);
                _context.SaveChanges();
            }
        }

        public void AddUser(User user)
        {
            if (_context.Users.Any(u => u.U_Email == user.U_Email || u.U_Name == user.U_Name))
            {
                throw new InvalidOperationException("User with the same email or name already exists.");
            }

            _context.Users.Add(user);
            _context.SaveChanges();
        }


        public void DeleteUserById(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("User not found.");
            }
        }

        public void UpdateUser(string name, User updatedUser)
        {
            var user = _context.Users.FirstOrDefault(u => u.U_Name == name);
            if (user != null)
            {
                user.U_Name = updatedUser.U_Name ?? user.U_Name;
                user.U_Email = updatedUser.U_Email ?? user.U_Email;
                user.U_Gender = updatedUser.U_Gender;
                user.U_Passcode = updatedUser.U_Passcode ?? user.U_Passcode;
                user.U_Password = updatedUser.U_Password ?? user.U_Password;

                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("User not found.");
            }
        }


    }
}
