using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BankingApplication
{
    class Program
    {
        public const string STRING_REGEX = @"^[a-zA-Z]*$";
        static void Main(string[] args)
        {
            Dictionary<string , Customer> customers = new Dictionary<string , Customer>();

            Dictionary<string , Account> accounts = new Dictionary<string , Account>();

            ProcessInput(customers , accounts);
        }
        private static void ProcessInput(Dictionary<string , Customer> customers , Dictionary<string , Account> accounts)
        {
            string choice;
            do
            {
                Console.WriteLine("\nSelect one of the following options:");
                Console.WriteLine("1.Add new customer");
                Console.WriteLine("2.Add new account");
                Console.WriteLine("3.WithDraw");
                Console.WriteLine("4.Deposite");
                Console.WriteLine("5.Display all customers");
                Console.WriteLine("6.Display balance using account number");
                Console.WriteLine("7.Display balance for all the accounts");
                Console.WriteLine("8.Account statement");
                Console.WriteLine("9.Exit");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddNewCustomer(customers);
                        break;
                    case "2":
                        AddNewAccount(customers , accounts);
                        break;
                    case "3":
                        Withdraw(accounts);
                        break;
                    case "4":
                        Deposite(accounts);
                        break;
                    case "5":
                        DisplayAllCustomers(customers);
                        break;
                    case "6":
                        DisplayBalance(accounts);
                        break;
                    case "7":
                        DisplayBalanceForAllAccounts(customers);
                        break;
                    case "8":
                        AccountStatement(accounts);
                        break;
                    case "9":
                        break;
                    default:
                        Console.WriteLine("Please enter correct choice");
                        break;
                }
            } while (choice != "9");
        }
        private static void AddNewCustomer(Dictionary<string , Customer> customers)
        {
            Console.Write("Enter Firstname:");
            string firstName = IsValidString(Console.ReadLine());

            Console.Write("Enter Lastname:");
            string lastName = IsValidString(Console.ReadLine());

            Customer newCustomer = new Customer(firstName , lastName);
            customers.Add(
               newCustomer.CustomerId ,
               newCustomer
            );
            Console.WriteLine("\nCustomer is added successfully");
            Console.WriteLine($"Customer id is:{newCustomer.CustomerId}");
        }
        private static string IsValidString(string str)
        {
            while (!Regex.IsMatch(str , STRING_REGEX) || str == "")
            {
                if (str == "")
                    Console.WriteLine("Name cannot be empty");
                else
                    Console.WriteLine("Name must contais only alphabets");
                str = Console.ReadLine();

            }

            return str;
        }
        private static void AddNewAccount(Dictionary<string , Customer> customers , Dictionary<string , Account> accounts)
        {
            Console.Write("Enter customer id:");
            string customerId = Console.ReadLine();

            if (!customers.ContainsKey(customerId))
            {
                Console.WriteLine("Customer not found!");
                return;
            }
            customers[customerId].AddAccount(customers , accounts);

        }
        private static void Withdraw(Dictionary<string , Account> accounts)
        {
            Account account = FindAccount(accounts);
            if (account != null) account.Debit();
        }
        private static void Deposite(Dictionary<string , Account> accounts)
        {
            Account account = FindAccount(accounts);
            if (account != null) account.Credit();
        }
        private static void DisplayAllCustomers(Dictionary<string , Customer> customers)
        {
            Console.WriteLine($"Customer name\t\t\tCustomer id");
            foreach (var customer in customers) Console.WriteLine($"{customer.Value.FullName}\t\t{customer.Key}");
        }
        private static void AccountStatement(Dictionary<string , Account> accounts)
        {
            Account account = FindAccount(accounts);
            if (account != null) account.AccountStatement();
        }
        private static void DisplayBalanceForAllAccounts(Dictionary<string , Customer> customers)
        {
            Console.Write("Enter customer id:");
            string customerId = Console.ReadLine();
            if (!customers.ContainsKey(customerId))
            {
                Console.WriteLine("Customer not found");
                return;
            }
            customers[customerId].DisplayAllAccounts();
        }
        private static void DisplayBalance(Dictionary<string , Account> accounts)
        {
            Account account = FindAccount(accounts);
            if (account != null) Console.WriteLine($"Current balance is:{account.Balance}");
        }
        private static Account FindAccount(Dictionary<string , Account> accounts)
        {
            Console.Write("Enter account number:");
            string accountNumber = Console.ReadLine();
            if (accountNumber == "")
            {
                Console.WriteLine("Account number cannot be null");
                return null;
            }
            if (!accounts.ContainsKey(accountNumber))
            {
                Console.WriteLine("Account not found");
                return null;
            }
            return accounts[accountNumber];
        }
    }
}
