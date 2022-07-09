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

        public Product Product { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        public decimal Total()
            => Price * Quantity;
    }
}