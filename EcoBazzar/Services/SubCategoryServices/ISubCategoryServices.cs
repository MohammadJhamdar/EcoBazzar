using EcoBazzar.BindingModel.SubCategory;
using EcoBazzar.DataModel;

namespace EcoBazzar.Services.SubCategoryServices
{
    public interface ISubCategoryServices
    {
        public Task<string> CreateSubCategory(SubCategoryBindingModel binding);
        public Task<SubCategory> UpdateSubCategory(SubCategoryBindingModel binding,int id);
        public Task<string> DeleteSubCategory(int id);
        public Task<SubCategory> GetSubCategory(int id);
        public Task<List<SubCategory>> GetAllSubCategories();
        public Task<List<SubCategory>> Filter(string? name,int? CategoryId);
    }
}
