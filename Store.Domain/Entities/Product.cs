using System.ComponentModel.DataAnnotations;

namespace Store.Domain.Entities
{
    public class Product : Entity
    {
        public Product(string title, decimal price, bool active)
        {
            Title = title;
            Price = price;
            Active = active;
        }

        [Required(ErrorMessage = "Título obrigatório")]
        [StringLength(155, MinimumLength = 3, ErrorMessage = "Título do produto deve estar entre 3 e 155 caracteres")]
        public string Title { get; private set; }

        [Range(0.1, double.MaxValue, ErrorMessage = "O preço do produto deve ser maior ou igual a 0.1")]
        public decimal Price { get; private set; }
        public bool Active { get; private set; }
    }
}