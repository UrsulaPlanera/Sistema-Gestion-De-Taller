using Microsoft.AspNetCore.Mvc;
using SistemaGestionDeTaller.Models;

namespace SistemaGestionDeTaller.Areas.Authentication.Controllers
{
    [Area("Authentication")]
    public class AuthenticationController : Controller
    {

        private readonly DbstContext _context;

        public AuthenticationController(DbstContext context)
        {
            _context = context;
        }

        // GET: AuthenticationController
        public ActionResult Login()
        {
            return View();
        }

        public bool UserExist(string username, string password)
        {
            bool exist = false;

            var result = from u in _context.Users
                            where u.Username == username && u.Password == password
                            select new { u.Username, u.Password };

            if(result.Any())
            {
                exist = true;
            }

            return exist;
        }

        public bool UserIsOnline()
        {
            bool online = false;

            var userCondition = from u in _context.Users
                                select u.IsOnline;

            if(userCondition.Any())
            {
                online = true;
            }

            return online;
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            bool success = UserExist(username, password);

            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Nombre de usuario o contraseña incorrectos.");
            }
            else
            {
                return RedirectToAction("Index", "Utilities", new { area = "Main" });
            }

            return View();
        }

        public ActionResult Recover()
        {
            return View();
        }

    }
}
