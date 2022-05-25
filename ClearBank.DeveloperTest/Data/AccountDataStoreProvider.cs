using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Interfaces;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class AccountDataStoreProvider : IAccountDataStoreProvider
    {
        private IAccountDataStoreProvider accountDataStore;
        private object request;

        public AccountDataStoreProvider(string type)
        {
            if (type == "Backup")
            {
                accountDataStore = new BackupAccountDataStore();
            }
            else
            {
                accountDataStore = new AccountDataStore();
            }
        }

        public Account GetAccount()
        {
            return accountDataStore.GetAccount(request.DebtorAccountNumber);
        }

        public Account GetAccount(object debtorAccountNumber)
        {
            throw new NotImplementedException();
        }

        public Account UpdateAccount(Account account)
        {
            return accountDataStore.UpdateAccount(account);
        }

        IAccountDataStoreProvider IAccountDataStoreProvider.AccountDataStoreProvider(string type)
        {
            return accountDataStore.AccountDataStoreProvider(type);
        }
    }
}

