using International_Business_Men.Class;
using Newtonsoft.Json;

namespace International_Business_Men.business
{
    public  class business
    {


        private const string Transactions = "Files/transactions.json";
        private const string Rates = "Files/rates.json";
        public business()
        {
        }
        public  List<transactions> ReadTransactions()
        {
            using (StreamReader r = new StreamReader(Transactions))
            {
                string json = r.ReadToEnd();
                List<transactions> items = JsonConvert.DeserializeObject<List<transactions>>(json);

                if (items is null) throw new Exception("Error");

                return items;

            }
        }

        public List<rates> ReadRates()
        {

            using (StreamReader r = new StreamReader(Rates))
            {
                string json = r.ReadToEnd();
                List<rates> items = JsonConvert.DeserializeObject<List<rates>>(json);

                if (items is null) throw new Exception("Error");

                return items;

            }

        }


        public  List<transactionsEur> DetailSKu()
        {
            List<transactions> transactions = ReadTransactions();
            List<rates> rates = ReadRates();
            List<transactionsEur> transactionsEur = new List<transactionsEur>();
                        foreach (transactions trans in transactions)
            {
                transactionsEur TransactionsEur = new transactionsEur();
                  
                transactionsEur transEur = Conversor(TransactionsEur.Copy(trans), rates);
                if (!string.IsNullOrEmpty(transEur.sku))
                { transactionsEur.Add(transEur);
                }

            }
            List<string> Skus = transactionsEur.Select(f => f.sku).Distinct().ToList();
            List<transactionsEur> ListFinishedEur = new List<transactionsEur>();
            foreach (string Sku in Skus)
            {
                transactionsEur FinishEur = new transactionsEur();
                FinishEur.sku = Sku;
                FinishEur.amount = (float)Math.Round(transactionsEur.Where(f => f.sku.Equals(Sku) &&  string.IsNullOrEmpty(f.error)).Sum(f => (f.amount)),2);
                FinishEur.currency = "EUR";
                ListFinishedEur.Add(FinishEur);
            }

            ListFinishedEur.AddRange(transactionsEur.Where(f => !string.IsNullOrEmpty(f.error)).ToList());

            return ListFinishedEur;
        }

        private  transactionsEur Conversor(transactionsEur Transaction, List<rates> Rates)

        {
            transactionsEur TransactionsEur = new transactionsEur();

            
            foreach (rates rate in Rates)
            {
                if (Transaction.currency.Equals("EUR"))
                {
                    return Transaction;

                }
                else if (rate.from.Equals(Transaction.currency) && rate.to.Equals("EUR") )
                {
                    TransactionsEur = Transaction;
                    TransactionsEur.amount = Transaction.amount * rate.rate;
                    return Transaction;

                }
                else if (rate.from.Equals(Transaction.currency))
                {
                    TransactionsEur.amount = Transaction.amount * rate.rate;
                    TransactionsEur.currency = rate.to;
                    if (string.IsNullOrEmpty(TransactionsEur.currencyOriginal))
                    {
                        TransactionsEur.currencyOriginal = Transaction.currencyOriginal;
                    }
                    TransactionsEur.sku = Transaction.sku;
                    TransactionsEur = Conversor(TransactionsEur, Rates);
                    return TransactionsEur;
                }
              
            }

              if (string.IsNullOrEmpty(TransactionsEur.currency))
            {
                TransactionsEur.sku = Transaction.sku;
                TransactionsEur.amount = 0;
                if (!string.IsNullOrEmpty(Transaction.currencyOriginal))
                {
                    TransactionsEur.currency = Transaction.currencyOriginal;
                }
                else
                {
                    TransactionsEur.currency = Transaction.currency;
                }
                TransactionsEur.error = "No se encontro transformación";
                return TransactionsEur;
            }

            return TransactionsEur;

        }
    }
}

