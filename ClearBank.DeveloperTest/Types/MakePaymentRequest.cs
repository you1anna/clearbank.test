using System;
using ClearBank.DeveloperTest.Enums;

namespace ClearBank.DeveloperTest.Types
{
    public class MakePaymentRequest
    {
        public string CreditorAccountNumber { get; set; }

        public string DebtorAccountNumber { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public PaymentScheme PaymentScheme { get; set; }
        public AccountStatus Status { get; set; }
    }
}

