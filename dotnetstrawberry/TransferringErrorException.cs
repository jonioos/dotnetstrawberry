using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetstrawberry
{
    class TransferringErrorException : Exception
    {
        /// <summary>
        /// Eccezione generata da un errato trasferimento del file
        /// </summary>
        /// <param name="message"></param>
        public TransferringErrorException(string message) : base(message){ }
    }
}
