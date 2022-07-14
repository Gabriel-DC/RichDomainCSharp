using System.ComponentModel.DataAnnotations;

namespace Store.Domain.Entities
{
    public abstract class Entity : DomainNotifiable
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}