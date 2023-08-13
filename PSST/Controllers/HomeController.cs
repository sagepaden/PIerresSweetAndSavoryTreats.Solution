using Microsoft.AspNetCore.Mvc;
using PSST.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
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
      public ActionResult Index()
      {
        Flavor[] flavors = _db.Flavors.ToArray();
        Treat[] treats = _db.Treats.ToArray();
        Dictionary<string,object[]> model = new Dictionary<string, object[]>();
        model.Add("flavors", flavors);
        model.Add("treats", treats);
        return View(model);
      }
    }
}