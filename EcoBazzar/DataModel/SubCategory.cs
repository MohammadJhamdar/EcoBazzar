namespace EcoBazzar.DataModel
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
