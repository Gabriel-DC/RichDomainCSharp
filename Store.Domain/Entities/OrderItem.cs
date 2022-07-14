using System.ComponentModel.DataAnnotations;

namespace Store.Domain.Entities
{
    public class OrderItem : Entity
    {
        public OrderItem(Product product, int quantity)
        {
            Product = product;
            Price = product is not null ? product.Price : 0;
            Quantity = quantity;
        }

        [Required(ErrorMessage = "Produto obrigatório")]
        public Product Product { get; private set; }

        public decimal Price { get; private set; }

        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior ou igual a 1")]
        public int Quantity { get; private set; }

        public decimal Total() => Price * Quantity;
    }
}
