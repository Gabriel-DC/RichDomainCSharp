using Flunt.Validations;

namespace Store.Domain.Entities
{
    public class Discount : Entity
    {
        public Discount(decimal amount, DateTime expireDate, string code)
        {
            Amount = amount;
            ExpireDate = expireDate;
            Code = code;
        }

        public string Code { get; set; }
        public decimal Amount { get; private set; }
        public DateTime ExpireDate { get; private set; }

        public bool IsExpired()
            => DateTime.Compare(DateTime.Now, ExpireDate) > 0;

        public decimal Value()
            => IsExpired() ? 0 : Amount;
    }
}