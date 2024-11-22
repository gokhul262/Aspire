using System;
using System.Collections;

class Program
{
    static void Main()
    {
        ArrayList users = new ArrayList();
        
        User user = new User("gokhul", "12345", DateTime.Now, "Admin", "Administration");
        users.Add(user);
        
        LoginManager loginManager = new LoginManager(users);

        User loggedInUser = null;

        Console.WriteLine("Welcome to Aspire Systems!");
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register a new account");
        Console.Write("Please choose an option (1 or 2): ");
        int option = int.Parse(Console.ReadLine());

        if (option == 2) // If the user selects option 2 then the following steps will be executed  
        {
            // Call method to handle user registration
            loggedInUser = RegisterNewUser(users);
        }

        // Login process
        while (loggedInUser == null)
        {
            loggedInUser = loginManager.LoginProcess();
        }

        Console.WriteLine($"\nLogin successful! Welcome, {loggedInUser.Name}.");

        // Proceed to the main menu after successful login
        Menu(loggedInUser, loginManager);
    }

    static User RegisterNewUser(ArrayList users)
    {
        string newUsername = InputValidator.ValidateUsername();
        string newPassword = InputValidator.ValidatePassword();

        Console.Write("Enter your full name: ");
        string newName = Console.ReadLine();

        Console.Write("Enter your department: ");
        string newDepartment = Console.ReadLine();

        Console.Write("Enter your date of joining (yyyy-mm-dd): ");
        DateTime newDateOfJoining = DateTime.Parse(Console.ReadLine());

        // Create a new User object and add it to the users list
        User newUser = new User(newUsername, newPassword, newDateOfJoining, newName, newDepartment);
        users.Add(newUser);

        Console.WriteLine("\nUser registration successful!");
        return newUser;
    }

    static void Menu(User loggedInUser, LoginManager loginManager)
    {
        int choice = 0;

        while (choice != 4)  
        {
            // Display personalized menu
            Console.WriteLine($"\nHello, {loggedInUser.Name}! Please choose an option:");
            Console.WriteLine("1. View Profile Details");
            Console.WriteLine("2. View Date of Joining");
            Console.WriteLine("3. View Department");
            Console.WriteLine("4. Logout");

            
            Console.Write("Enter your choice (1-4): ");
            try
            {
                choice = int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                continue;
            }


           //USer choice
            switch (choice)
            {
                case 1:
                    loggedInUser.DisplayProfile();
                    break;
                case 2:
                    Console.WriteLine("Date of Joining: " + loggedInUser.DateOfJoining.ToShortDateString());
                    break;
                case 3:
                    Console.WriteLine("Department: " + loggedInUser.Department);
                    break;
                case 4:
                    loginManager.Logout();
                    Console.WriteLine("Thank you . Have a nice day");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }
        }
    }
}
