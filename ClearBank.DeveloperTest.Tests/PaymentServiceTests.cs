using Xunit;
using Moq;
using FluentAssertions;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Interfaces;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Enums;

namespace ClearBank.DeveloperTest.Tests
{
    public class PaymentServiceTests
    {
        private readonly Mock<IAccountDataStoreProvider> _mockAccountDataStore;
        private readonly Mock<IBackupAccountDataStore> _mockDataStoreFactory;
        private readonly IPaymentService _paymentService;

        public PaymentServiceTests()
        {
            _mockAccountDataStore = new Mock<IAccountDataStoreProvider>();
            _mockDataStoreFactory = new Mock<IBackupAccountDataStore>();
            _paymentService = new PaymentService();
        }

        // Scenario: Happy path
        //    Fixture<accountstore>[backupaccountstore, accountdatastore]
        //    Fixture<paymentType>[
        //        bacs,
        //        fasterpayment,
        //        chaps]

        [Fact]
        public void testFasterPayment()
        {

            // Arrange
            var request = new MakePaymentRequest()
            {
                CreditorAccountNumber = "5000",
                DebtorAccountNumber = "3753757",
                Amount = 10,
                PaymentDate = DateTime.Now,
                PaymentScheme = PaymentScheme.FasterPayments
            };

            // Act
            var result = _paymentService.MakePayment(request);

            // Assert
            result.Success.Should().Be(true);

        }

        [Fact]
        public void testBacsPayment()
        {
            var request = new MakePaymentRequest()
            {
                CreditorAccountNumber = "5000",
                DebtorAccountNumber = "3753757",
                Amount = 10,
                PaymentDate = DateTime.Now,
                PaymentScheme = PaymentScheme.Bacs
            };

            var result = _paymentService.MakePayment(request);

            result.Success.Should().Be(true);
        }


        [Fact]
        public void testChapsPayment()
        {
            var request = new MakePaymentRequest()
            {
                CreditorAccountNumber = "5000",
                DebtorAccountNumber = "3753757",
                Amount = 10,
                PaymentDate = DateTime.Now,
                PaymentScheme = PaymentScheme.Chaps
            };

            var result = _paymentService.MakePayment(request);

            result.Success.Should().Be(true);
        }

        // given valid <paymentType> account in <accountstore>
        //    with payment flag Chaps
        //    with 100
        //    with status Disabled
        // when payment with <paymentType> for 25
        // then success should be false and balance should be 100

        [Fact]
        public void testChapsStatusDisabled()
        {
            var request = new MakePaymentRequest()
            {
                CreditorAccountNumber = "5000",
                DebtorAccountNumber = "3753757",
                Amount = 100,
                PaymentDate = DateTime.Now,
                PaymentScheme = PaymentScheme.Chaps,
                Status = AccountStatus.Disabled
            };

            var result = _paymentService.MakePayment(request);

            result.Success.Should().Be(false);
        }

        // given valid<paymentType> account in <accountstore>
        //    with payment flag
        //    with 100
        //    without live flag
        // when payment with <paymentType> for 25
        // then success should be true and balance should be 75

        [Fact]
        public void testChapsStatusLive()
        {
            var request = new MakePaymentRequest()
            {
                CreditorAccountNumber = "5000",
                DebtorAccountNumber = "3753757",
                Amount = 100,
                PaymentDate = DateTime.Now,
                PaymentScheme = PaymentScheme.Chaps,
                Status = AccountStatus.Live
            };

            var result = _paymentService.MakePayment(request);

            result.Success.Should().Be(false);
        }

        // Scenario: null account
        //  given no account
        //  when payment with[bacs, fasterpayment, chaps]
        //  then success should be false

        [Fact]
        public void handleNullAccount()
        {
            var result = _paymentService.MakePayment(null);

            result.Success.Should().Be(false);
        }

        // Scenario: negative cases
        // - Payment Type that doesn't exist
        // - Payment amount that is negative, huge payment 2^128, decimal < 0.01

    }

}
