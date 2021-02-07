using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Text;
using System.Threading;

namespace Bank_Managment_System
{
    struct AccountData
    {
        public string name;
        public string surname;
        public string accNumber;
        public string pin;
    }
    abstract class FileWrRd
    {
        protected void WrittingInFile(string path, string accInfo)
        {
            using (StreamWriter swd = new StreamWriter(path, true))
            {
                swd.WriteLine(accInfo);
                Console.WriteLine("All data was written right");
                swd.Close();
            }
        }
        protected bool CheckingExisting(string path, string accInfo)
        {
            bool b0ol = true;
            using (StreamReader sr = new StreamReader(path))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                    if (s == accInfo)
                    {
                        Console.WriteLine("Exist this account");
                        b0ol = false;
                        sr.Close();
                    }
            }
            return b0ol;
        }
        protected void ReadAllFile(string path, ref List<string> accounts)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string buff;
                while ((buff = sr.ReadLine()) != null)
                    accounts.Add(buff);
            }

        }
        protected void WriteAllFile(string path, ref List<string> accounts)
        {
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                foreach (string bal in accounts)
                    sw.WriteLine(bal);
                sw.Close();
            }

        }
    }

    class AccessAdmin
    {
        public bool AccessAdministration(string writtenPassword)
        {
            string passwordAdmin = "123312";
            if (passwordAdmin == writtenPassword) return true; else return false;
        }
    }
    class SignInPerson : FileWrRd
    {
        public AccountData accountData = new AccountData();
        public SignInPerson()
        {
            Random rand = new Random();
            string pathD = @"D:\HACK\for education\Additional\IT and programing\Projects\C#\Bank Managment System\DataAccount.txt";         
            string pathB = @"D:\HACK\for education\Additional\IT and programing\Projects\C#\Bank Managment System\Account Balance.txt";     
            
            for (int i = 1; i <= 14; i++)
                accountData.accNumber += rand.Next(10);
            Console.WriteLine($"Your bank account:{0}", accountData.accNumber);

            bool buff = true;                                                                                                               
            while (buff)
            {
                Console.WriteLine("Write your pin-code(4 digits):");
                accountData.pin = Console.ReadLine();
                if (accountData.pin.Length != 4) Console.WriteLine("Uncorrect form");
                else
                {
                    buff = false;
                }
            }

            Console.WriteLine("Write your name:");                                                                                             
            accountData.name = Console.ReadLine();

            Console.WriteLine("Write your surname:");                                                                                           
            accountData.surname = Console.ReadLine();

            string info = accountData.accNumber +" "+ accountData.pin +" "+ accountData.name+" "+ accountData.surname;
            CheckingExisting(pathD, info);

            if (CheckingExisting(pathD,info) == true)
            {
                WrittingInFile(pathD, info);
                WrittingInFile(pathB, accountData.accNumber + ' ' + "0");
            }
        }
    }

    class LogInPerson : FileWrRd
    {
        public AccountData account = new AccountData();

        public LogInPerson()
        {
            string checkAccInfo = "";
            string accountInfo = "";
            List<string> accounts = new List<string>();
            string path = @"D:\HACK\for education\Additional\IT and programing\Projects\C#\Bank Managment System\DataAccount.txt";
            ReadAllFile(path, ref accounts);

            Console.WriteLine($"Write your bank account number:");
            checkAccInfo += Console.ReadLine() + " ";
            Console.WriteLine($"Write your pin:");
            checkAccInfo += Console.ReadLine() + " ";

            foreach (string acc in accounts)
                if (acc.StartsWith(checkAccInfo)) accountInfo = acc;

            int iter;
            for (iter = 0; iter < 14; iter++)
                account.accNumber += accountInfo[iter];

            for (iter = 19; accountInfo == " "; iter++)
                account.name += accountInfo[iter];

            for (iter += 1; iter < accountInfo.Length; iter++)
                account.surname += accountInfo[iter];
        }
    }
    class Account : FileWrRd
    {
        AccountData account = new AccountData();
        string pathB = @"D:\HACK\for education\Additional\IT and programing\Projects\C#\Bank Managment System\Account Balance.txt";

        public Account(string accountNumber, string name, string surname)
        {
            account.accNumber = accountNumber;
            account.name = name;
            account.surname = surname;
        }
        public void Processing()
        {
            bool buff = true;
            while (buff)
            {
                Console.WriteLine("Personal bank account");
                Console.WriteLine($"Hello, {account.name} {account.surname}");
                Console.WriteLine("Services:");
                Console.WriteLine("1. Money Transaction");
                Console.WriteLine("2. Deposite");
                Console.WriteLine("3. Withdrawal of money");
                Console.WriteLine("4. Withdrawal of money with other currency");
                Console.WriteLine("5. Deposite of money with other currency");
                Console.WriteLine("6. Show balance");
                Console.WriteLine("7. Exit");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Choose service: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        moneyTransaction();
                        break;
                    case "2":
                        Deposite();
                        break;
                    case "3":
                        Withdrawal();
                        break;
                    case "6":
                        showBalance();
                        break;
                    case "7":
                        Environment.Exit(-1);
                        break;
                    default:
                        Console.WriteLine("Something wrong . . . No service . . . Try again . . .");
                        break;
                }
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private void moneyTransaction()
        {
            List<string> balancesAcc = new List<string>();
            Console.WriteLine($"Write aaccount number of Recipient: ");
            string accRecNumber = Console.ReadLine();
            Console.WriteLine($"Write a transfer amount: ");
            string transAmount = Console.ReadLine();
            string balanceRec = "";

            ReadAllFile(this.pathB, ref balancesAcc);

            for (int j = 0; j < balancesAcc.Count; j++)                                                                         
            {
                if (balancesAcc[j].StartsWith(account.accNumber))
                {
                    string balanceSen = "";
                    for (int iter = 15; iter < balancesAcc[j].Length; iter++)
                        balanceSen += balancesAcc[j][iter];

                    if (balanceSen == "0") Console.WriteLine("Not enought money for transaction");
                    else
                    {
                        int bal = Convert.ToInt32(balanceSen);
                        int tran = Convert.ToInt32(transAmount);
                        if (bal - tran < 0) Console.WriteLine("Not enought money");
                        else
                        {
                            balanceSen = Convert.ToString(bal - tran);

                            balanceSen = account.accNumber + " " + balanceSen;
                            balancesAcc[j] = balanceSen;

                            for (int iter = 0; iter < balancesAcc.Count; iter++)                                                      
                            {
                                if (balancesAcc[iter].StartsWith(accRecNumber))
                                {
                                    for (int iter1 = 15; iter1 < balancesAcc[iter].Length; iter1++)
                                        balanceRec += balancesAcc[iter][iter1];

                                    balanceRec = Convert.ToString(Convert.ToInt32(balanceRec) + Convert.ToInt32(transAmount));

                                    balanceRec = accRecNumber + " " + balanceRec;
                                    balancesAcc[iter] = balanceRec;
                                }
                            }
                            Console.WriteLine();
                            Console.Clear();
                            Console.WriteLine("Transaction was successful . . .");
                        }
                    }
                }
                WriteAllFile(this.pathB,ref balancesAcc);
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private void Deposite()
        {
            Console.WriteLine("The amount of money deposited on the bank account:");
            Console.WriteLine();
            string amount = Console.ReadLine();
            List<string> balancesAcc = new List<string>();
            string balanceClient="";

            ReadAllFile(this.pathB, ref balancesAcc);

            for (int i = 0; i < balancesAcc.Count; i++)
            {
                if (balancesAcc[i].StartsWith(account.accNumber))
                {
                    for(int iter = 15; iter < balancesAcc[i].Length; iter++)
                        balanceClient += balancesAcc[i][iter];

                    balanceClient = Convert.ToString(Convert.ToInt32(balanceClient) + Convert.ToInt32(amount));

                    balanceClient = account.accNumber + " " + balanceClient;
                    balancesAcc[i] = balanceClient;
                    Console.WriteLine();
                    Console.Clear();
                    Console.WriteLine("Depositing was successful. . .");
                }
            }
            WriteAllFile(pathB, ref balancesAcc);
            Console.WriteLine();
            Console.WriteLine();
        }

        private void Withdrawal()
        {
            List<string> balances = new List<string>();
            string amount;
            Console.WriteLine("Write withdrawal amount: ");
            Console.WriteLine();
            amount = Console.ReadLine();
            string balanceClient = "";

            ReadAllFile(this.pathB, ref balances);

            for (int i = 0; i < balances.Count; i++)
            {
                if (balances[i].StartsWith(account.accNumber))
                {
                    for (int iter = 15; iter < balances[i].Length; iter++)
                        balanceClient += balances[i][iter];
                    int am = Convert.ToInt32(amount);
                    int bal = Convert.ToInt32(balanceClient);
                    if (bal - am < 0) Console.WriteLine("Not enought money . . .");
                    else
                    {
                        balanceClient = Convert.ToString(bal-am);
                        balanceClient = account.accNumber + " " + balanceClient;
                        balances[i] = balanceClient;
                        Console.WriteLine();
                        Console.Clear();
                        Console.WriteLine("Withdrawing was succesful . . .");
                    }
                }
            }
            WriteAllFile(pathB, ref balances);
            Console.WriteLine();
            Console.WriteLine();
        }
        private void showBalance()
        {
            using (StreamReader sr = new StreamReader(pathB))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                    if (s.StartsWith(account.accNumber))
                    {
                        Console.WriteLine();
                        Console.WriteLine("BALANCE: ");
                        for (int i = 15; i < s.Length; i++)
                            Console.Write(s[i]);
                    }
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}

