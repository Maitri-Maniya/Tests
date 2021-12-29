using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication
{
    public enum AccountType
    {
        Saving,
        Current
    }
    public class Account
    {
        private string _accountNumber;
        private decimal _balance;
        private AccountType _type;
        private List<Transaction> _transactions;

        public const decimal MAX_WITHDRAW = 50000;
        public const decimal HOURLY_WITHDRAW_LIMIT = 2000000;
        public const decimal SERVICE_CHAREG_AMOUNT = 30;
        public const decimal SERVICE_CHAREG_APPLY = 30000;
        public const int HOURLY_TRANSACTION_LIMIT = 4;
        public const decimal MINIMUM_DEPOSITE = 1000;

        public AccountType Type
        {
            get
            {
                return _type;
            }
        }
        public string AccountNumber
        {
            get
            {
                return _accountNumber;
            }
        }
        public decimal Balance
        {
            get
            {
                return _balance;
            }
        }
        public List<Transaction> Transactions
        {
            get
            {
                return _transactions;
            }
        }

        public Account(AccountType type , decimal amount)
        {
            _accountNumber = Guid.NewGuid().ToString();
            _type = type;
            _transactions = new List<Transaction>();
            this.MakeTransaction(amount , TransactionType.Credit);
        }

        public void Credit()
        {
            DateTime currTime = DateTime.Now;
            int hourlyTansactions = 0;
            for (int i = ( this.Transactions.Count - 1 ) ; i > 0 ; i--)
            {
                TimeSpan ts = currTime - this.Transactions[i].Time;
                if (ts.TotalHours > 1) break;
                if (this.Transactions[i].Type != TransactionType.Charge)
                    hourlyTansactions++;
            }
            if (hourlyTansactions >= HOURLY_TRANSACTION_LIMIT)
            {
                Console.WriteLine($"\nYou have excceded the transaction limit. you can make only {HOURLY_TRANSACTION_LIMIT} transactions per hour");
                return;
            }

            Console.Write("Amount:");
            string input = Console.ReadLine();
            decimal amount = 0;
            if (!( decimal.TryParse(input , out amount) ))
            {
                Console.WriteLine("Amount must be numeric");
                return;
            }
            if (amount <= 0)
            {
                Console.WriteLine("Amount must be greater than 0");
                return;
            }
            if (amount % 100 != 0)
            {
                Console.WriteLine($"Amount must be in multiple of 100");
                return;
            }
            this.MakeTransaction(amount , TransactionType.Credit);
            Console.WriteLine("Amount is credited successfully");
            Console.WriteLine($"your updated balance is: {this.Balance}");
        }
        public void Debit()
        {
            DateTime currTime = DateTime.Now;
            decimal HourlyWithDrawAmount = 0;
            int hourlyTansactions = 0;
            for (int i = ( this.Transactions.Count - 1 ) ; i > 0 ; i--)
            {
                TimeSpan ts = currTime - this.Transactions[i].Time;
                if (ts.TotalHours > 1) break;
                if (this.Transactions[i].Type == TransactionType.Debit)
                    HourlyWithDrawAmount += this.Transactions[i].Amount;
                if (this.Transactions[i].Type != TransactionType.Charge)
                    hourlyTansactions++;
            }
            if (hourlyTansactions >= HOURLY_TRANSACTION_LIMIT)
            {
                Console.WriteLine($"You have excceded the transaction limit. you can make only {HOURLY_TRANSACTION_LIMIT} transactions per hour");
                return;
            }
            Console.Write("Amount:");
            string input = Console.ReadLine();
            decimal amount = 0;
            if (!( decimal.TryParse(input , out amount) ))
            {
                Console.WriteLine("Amount must be numeric");
                return;
            }
            if (amount <= 0)
            {
                Console.WriteLine("Amount must be greater than 0");
                return;
            }
            if (amount > this.Balance)
            {
                Console.WriteLine("You do not have sufficient balance in your account");
                return;
            }
            if (amount % 100 != 0)
            {
                Console.WriteLine($"Amount must be in multiple of 100");
                return;
            }
            if (amount >= MAX_WITHDRAW)
            {
                Console.WriteLine($"You have exceeded the transaction limit. Maximam withdraw limit is {MAX_WITHDRAW} per transaction");
                return;
            }
            if (HourlyWithDrawAmount > HOURLY_WITHDRAW_LIMIT)
            {
                Console.WriteLine($"You have exceeded the transaction limit. Maximam withdraw limit is {HOURLY_WITHDRAW_LIMIT} per hour");
                return;
            }
            this.MakeTransaction(amount , TransactionType.Debit);
            if (amount >= SERVICE_CHAREG_APPLY)
            {
                Console.WriteLine($"Service charge of {SERVICE_CHAREG_AMOUNT} will be applied if the withdrawal amount is more than {SERVICE_CHAREG_APPLY}");
                this.MakeTransaction(SERVICE_CHAREG_AMOUNT , TransactionType.Charge);
            }
            Console.WriteLine("Amount is debited successfully");
            Console.WriteLine($"your updated balance is:{this.Balance}");
        }
        public void AccountStatement()
        {
            if (this.Transactions.Count == 0)
            {
                Console.WriteLine("No transactions");
                return;
            }
            Console.WriteLine("\tTransaction Id\t\t\t\tTime\t\tAmount\tType\tBalance");

            for (int i = ( this.Transactions.Count - 1 ) ; i >= 0 ; i--)
            {
                Transaction currentTransaction = this.Transactions[i];
                Console.WriteLine($"{currentTransaction.TransactionId}\t{currentTransaction.Time}\t{currentTransaction.Amount}\t{currentTransaction.Type}\t{currentTransaction.CurrentBalance}");
            }
        }
        public void MakeTransaction(decimal amount , TransactionType transactionType)
        {
            this._balance = ( transactionType == TransactionType.Debit || transactionType == TransactionType.Charge ) ? ( this._balance - amount ) : ( this._balance + amount );
            Transaction newTransaction = new Transaction(amount , this._balance , transactionType);
            this._transactions.Add(newTransaction);
        }
    }

}
