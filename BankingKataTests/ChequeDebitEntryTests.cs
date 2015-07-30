using System;
using BankingKata;
using NUnit.Framework;

namespace BankingKataTests
{
    [TestFixture]
    class ChequeDebitEntryTests
    {
        [Test]
        public void WhenCachingInACheck_TheAmountShouldBeTakenOutOfTheAccount() {
            // ARRANGE
            var transactionLog = new Ledger();
            var expectedTotal = new Money(-1m);

            // ACT
            transactionLog.Record(new ChequeDebitEntry(DateTime.Now, new Money(1m), 10));
            
            // ASSERT
            var actualTotal = transactionLog.Accept(new BalanceCalculatingVisitor(), new Money(0m));
            Assert.That(actualTotal, Is.EqualTo(expectedTotal));
        }

        [Test]
        public void WhenCachingInTwoChecks_TheSUmOfTheAmountsShouldBeTakenOutOfTheAccount()
        {
            // ARRANGE
            var transactionLog = new Ledger();
            var expectedTotal = new Money(-3m);

            // ACT
            transactionLog.Record(new ChequeDebitEntry(DateTime.Now, new Money(1m), 10));
            transactionLog.Record(new ChequeDebitEntry(DateTime.Now, new Money(2m), 10));

            // ASSERT
            var actualTotal = transactionLog.Accept(new BalanceCalculatingVisitor(), new Money(0m));
            Assert.That(actualTotal, Is.EqualTo(expectedTotal));
        }


    }
}
