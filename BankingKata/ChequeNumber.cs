using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingKata
{
    public class ChequeNumber
    {
        private readonly int _chequeNumber;

        public ChequeNumber(int chequeNumber)
        {
            this._chequeNumber = chequeNumber;
        }

        public override string ToString()
        {
            return _chequeNumber.ToString("000000");
        }
    }
}
