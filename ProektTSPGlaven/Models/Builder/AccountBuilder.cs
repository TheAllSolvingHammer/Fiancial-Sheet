using ProektTSPGlaven.Models.Database;

namespace ProektTSPGlaven.Models.Builder
{
    public class AccountBuilder
    {
        
        private readonly Database.Account account;

        public AccountBuilder()
        {
            account = new Database.Account();
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

        public AccountBuilder withUserId(int id)
        {
            account.userID = id;
            return this;
        }

        public Database.Account build()
        {
            return account;
        }

    }
}
