using System.ComponentModel.DataAnnotations;

namespace Store.Domain.Entities
{
    public class Discount : Entity
    {
        public Discount(decimal amount, DateTime expireDate)
        {
            Amount = amount;
            ExpireDate = expireDate;
        }

        [Required(ErrorMessage = "Valor de desconto obrigatório")]
        [Range(0.1, double.MaxValue, ErrorMessage = "O Valor do desconto deve ser maior ou igual que 0.1")]
        public decimal Amount { get; private set; }

        [Required(ErrorMessage = "Data de Expiração Inválida")]
        public DateTime ExpireDate { get; private set; }

        public bool IsExpired()
            => DateTime.Compare(DateTime.Now, ExpireDate) > 0;

        public decimal Value()
            => IsExpired() ? 0 : Amount;
    }
}