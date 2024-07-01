using EcoBazzar.BindingModel.Category;
using EcoBazzar.DataModel;

namespace EcoBazzar.Services.CategoryServices
{
    public interface ICategoryServices
    {
        public Task<int> CreateCategory(CategoryBindinModel model);
        public Task<Category> UpdateCategory(CategoryBindinModel model,int id);
        public Task<string> DeleteCategory(int id);
        public Task<List<Category>> GetAllCategories();
        public Task<Category> GetCategoryById(int? id);
        public Task<Category> GetCategoryByName(string name);
    }
}
