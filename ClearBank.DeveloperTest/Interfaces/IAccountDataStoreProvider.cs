using ClearBank.DeveloperTest.Types;


namespace ClearBank.DeveloperTest.Interfaces
{
    public interface IAccountDataStoreProvider
    {

        public Account GetAccount();

        public Account UpdateAccount(Account account);

        IAccountDataStoreProvider AccountDataStoreProvider(string type);

        Account GetAccount(object debtorAccountNumber);
    }
}