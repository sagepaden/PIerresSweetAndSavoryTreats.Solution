using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using PSST.Models;

namespace PSST.Controllers
{
    [Authorize]
    public class TreatsController : Controller
    {
        private readonly PSSTContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public TreatsController(UserManager<ApplicationUser> userManager, PSSTContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task<ActionResult> Index()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
            List<Treat> userTreats = _db.Treats
                                .Include(PSST => PSST.JoinEntities)
                                .ThenInclude(join => join.Treat)
                                .ToList();
            return View(userTreats);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Treat treat, int TreatId)
        {
            if (!ModelState.IsValid)
            {
                return View(treat);
            }
            else
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
                treat.User = currentUser;
                _db.Treats.Add(treat);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Details(int id)
        {
            Treat thisTreat = _db.Treats
                                .Include(PSST => PSST.JoinEntities)
                                .ThenInclude(join => join.Treat)
                                .FirstOrDefault(PSST => PSST.TreatId == id);
            return View(thisTreat);
        }

        public ActionResult Edit(int id)
        {
            Treat thisTreat = _db.Treats
                                .FirstOrDefault(PSST => PSST.TreatId == id);
            return View(thisTreat);
        }
        [HttpPost]
        public ActionResult Edit(Treat treat)
        {
            _db.Treats.Update(treat);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Treat thisTreat = _db.Treats
                                .FirstOrDefault(PSST => PSST.TreatId == id);
            return View(thisTreat);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Treat thisTreat = _db.Treats
                                .FirstOrDefault(PSST => PSST.TreatId == id);
            _db.Treats.Remove(thisTreat);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> AddFlavor(int id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
            List<Flavor> userFlavors = _db.Flavors
                                .Where(PSST => PSST.User.Id == currentUser.Id)
                                .ToList();
            Treat thisTreat = _db.Treats
                                .FirstOrDefault(PSST => PSST.TreatId == id);
            ViewBag.FlavorId = new SelectList(userFlavors, "FlavorId", "Name");
            return View(thisTreat);
        }

        [HttpPost]
        public ActionResult AddFlavor(Treat treat, int FlavorId)
        {
            #nullable enable
            TreatFlavor? joinEntity = _db.TreatFlavors.FirstOrDefault(join => (join.TreatId == treat.TreatId && join.FlavorId == FlavorId));
            #nullable disable
            if (joinEntity == null && FlavorId != 0)
            {
                _db.TreatFlavors.Add(new TreatFlavor() { FlavorId = FlavorId, TreatId = treat.TreatId });
                _db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = treat.TreatId });
        }

        [HttpPost]
        public ActionResult DeleteJoin(int joinId)
        {
            TreatFlavor entry = _db.TreatFlavors.FirstOrDefault(entry => entry.TreatFlavorId == joinId);
            _db.TreatFlavors.Remove(entry);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}