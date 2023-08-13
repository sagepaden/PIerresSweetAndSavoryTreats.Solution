using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PSST.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

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
            Flavor[] flavors = _db.Flavors.ToArray();
            Dictionary<string, object[]> model = new Dictionary<string, object[]>();
            model.Add("flavors", flavors);
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
            Treat[] treats = null;
            if (currentUser != null)
            {
                treats = _db.Treats
                    // .Where(entry => entry.User.Id == currentUser.Id)
                    .ToArray();
            }
            model.Add("treats", treats ?? new Treat[0]);
            return View(model);
        }
    }
}