using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Store.Domain.Commands.Interfaces
{
    public interface ICommand
    {
        ValidationResult Validate();
    }
}