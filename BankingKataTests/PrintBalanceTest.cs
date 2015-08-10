using BankingKata;
using NSubstitute;
using NUnit.Framework;
using System;
using System.IO;
using NUnit.Framework.Constraints;

namespace BankingKataTests
{
    [TestFixture]
    public sealed class PrintBalanceTest
    {

        private Account _account;
        private IPrinter _printer;
        private StringWriter _stringWriter;

        [SetUp]
        public void SetUp()
        {
            _account = new Account();

            _printer = new ConsolePrinter();
           
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
        }

        [Test]
        public void BalanceOfZeroIsPassedToThePrinter_ForANewAccount()
        {
            _printer = Substitute.For<IPrinter>();
            _account.PrintBalance(_printer);

            _printer.Received().PrintBalance(new Money(0m));
        }

        [Test]
        public void BalanceOfZeroIsPrinted_ForANewAccount()
        {
            _account.PrintBalance(_printer);

            var output = _stringWriter.GetStringBuilder();
            var expected = "Balance: £0.00";
            Assert.That(output.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void BalanceInThousandsIsPrinted()
        {
            _account.Deposit(DateTime.Now, new Money(1234.56m));

            _account.PrintBalance(_printer);

            var output = _stringWriter.GetStringBuilder();
            var expected = "Balance: £1,234.56";
            Assert.That(output.ToString(), Is.EqualTo(expected));

        }

        [Test]
        public void LastTransactionIsPrinted()
        {
            _account.Deposit(DateTime.Now, new Money(123m));
            _account.Deposit(DateTime.Now, new Money(456m));
            _account.Deposit(new DateTime(2015, 07, 13), new Money(789m));

            _account.PrintLastTransaction(_printer);

            var output = _stringWriter.GetStringBuilder();
            var expected = "Last transaction: DEP 13 Jul 2015 £789.00";
            Assert.That(output.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void CashWithdrawalIsPrinted()
        {
            _account.Withdraw(new DebitEntry(new DateTime(2015, 07, 13), new Money(123m)));

          
            _account.PrintLastTransaction(_printer);

            var output = _stringWriter.GetStringBuilder();
            var expected = "Last transaction: ATM 13 Jul 2015 (£123.00)";
            Assert.That(output.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void ChequeWithdrawalIsPrinted()
        {
            _account.Withdraw(new ChequeWithdrawal(new DateTime(2015, 07, 13), new Money(789m), new ChequeNumber(100)));

            _account.PrintLastTransaction(_printer);

            var output = _stringWriter.GetStringBuilder();
            var expected = "Last transaction: CHQ 000100 13 Jul 2015 (£789.00)";
            Assert.That(output.ToString(), Is.EqualTo(expected));
        }
    }
}
