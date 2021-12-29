using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication
{
    public enum TransactionType
    {
        Debit,
        Credit,
        Charge
    }
    public class Transaction
    {
        private string _transactionId;
        private decimal _amount;
        private DateTime _time;
        private decimal _currentBalance;
        private TransactionType _type;

        public string TransactionId
        {
            get
            {
                return _transactionId;
            }
        }
        public decimal Amount
        {
            get
            {
                return _amount;
            }
        }
        public DateTime Time
        {
            get
            {
                return _time;
            }
        }
        public decimal CurrentBalance
        {
            get
            {
                return _currentBalance;
            }
        }
        public TransactionType Type
        {
            get
            {
                return _type;
            }
        }

        public Transaction(decimal amount , decimal currentBalance , TransactionType type)
        {
            _transactionId = Guid.NewGuid().ToString();
            _amount = amount;
            _time = DateTime.Now;
            _currentBalance = currentBalance;
            _type = type;
        }
    }
}
