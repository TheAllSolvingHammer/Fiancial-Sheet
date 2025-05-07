using ProektTSPGlaven.Models.Database;

namespace ProektTSPGlaven.Models.Builder
{
    public class TransactionBuilder
    {
        private readonly TransactionEntity transactionEntity;

        public TransactionBuilder()
        {
            transactionEntity = new TransactionEntity();
        }

        public TransactionBuilder withAccount(Database.Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }
            transactionEntity.account = account;
            return this;
        }

        public TransactionBuilder withAmount(Decimal amount)
        {
            transactionEntity.amount = amount;
            return this;
        }

        public TransactionBuilder withType(TransactionType transactionType)
        {
            transactionEntity.type = transactionType;
            return this;
        }

        public TransactionBuilder withCategory(string category)
        {
            transactionEntity.category = category;
            return this;
        }

        public TransactionBuilder withDescription(string description)
        {
            transactionEntity.description = description;
            return this;
        }

        public TransactionEntity build()
        {
            transactionEntity.createdAt = DateTime.Now;
            return transactionEntity;
        }

    }
}
