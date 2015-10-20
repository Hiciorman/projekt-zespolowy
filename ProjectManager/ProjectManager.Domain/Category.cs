namespace ProjectManager.Domain
{
    public class Category : IntEntity
    {
        public string Description { get; set; }
        public CategoryType Type { get; set; }
    }
}