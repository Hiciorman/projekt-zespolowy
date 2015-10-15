namespace ProjectManager.Domain
{
    public class Status : IntEntity
    {
        public string Description { get; set; }
        public StatusType Type { get; set; }
    }
}