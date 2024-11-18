using project_libraryy.Models;
using project_libraryy.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_libraryy.Repositories
{
    public class CategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }


        public Category GetByName(string name)
        {
            return _context.Categories.FirstOrDefault(c => c.C_Name == name);
        }


        public void Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }


        public void Update(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }
        public Category GetById(int categoryId)
        {
            return _context.Categories.FirstOrDefault(c => c.C_ID == categoryId);
        }





        public void Delete(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }

    }
}
