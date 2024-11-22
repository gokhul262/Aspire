using System;
using System.Text.RegularExpressions;

public static class InputValidator
{
    public static string ValidateUsername()
    {
        string username = string.Empty;
        while (true)
        {
            Console.Write("Enter a new username (alphanumeric only): ");
            username = Console.ReadLine();

            if (Regex.IsMatch(username, @"^[a-zA-Z0-9]+$"))
                break;
            else
                Console.WriteLine("Invalid username. Please use alphanumeric characters only.");
        }
        return username;
    }

    public static string ValidatePassword()
    {
        string password = string.Empty;
        while (true)
        {
            Console.Write("Enter a new password (at least 8 characters, must contain letters and numbers): ");
            password = Console.ReadLine();

            if (Regex.IsMatch(password, @"^(?=.*[a-zA-Z])(?=.*\d)[A-Za-z\d]{8,}$"))
                break;
            else
                Console.WriteLine("Password must be at least 8 characters long and contain both letters and numbers.");
        }
        return password;
    }
}
