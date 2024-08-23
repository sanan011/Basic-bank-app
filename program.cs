using System;

namespace MyFirstProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            static int chooseOperation()
            {
                Console.WriteLine("---------- Welcome to the bank --------");
                Console.WriteLine("|      Please choose an operation:    |");
                Console.WriteLine("|                                     |");
                Console.WriteLine("|  1. Log in to your existing account |");
                Console.WriteLine("|             2. Sign up              |");
                Console.WriteLine("|                                     |");
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("");

                int operationNum;
                do
                {
                    Console.Write("Operation number: ");
                    operationNum = Convert.ToInt32(Console.ReadLine());
                } while (operationNum != 1 && operationNum != 2);

                return operationNum;
            }

            int operationNum = chooseOperation();
            bankUsers[] bankUsersArray = new bankUsers[10];

            // Crucial part is that we assuma that our money in balance is in USD.
            bankUsers user1 = new bankUsers("jack", 34, 4500, "Jackswanson1990");
            bankUsers user2 = new bankUsers("smith", 26, 2200, "sMithnewJearsy");
            bankUsers user3 = new bankUsers("dane", 45, 12000, "Floridadane1979");

            bankUsersArray[0] = user1;
            bankUsersArray[1] = user2;
            bankUsersArray[2] = user3;

            if (operationNum == 1)
            {
                bool userFound = false;
                int userIndex = -1;
                Console.Write("Your name: ");
                string name = Console.ReadLine().ToLower();

                Console.Write("Password: ");
                string password = Console.ReadLine();

                for (int i = 0; i < bankUsersArray.Length; i++)
                {
                    if (bankUsersArray[i] != null && bankUsersArray[i].name == name && bankUsersArray[i].password == password)
                    {
                        userFound = true;
                        userIndex = i;
                        break;
                    }
                }

                if (userFound)
                {
                    Console.WriteLine("Login successful!");
                    Console.WriteLine("");
                    Console.WriteLine("---------- Please choose an operation: --------");
                    Console.WriteLine("|                                             |");
                    Console.WriteLine("|      1. Withdraw money from your account    |");
                    Console.WriteLine("|     2. Top up your balance in your account  |");
                    Console.WriteLine("|             3. Show your balance            |");
                    Console.WriteLine("|                    4. Exit                   |");
                    Console.WriteLine("-----------------------------------------------");
                    Console.WriteLine("");

                    int userChoice;
                    int amountOfMoney;
                    do
                    {
                        Console.Write("Choose the operation you want to do: ");
                        userChoice = Convert.ToInt32(Console.ReadLine());
                    } while (userChoice != 1 && userChoice != 2 && userChoice != 3 && userChoice != 4);

                    if (userChoice == 1)
                    {
                        Console.WriteLine("Choose currency: 1. USD  2. AZN");
                        int currencyChoice;
                        do
                        {
                            Console.Write("Currency (1 or 2): ");
                            currencyChoice = Convert.ToInt32(Console.ReadLine());
                        } while (currencyChoice != 1 && currencyChoice != 2);

                        Console.Write("How much money do you want to withdraw? ");
                        amountOfMoney = Convert.ToInt32(Console.ReadLine());

                        float conversionRate = 1.7f;
                        float amountInUSD = amountOfMoney;

                        if (currencyChoice == 2) // AZN
                        {
                            amountInUSD = amountOfMoney / conversionRate; // Convert AZN to USD
                        }

                        if (amountInUSD > bankUsersArray[userIndex].wallet)
                        {
                            Console.WriteLine("Insufficient funds!");
                        }
                        else
                        {
                            bankUsersArray[userIndex].withdraw(amountInUSD);
                            Console.WriteLine($"Withdraw successful! New balance: {bankUsersArray[userIndex].wallet} USD");
                        }
                    }
                    else if (userChoice == 2)
                    {
                        Console.Write("How much money do you want to top up? ");
                        amountOfMoney = Convert.ToInt32(Console.ReadLine());

                        bankUsersArray[userIndex].topUpBalance(amountOfMoney);
                        Console.WriteLine($"Top-up successful! New balance: {bankUsersArray[userIndex].wallet} USD");
                    }
                    else if (userChoice == 3)
                    {
                        Console.WriteLine("Your balance: " + bankUsersArray[userIndex].wallet + " USD");
                    }
                    else if (userChoice == 4)
                    {
                        Console.WriteLine("Thank you for using our services!");
                    }
                }
                else
                {
                    Console.WriteLine("User not found or incorrect password!");
                }
            }
            else if (operationNum == 2)
            {
                Console.WriteLine("Please enter your information!");
                Console.WriteLine("");

                Console.Write("Your first name: ");
                string name = Console.ReadLine().ToLower();

                Console.Write("Your age: ");
                int age = Convert.ToInt32(Console.ReadLine());

                Console.Write("Your balance: ");
                float wallet = Convert.ToSingle(Console.ReadLine());

                string password;
                do
                {
                    Console.Write("Create password: ");
                    password = Console.ReadLine();
                } while (!IsValidPassword(password));

                bankUsers newUser = new bankUsers(name, age, wallet, password);

                bool userAdded = false;
                for (int i = 0; i < bankUsersArray.Length; i++)
                {
                    if (bankUsersArray[i] == null)
                    {
                        bankUsersArray[i] = newUser;
                        userAdded = true;
                        Console.WriteLine("User successfully signed up!");
                        break;
                    }
                }

                if (!userAdded)
                {
                    Console.WriteLine("No available spots to add a new user.");
                }
            }
        }

        static bool IsValidPassword(string password)
        {
            if (password.Length < 8)
            {
                Console.WriteLine("Your password must be at least 8 characters long and contain an uppercase character!");
                return false;
            }

            foreach (char c in password)
            {
                if (char.IsUpper(c))
                {
                    return true;
                }
            }

            Console.WriteLine("Your password must contain at least one uppercase character!");
            return false;
        }
    }

    public class bankUsers
    {
        public string name;
        public int age;
        public float wallet;
        public string password;

        public bankUsers(string userName, int userAge, float userWallet, string userPassword)
        {
            name = userName;
            age = userAge;
            wallet = userWallet;
            password = userPassword;
        }

        public void showBalance()
        {
            Console.WriteLine($"Current balance: {wallet} USD");
        }

        public float withdraw(float amount)
        {
            wallet -= amount;
            return wallet;
        }

        public float topUpBalance(float amount)
        {
            wallet += amount;
            return wallet;
        }
    }
}
