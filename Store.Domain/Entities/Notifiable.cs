using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class DomainNotifiable
    {
        private readonly List<ValidationResult> _validationResults = new();
        private bool _isValid => ValidationResults.Count == 0;

        public IReadOnlyCollection<ValidationResult> ValidationResults => _validationResults;

        public (List<ValidationResult> Results, bool IsValid) Validate()
        {
            var contexto = new ValidationContext(this, null, null);
            Validator.TryValidateObject(this, contexto, _validationResults, true);
            return (Results: _validationResults, IsValid: _isValid);
        }

        public void AddNotification(string errorMessage)
            => _validationResults.Add(new ValidationResult(errorMessage));

        public void AddNotification(string errorMessage, string memberName)
            => _validationResults.Add(new ValidationResult(errorMessage, new List<string>() { memberName }));

        public virtual bool IsValid()
            => this.Validate().IsValid;

        public void ClearNotifications()
            => _validationResults.Clear();
    }
}