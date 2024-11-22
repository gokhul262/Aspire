using System;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public DateTime DateOfJoining { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }

    public User(string username, string password, DateTime dateOfJoining, string name, string department)
    {
        Username = username;
        Password = password;
        DateOfJoining = dateOfJoining;
        Name = name;
        Department = department;
    }

    public void DisplayProfile()
    {
        // This will display only the logged-in user's profile information
        Console.WriteLine($"User Profile for {Name}:");
        Console.WriteLine($"Username: {Username}");
        Console.WriteLine($"Date of Joining: {DateOfJoining.ToShortDateString()}");
        Console.WriteLine($"Department: {Department}");
    }
}
