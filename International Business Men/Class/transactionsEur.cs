using System.Text.Json.Serialization;

namespace International_Business_Men.Class
{
    public class transactionsEur :transactions
    {
        public string? error { get; set; }

        [JsonIgnore]
        public string currencyOriginal { get; set; }




        public transactionsEur Copy(transactions Transactions )
        {
            transactionsEur TransactionsEur = new transactionsEur();
            TransactionsEur.sku = Transactions.sku;
            TransactionsEur.currency = Transactions.currency;
            TransactionsEur.currencyOriginal = Transactions.currency;
            TransactionsEur.amount = Transactions.amount;
            return TransactionsEur;
        }
    }
}
