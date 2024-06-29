using EcoBazzar.BindingModel.Product;
using EcoBazzar.DataBase;
using EcoBazzar.DataModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace EcoBazzar.Services.ProductServices
{
    public class ProductServices : IProductServices
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _context;
        
        public ProductServices(AppDbContext context,IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }
        public async Task<string> CreateProduct(ProductBindingModel model)
        {
            Product product= new Product();
            MapToDataModel(model, product);
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
            return product.Name;
        }

        public async Task<string> DeleteProduct(int id)
        {
            var product = await _context.products.FindAsync(id);
            if(product !=null)
            {
                _context.products.Remove(product);
                await _context.SaveChangesAsync();
                return product.Name;
            }
            return null;
        }


        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.products.ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _context.products.FindAsync(id);
            if (product != null)
            {
                
                return product;
            }
            return null;
        }

        public async Task<List<Product>> Filter(string? name, int? minprice, int? maxprice, double? rating, int? SubCategoryId)
        {
            var query = _context.products.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            if (minprice.HasValue)
            {
                query = query.Where(p => p.Price >= minprice.Value);
            }

            if (maxprice.HasValue)
            {
                query = query.Where(p => p.Price <= maxprice.Value);
            }

            if (rating.HasValue)
            {
                query = query.Where(p => p.Rating >= rating.Value);
            }

            if (SubCategoryId.HasValue)
            {
                query = query.Where(p => p.SubCategoryId == SubCategoryId.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<Product> UpdateProduct(ProductBindingModel model, int id)
        {
            var product = await _context.products.FindAsync(id);
            if (product != null)
            {
               MapToDataModelv2(model, product);
                await _context.SaveChangesAsync();
                return product;
            }
            return null;
        }


        private void MapToDataModel(ProductBindingModel model,Product product)
        {
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Rating = model.Rating;
            product.DateAdded = DateTime.Today;
            product.Brand= model.Brand;
            product.Weight = model.Weight;
            product.SubCategoryId= model.SubCategoryId;
            product.Discount= model.Discount;
            if (model.Image != null)
            {
                var uniqueFileName = GetUniqueFileName(model.Image.FileName);
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Products");
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(stream);
                }
                product.Image = "/Products/" + uniqueFileName;
            }


        }
        private void MapToDataModelv2(ProductBindingModel model, Product product)
        {
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Rating = model.Rating;
            product.DateModified = DateTime.Today;
            product.Brand = model.Brand;
            product.Weight = model.Weight;
            product.SubCategoryId = model.SubCategoryId;
            product.Discount = model.Discount;
            if (model.Image != null)
            {
                var uniqueFileName = GetUniqueFileName(model.Image.FileName);
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Products");
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(stream);
                }
                product.Image = "/Products/" + uniqueFileName;
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
