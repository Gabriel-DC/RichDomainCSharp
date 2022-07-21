using System.ComponentModel.DataAnnotations;

namespace Store.Domain.Entities
{
    public class Customer : Entity
    {
        public Customer(string name, string email)
        {
            Name = name?.Trim();
            Email = email?.Trim();
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Nome obrigatório")]
        public string? Name { get; private set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string? Email { get; private set; }

        public string? Document { get; private set; }


        public void SetEmail(string email)
        {
            Email = email?.Trim();
        }
    }
}