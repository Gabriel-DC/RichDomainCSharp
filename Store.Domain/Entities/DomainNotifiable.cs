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
        public bool Valid => ValidationResults.Count == 0;

        public IReadOnlyCollection<ValidationResult> ValidationResults => _validationResults;

        public (List<ValidationResult> Results, bool IsValid) Revalidate()
        {
            _validationResults.Clear();
            var contexto = new ValidationContext(this, null, null);
            Validator.TryValidateObject(this, contexto, _validationResults, true);
            return (Results: _validationResults, IsValid: Valid);
        }

        public (List<ValidationResult> Results, bool IsValid) Validate()
        {
            var contexto = new ValidationContext(this, null, null);
            Validator.TryValidateObject(this, contexto, _validationResults, true);
            return (Results: _validationResults, IsValid: Valid);
        }

        public void AddNotification(string errorMessage)
            => _validationResults.Add(new ValidationResult(errorMessage));

        public void AddNotification(string errorMessage, string memberName)
            => _validationResults.Add(new ValidationResult(errorMessage, new List<string>() { memberName }));

        public virtual bool IsValid()
            => this.Validate().IsValid;

        public DomainNotifiable ClearNotifications()
        {
            _validationResults.Clear();
            return this;
        }


        public void ClearAllNotifications(params Entity[]? entities)
        {
            foreach (var entity in entities!)
                entity.ClearNotifications();
        }

        public void AddNotifications(List<ValidationResult> validations)
        {
            if (validations is not null)
                _validationResults.AddRange(validations);
        }

        public void AddNotifications(ICollection<ValidationResult> validations)
        {
            if (validations is not null)
                _validationResults.AddRange(validations);
        }

        public void AddNotifications(IReadOnlyCollection<ValidationResult> validations)
        {
            if (validations is not null)
                _validationResults.AddRange(validations);
        }

        public void AddNotifications(Entity entity)
        {
            if (entity is not null)
                AddNotifications(entity.Validate().Results);
        }

        public void ValidateEntities(params Entity[]? entities)
        {
            foreach (var entity in entities!)
                AddNotifications(entity);
        }

        public void AgroupNotifications(params Entity[]? entities)
        {
            foreach (var entity in entities!)
                AgroupNotifications(entity);
        }

        public void AgroupNotifications(Entity entity)
        {
            if (entity is not null)
                AddNotifications(entity.ValidationResults);
        }
    }
}