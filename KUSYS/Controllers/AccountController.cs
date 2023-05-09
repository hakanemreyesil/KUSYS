using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KUSYS.Controllers
{
    public class AccountController : Controller
    {
        private IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = userService.GetUser(username, password);


            if (user != null)
            {
                user.Role = userService.GetRoleByUserId(user.UserId);
                var claims = new List<Claim>();
                claims.Add(new Claim("username", username));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()));
                claims.Add(new Claim(ClaimTypes.Role, user.Role.RoleName));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return Redirect("/home/index");
            }
            else
            {
                return Redirect("/login");
            }

        }

        [HttpGet("denied")]
        public async Task<IActionResult> Denied()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
