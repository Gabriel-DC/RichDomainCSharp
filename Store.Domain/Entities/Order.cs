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
            Items = new List<OrderItem>();
        }

        [Required(ErrorMessage = "Cliente inválido")]
        public Customer Customer { get; private set; }
        public DateTime Date { get; private set; }
        public EOrderStatus Status { get; private set; }
        public decimal DeliveryFee { get; set; }
        public Discount? Discount { get; private set; }
        public string Number { get; private set; }

        [MinLength(1, ErrorMessage = "O pedido não possui itens")]
        public IList<OrderItem> Items { get; private set; }

        public void AddItem(Product product, int quantity)
        {
            var item = new OrderItem(product, quantity);
            if (item.IsValid())
                Items.Add(item);
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
