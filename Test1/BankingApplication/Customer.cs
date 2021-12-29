using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BankingApplication
{
    class Customer
    {
        private string _customerId;
        private string _firstName;
        private string _lastName;
        private List<Account> _accounts;

        public string CustomerId
        {
            get
            {
                return _customerId;
            }
        }
        public string FirstName
        {
            get
            {
                return _firstName;
            }


        }
        public string LastName
        {
            get
            {
                return _lastName;
            }

        }
        public string FullName
        {
            get
            {
                return _firstName + " " + _lastName;
            }

        }
        public List<Account> Accounts
        {
            get
            {
                return _accounts;
            }

        }

        public Customer(string firstName, string lastName)
        {
            _customerId = Guid.NewGuid().ToString();
            _firstName = firstName;
            _lastName = lastName;
            _accounts = new List<Account>();
        }

        public void AddAccount(Dictionary<string, Customer> customers, Dictionary<string, Account> accounts)
        {
            Console.WriteLine("\nSelect account type:");
            Console.WriteLine("1.Saving");
            Console.WriteLine("2.Current");

            while (true)
            {
                string accountType = Console.ReadLine();
                AccountType type;
                if (accountType != "1" && accountType != "2")
                {
                    Console.WriteLine("Please enter corret choice");
                    continue;
                }
                type = accountType == "1" ? AccountType.Saving : AccountType.Current;
                Console.WriteLine("Enter amount");
                while (true)
                {
                    string input = Console.ReadLine();
                    decimal amount = 0;


                    if (!( decimal.TryParse(input, out amount) ))
                    {
                        Console.WriteLine("Amount must be numeric");
                        continue;
                    }
                    if (amount <= 0)
                    {
                        Console.WriteLine("Amount must be greater than 0");
                        continue;
                    }
                    if (amount < Account.MINIMUM_DEPOSITE)
                    {
                        Console.WriteLine($"Minimum deposite amount should be {Account.MINIMUM_DEPOSITE} to open an account ");
                        continue;
                    }
                    Account newAccount = new Account(type, amount);
                    this._accounts.Add(newAccount);
                    accounts.Add(newAccount.AccountNumber, newAccount);
                    Console.WriteLine("\nAccount is created successfully");
                    Console.WriteLine($"Account number is:{newAccount.AccountNumber}");
                    return;
                }

            }
        }
        public void DisplayAllAccounts()
        {
            Console.WriteLine($"\n\tAccount Number\t\t\tType\tBalance");
            foreach (var account in this.Accounts) Console.WriteLine($"{account.AccountNumber}\t{account.Type}\t{account.Balance}");
        }
    }
}
