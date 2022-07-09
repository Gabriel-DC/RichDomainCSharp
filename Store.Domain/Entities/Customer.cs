namespace Store.Domain.Entities
{
    public class Customer : Entity
    {
        public Customer(string name, string email)
        {
            Name = name.Trim();
            Email = email.Trim();
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
    }
}