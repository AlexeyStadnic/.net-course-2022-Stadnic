using Models;
using Xunit;
using Xunit.Abstractions;

namespace ServiceTests
{
    public class ThreadAndTaskTests
    {
        private ITestOutputHelper _output;

        public Account account = new Account();
        public Currency currency = new Currency();

        public Object locker = new Object();
        
        public ThreadAndTaskTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void AccrualMoneyPositiveTest()
        {
            currency.Name = "USD";
            currency.Code = 840;
            account.Currency = currency;
            account.Amount = 0;

            var flowOne = new Thread(AccrualMoney);
            flowOne.Name = "flowOne";
            var flowTwo = new Thread(AccrualMoney);
            flowTwo.Name = "flowTwo";
            
            flowOne.Start();            
            flowTwo.Start();

            Thread.Sleep(50000);
            Assert.True(true);            
        }

        void AccrualMoney()
        {
            
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(100);
                lock (locker)
                {
                    account.Amount += 100;
                }
                _output.WriteLine($"{Thread.CurrentThread.Name}: {account.Amount}");
            }
        }
    }
}
