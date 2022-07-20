using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Domain.Commands.Interfaces
{
    public interface ICommandResult
    {
        bool Success { get; set; }
        string Message { get; set; }
        object Data { get; set; }
    }
}