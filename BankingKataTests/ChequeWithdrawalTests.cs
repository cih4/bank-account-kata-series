using System;
using BankingKata;
using NUnit.Framework;

namespace BankingKataTests
{
    [TestFixture]
    class ChequeWithdrawalTests
    {
        [Test]
        public void WhenCashingInACheque_TheAmountShouldBeTakenOutOfTheAccount() {
            // ARRANGE
            var transactionLog = new Ledger();
            var expectedTotal = new Money(-1m);

            // ACT
            transactionLog.Record(new ChequeWithdrawal(DateTime.Now, new Money(1m), new ChequeNumber(10)));
            
            // ASSERT
            var actualTotal = transactionLog.Accept(new BalanceCalculatingVisitor(), new Money(0m));
            Assert.That(actualTotal, Is.EqualTo(expectedTotal));
        }

        [Test]
        public void WhenCashingInTwoCheques_TheSUmOfTheAmountsShouldBeTakenOutOfTheAccount()
        {
            // ARRANGE
            var transactionLog = new Ledger();
            var expectedTotal = new Money(-3m);

            // ACT
            transactionLog.Record(new ChequeWithdrawal(DateTime.Now, new Money(1m), new ChequeNumber(10)));
            transactionLog.Record(new ChequeWithdrawal(DateTime.Now, new Money(2m), new ChequeNumber(10)));

            // ASSERT
            var actualTotal = transactionLog.Accept(new BalanceCalculatingVisitor(), new Money(0m));
            Assert.That(actualTotal, Is.EqualTo(expectedTotal));
        }

        [Test]
        public void ChequeApplyTo()
        {
            var chequeWithdrawal = new ChequeWithdrawal(new DateTime(2001, 1, 1), new Money(1m), new ChequeNumber(10));
            var chequeWithdrawalString = chequeWithdrawal.ToString();
            Assert.That(chequeWithdrawalString, Is.EqualTo("CHQ 000010 01 Jan 2001 (£1.00)"));
        }
    }
}
