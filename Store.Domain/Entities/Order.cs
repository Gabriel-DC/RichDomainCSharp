using System.ComponentModel.DataAnnotations;
using Store.Domain.Enums;

namespace Store.Domain.Entities
{
    public class Order : Entity
    {
        public Order(Customer customer, decimal deliveryFee, Discount? discount)
        {
            Customer = customer;
            Date = DateTime.Now;
            Number = Guid.NewGuid().ToString().Substring(0, 8);
            Status = EOrderStatus.WaitingPayment;
            DeliveryFee = deliveryFee;
            Discount = discount;                      
        }

        [Required(ErrorMessage = "Cliente inválido")]
        public Customer Customer { get; private set; }
        public DateTime Date { get; private set; }
        public EOrderStatus Status { get; private set; }
        public decimal DeliveryFee { get; private set; }
        public Discount? Discount { get; private set; }
        public string Number { get; private set; }

        private readonly List<OrderItem> _items = new();

        [MinLength(1, ErrorMessage = "O pedido não possui itens")]
        public IReadOnlyCollection<OrderItem> Items => _items;

        public void AddItem(Product product, int quantity)
        {
            var item = new OrderItem(product, quantity);
            if (item.IsValid())
                _items.Add(item);
        }

        public decimal Total()
        {
            decimal total = 0;
            foreach (var item in Items)
                total += item.Total();

            total += DeliveryFee;
            total -= Discount != null ? Discount.Value() : 0;

            return total;
        }

        public void Pay(decimal amount)
        {
            if (amount == Total())
                Status = EOrderStatus.WaitingDelivery;
        }

        public void Cancel()
        {
            Status = EOrderStatus.Canceled;
        }
    }
}
