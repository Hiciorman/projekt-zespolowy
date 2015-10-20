namespace ProjectManager.Domain
{
    public class Priority : IntEntity
    {
        public string Description { get; set; }
        public PriorityType Type { get; set; }
    }
}