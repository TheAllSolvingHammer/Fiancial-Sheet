using ProektTSPGlaven.Models.Database;

namespace ProektTSPGlaven.Models.Builder
{
    public class AccountBuilder
    {

        private readonly Account account;

        public AccountBuilder()
        {
            account = new Account();
        }

        public AccountBuilder withName(string name)
        {
            account.name = name;
            return this;
        }

        public AccountBuilder withBalance(Decimal balance)
        {
            account.balance = balance;
            return this;
        }

        public AccountBuilder withUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            account.User = user;
            account.userID = user.userID;
            return this;

        }

        public Account build()
        {
            return account;
        }

    }
}
