using System;
using System.Collections;

public class LoginManager
{
    private ArrayList users;
    private User loggedInUser;

    public LoginManager(ArrayList users)
    {
        this.users = users;
    }

    // Login method
    public User LoginProcess()
    {
        Console.Write("Enter username: ");
        string username = Console.ReadLine();

        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        foreach (User user in users)
        {
            if (user.Username == username && user.Password == password)
            {
                loggedInUser = user;
                return loggedInUser;
            }
        }

        Console.WriteLine("Invalid login credentials. Please try again.");
        return null;
    }

    // Logout method
    public void Logout()
    {
        loggedInUser = null;
    }
}
