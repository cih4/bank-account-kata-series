using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingKata
{
    public class ChequeDebitEntry : DebitEntry
    {
        protected int chequeNumber;

        public ChequeDebitEntry(DateTime transactionDate, Money transactionAmount, int chequeNumber) : base(transactionDate, transactionAmount)
        {
            this.chequeNumber = chequeNumber;
        }

        public override string ToString()
        {
            return string.Format("CHQ {0} {1} ({2})", chequeNumber.ToString("000000") ,transactionDate.ToString("dd MMM yyyy"), transactionAmount);
        }
    }
}
