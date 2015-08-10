using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingKata
{
    public class ChequeWithdrawal : DebitEntry
    {
        protected readonly ChequeNumber ChequeNumber;

        public ChequeWithdrawal(DateTime transactionDate, Money transactionAmount, ChequeNumber chequeNumber) : base(transactionDate, transactionAmount)
        {
            ChequeNumber = chequeNumber;
        }

        public override string ToString()
        {
            return string.Format("CHQ {0} {1}", ChequeNumber.ToString(), GetBaseInformationString());
        }
    }
}
