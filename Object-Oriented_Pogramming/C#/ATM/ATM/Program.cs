using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class Program
    {
        static void printOptions()
        {
            Console.WriteLine("Please choose from one of the following options...");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Show Balance");
            Console.WriteLine("4. Exit");
        }

        static void deposit(cardHolder currentUser)
        {
            Console.WriteLine("How much do you want to deposit?");
            double amount = double.Parse(Console.ReadLine());
            currentUser.setBalance(currentUser.getBalance() + amount);
            Console.WriteLine("Your new Balance is: " + currentUser.getBalance());
            

        }
        static void withdraw(cardHolder currentUser)
        {
            Console.WriteLine("How much do you want to withdraw?");
            double amount = double.Parse(Console.ReadLine());
            if (currentUser.getBalance() < amount)
            {
                Console.WriteLine("Insufficient balance");
            }
            else
            {
                currentUser.setBalance(currentUser.getBalance() - amount);
                Console.WriteLine("Your new Balance is: " + currentUser.getBalance());
            }
        }
        static void balance(cardHolder currentUser)
        {
            Console.WriteLine("Your Balance is: " + currentUser.getBalance());
        }
        public static void Main(String[] args)
        {            
            List<cardHolder> cardHolders = new List<cardHolder>();
            cardHolders.Add(new cardHolder("4532761841325802", 4321, "Ashley", "Jones", 321.13));
            cardHolders.Add(new cardHolder("4532201841325802", 6112, "Frida", "Dickersen", 531.19));
            cardHolders.Add(new cardHolder("87532761841325802", 6912, "Elmar", "Dickersen", 831.19));
            cardHolders.Add(new cardHolder("5432761841325802", 3112, "Yane", "Dickersen", 931.19));
            
            // Prompt user
            Console.WriteLine("Welcome to SimpleATM");
            Console.WriteLine("Please insert your debit card: ");
            String debitCardNum;
            cardHolder currentUser;
            int userPin;
            
            while (true)
            {
                try
                {
                    debitCardNum = Console.ReadLine();
                    // Check mit der Datenbank
                    currentUser = cardHolders.FirstOrDefault(i => i.cardNum == debitCardNum);
                    if (currentUser != null) 
                    { 
                        break; 
                    }
                    else 
                    { 
                        Console.WriteLine("Card not recognized. Please try again!"); 
                    }
                }
                catch
                {
                    Console.WriteLine("Card not recognized. Please try again!");
                }
            }

            while (true)
            {
                try
                {
                    Console.WriteLine("Please insert your pin: ");
                    userPin = int.Parse(Console.ReadLine());
                    if (userPin == currentUser.getPin())
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect pin. Please try again.");
                    }
                }
                catch
                {
                    Console.WriteLine("Incorrect pin. Please try again.");
                }
            }

            Console.WriteLine("Welcome " + currentUser.getFirstName());
            int option;
            do
            {
                // Print Options
                printOptions();
                
                // Get Option
                option = int.Parse(Console.ReadLine());
                switch(option)
                {
                    case 1:
                        deposit(currentUser);
                        break;
                    case 2:
                        withdraw(currentUser);
                        break;
                    case 3:
                        balance(currentUser);
                        break;
                }

            }
            while (option != 4);
            
            // Verabschiedung
            Console.WriteLine("Thank you! Have a nice day!");
        }
    }
}

