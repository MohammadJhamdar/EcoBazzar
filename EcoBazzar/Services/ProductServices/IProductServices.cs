using EcoBazzar.BindingModel.Product;
using EcoBazzar.DataModel;

namespace EcoBazzar.Services.ProductServices
{
    public interface IProductServices
    {
        public Task<string> CreateProduct(ProductBindingModel model);
        public Task<Product> UpdateProduct(ProductBindingModel model ,int id);
        public Task<Product>GetProductById(int id);
        public Task<List<Product>> GetAllProducts();
        public Task<string> DeleteProduct(int id);
        public Task<List<Product>> Filter(string? name,int? minprice, int? maxprice, double? rating,int? SubCategoryId);
    }
}
