namespace EcoBazzar.DataModel
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Image {  get; set; }
        public string Brand { get; set; }
        public double Rating { get; set; }
        public double Weight { get; set; }
        public int Stock { get; set; }
        public double Discount { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateModified { get; set; }
        public SubCategory SubCategory { get; set; }
        public int SubCategoryId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }

    }
}
