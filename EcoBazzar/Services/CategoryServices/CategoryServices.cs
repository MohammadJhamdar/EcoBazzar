using EcoBazzar.BindingModel.Category;
using EcoBazzar.DataBase;
using EcoBazzar.DataModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace EcoBazzar.Services.CategoryServices
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _context;
        public CategoryServices(IWebHostEnvironment webHostEnvironment, AppDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        public async Task<int> CreateCategory(CategoryBindinModel model)
        {
            var category = new Category();  
            MapToDataModel(model, category);
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();
            return category.Id;
        }

        public async Task<string> DeleteCategory(int id)
        {
            var category= await _context.categories.FindAsync(id);
            if (category != null)
            {
                _context.Remove(category);
                await _context.SaveChangesAsync();
                return category.Name;
            }
            return null;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int? id)
        {
            var category = await _context.categories.FindAsync(id);
            if (category != null)
            {
                
                return category;
            }
            return null;
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            var category=await _context.categories.SingleOrDefaultAsync(c=> c.Name == name);
            if(category != null)
            {
                return category;
            }
            return null;
        }

        public async Task<Category> UpdateCategory(CategoryBindinModel model, int id)
        {
            var category= await _context.categories.FindAsync(id);
            if(category != null)
            {
                MapToDataModel(model, category);
                await _context.SaveChangesAsync();
            }
            return null;
            
        }

        private void MapToDataModel(CategoryBindinModel bindinModel, Category category)
        {
            category.Name = bindinModel.Name;
            category.Description = bindinModel.Description;
            if (bindinModel.Image != null)
            {
                var uniqueFileName = GetUniqueFileName(bindinModel.Image.FileName);
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Category");
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    bindinModel.Image.CopyTo(stream);
                }
                category.Image = "/Category/" + uniqueFileName;
            }
            
        }



        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
            + "_"
            + Guid.NewGuid().ToString().Substring(0, 4)
            + Path.GetExtension(fileName);
        }
    }
}
