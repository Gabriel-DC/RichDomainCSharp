using System.ComponentModel.DataAnnotations;
using Flunt.Notifications;

namespace Store.Domain.Entities
{
    public abstract class Entity : Notifiable<Notification>
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}