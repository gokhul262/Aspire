using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using EmployeeManagementSystem.Data;

namespace EmployeeManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
 
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
 
        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }
 
        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Check if email already exists
                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email already exists.");
                    return View(user);
                }
 
                // Add user to database
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            return View(user);
        }
 
        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }
 
        // POST: Account/Login
        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Login(string email, string password)
{
    var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
 
    if (user != null)
    {
        // Create claims for the logged-in user
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Email, user.Email)
        };
 
        // Create identity and principal
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true, // Persistent login
        };
 
        // Sign in the user
       await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
    new ClaimsPrincipal(claimsIdentity),
    authProperties);
 
        return RedirectToAction("Index", "Employee");
    }
 
    ModelState.AddModelError("", "Invalid email or password.");
    return View();
}
 
        // GET: Account/Logout
        // Update Logout action
public async Task<IActionResult> Logout()
{
    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return RedirectToAction("Login");
}
    }
}
 
 
