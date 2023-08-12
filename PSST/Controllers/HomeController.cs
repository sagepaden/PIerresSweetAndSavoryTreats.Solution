using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using PSST.Models;

namespace PSST.Controllers
{
    public class HomeController : Controller
    {
        private readonly PSSTContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager, PSSTContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        [HttpGet("/")]
        public async Task<ActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                Treat[] treats = _db.Treats.ToArray();
                Flavor[] flavors = _db.Flavors.ToArray();
                Dictionary<string, object[]> model = new Dictionary<string, object[]>();
                model.Add("treats", treats);
                model.Add("flavors", flavors);
                string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
                return View(model);
            }
            else
            {
                return View(_db.Treats.ToList());
            }
        }
    }
}