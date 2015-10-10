using System;

namespace ProjectManager.Domain
{
    public abstract class GuidEntity : Entity<Guid>
    {
        protected GuidEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
