using System;
using System.Collections.Generic;
using System.Data;
using System.IO;


namespace Bank_Managment_System
{
    class Program
    {
        static void Main(string[] args)
        {
            AccessAdmin access = new AccessAdmin();
            int choise = 1;
            Console.WriteLine("Write admin password: ");
            string writtenPassword = Console.ReadLine();
            if (!access.AccessAdministration(writtenPassword)) Console.WriteLine("Uncorrect password");
            else
            {
                Console.WriteLine("1) Sign in");
                Console.WriteLine("2) Log in");
                Console.WriteLine();
                choise = Convert.ToInt32(Console.ReadLine());


                switch (choise)
                {
                    case 1:
                        Console.Clear();
                        SignInPerson signacc = new SignInPerson();
                        Console.WriteLine("Do you want to countinue? Write YES or NO");
                        switch (Console.ReadLine())
                        {
                            case "YES":
                                Account acc0unt = new Account(signacc.accountData.accNumber, signacc.accountData.name, signacc.accountData.surname);
                                acc0unt.Processing();
                                break;
                            case "NO":
                                Console.WriteLine("Exit");
                                Environment.Exit(-1);
                                break;
                            default:
                                Console.WriteLine("Wrong answear . . . ");
                                break;
                        }
                        break;
                    
                    case 2:
                        Console.Clear();
                        LogInPerson loginacc = new LogInPerson();
                        Account account = new Account(loginacc.account.accNumber,loginacc.account.name, loginacc.account.surname);
                        Console.Clear();
                        account.Processing();
                        break;
                    
                    default:
                        Console.WriteLine("Uncorrect form...");
                        break;
                }

            }
  
        }
    }
}
