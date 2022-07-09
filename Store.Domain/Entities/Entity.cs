using System.ComponentModel.DataAnnotations;

namespace Store.Domain.Entities
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

        private readonly List<ValidationResult> _validationResults = new();
        public bool IsValid => ValidationResults.Count == 0;

        public IReadOnlyCollection<ValidationResult> ValidationResults => _validationResults;

        public List<ValidationResult> Validate()
        {
            _validationResults.Clear();
            var contexto = new ValidationContext(this, null, null);
            Validator.TryValidateObject(this, contexto, _validationResults, true);
            return _validationResults;
        }
    }
}