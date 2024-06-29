using EcoBazzar.BindingModel.SubCategory;
using EcoBazzar.DataBase;
using EcoBazzar.DataModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace EcoBazzar.Services.SubCategoryServices
{
    public class SubCategoryServices : ISubCategoryServices
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _context;
        public SubCategoryServices(IWebHostEnvironment webHostEnvironment, AppDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        public async Task<string> CreateSubCategory(SubCategoryBindingModel binding)
        {
            var subCategory = new SubCategory();
            MapToDataModel(binding, subCategory);
            await _context.AddAsync(subCategory);
            await _context.SaveChangesAsync();
            return subCategory.Name;
        }

        public async Task<string> DeleteSubCategory(int id)
        {
           var subCategory=await _context.subcategories.FindAsync(id);
            if (subCategory != null)
            {
                _context.Remove(subCategory);
                await _context.SaveChangesAsync();
                return subCategory.Name;
            }
            return null;
        }

        public async Task<List<SubCategory>> Filter(string? name, int? CategoryId)
        {
            var query = _context.subcategories.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(sc => sc.Name.Contains(name));
            }

            if (CategoryId.HasValue)
            {
                query = query.Where(sc => sc.CategoryId == CategoryId.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<List<SubCategory>> GetAllSubCategories()
        {
            return await _context.subcategories.ToListAsync();
        }

        public async Task<SubCategory> GetSubCategory(int id)
        {
            var subCategory = await _context.subcategories.FindAsync(id);
            if (subCategory != null)
            {
                return subCategory;
            }
            return null;
        }

        public async Task<SubCategory> UpdateSubCategory(SubCategoryBindingModel binding, int id)
        {
            var subCategory = await _context.subcategories.FindAsync(id);
            if (subCategory != null)
            {
                MapToDataModel(binding, subCategory);
                await _context.SaveChangesAsync();
                return subCategory;
            }
            return null;
        }

        private void MapToDataModel(SubCategoryBindingModel model,SubCategory subCategory) {
            subCategory.Name = model.Name;
            subCategory.Description = model.Description;
            subCategory.CategoryId= model.CategoryId;
            if (model.Image != null)
            {
                var uniqueFileName = GetUniqueFileName(model.Image.FileName);
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "SubCategory");
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(stream);
                }
                subCategory.Image = "/SubCategory/" + uniqueFileName;
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
