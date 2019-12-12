using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellersWebMVC.Services.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message ) : base(message)
        {

        }
    }
}
/*
 * Para criar a possibilidade de exibir um erro personalizado
 */