using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Domain
{
    public abstract class Entity<T>
    {
        [Key]
        public T Id { get; set; }
    }
}