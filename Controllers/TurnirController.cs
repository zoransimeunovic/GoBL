using GoBL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoBL.Controllers
{
    public class TurnirController : BaseController
    {
        private readonly ApplicationDbContext db;

        public TurnirController(ApplicationDbContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Registracija()
        {
            ViewBag.CurrentCulture = HttpContext.Session.GetString("Culture") ?? System.Globalization.CultureInfo.CurrentUICulture.Name; ;
            return View(new TurnirRegistracijaViewModel());
        }

        [HttpPost]
        public IActionResult Registracija(TurnirRegistracijaViewModel model)
        {
            try
            {
                TempData["Poruka"] = null;
                if (!ModelState.IsValid)
                    return View(model);
                // Mapa ViewModel -> Entity  
                var noviIgrac = new Igrac
                {
                    TurnirId = GoBL.Models.Turnir.TurnirId_Haupt,
                    Ime = model.Ime,
                    Prezime = model.Prezime,
                    Kategorija = model.Kategorija,
                    Klub = model.Klub,
                    Drzava = model.Drzava,
                    DatumRodjenja = model.DatumRodjenja,
                    Email = model.Email,
                    Napomena = model.Napomena,
                    DatumPrijave = DateTime.Now.Date
                };
                db.igraci.Add(noviIgrac);
                db.SaveChanges(); // <--- ovo snima u bazu  
                TempData["Poruka"] = "Uspješno ste se registrovali!";
                return RedirectToAction("TurnirRaspis_NeMijenjaj", "Home");
            }
            catch (DbUpdateException ex)
            {
                // Ispiši u log ili TempData poruku za debug
                var sqlMessage = ex.InnerException?.Message ?? ex.Message;
                TempData["Error"] = $"Greška pri registraciji: {sqlMessage}";
                return RedirectToAction("TurnirRaspis", "Home");
            }
        }

        [HttpGet]
        public IActionResult PrijavljeniIgraci()
        {
            try
            {
                var igraci = db.igraci
                    .Where(i => i.TurnirId == GoBL.Models.Turnir.TurnirId_Haupt)
                    .AsEnumerable() // Povuci podatke iz baze pa sortiraj u memoriji
                    .OrderBy(i => GoBL.Helpers.KategorijeList.Kategorije.IndexOf(i.Kategorija))
                    .ThenBy(i => i.Prezime)
                    .ToList();
                //var igraci = db.igraci
                //    .OrderBy(i => i.Kategorija)
                //    .ThenBy(i => i.Prezime)
                //    .ToList();
                return View(igraci);
            }
            catch (DbUpdateException ex)
            {
                // Ispiši u log ili TempData poruku za debug
                var sqlMessage = ex.InnerException?.Message ?? ex.Message;
                TempData["Error"] = $"Greška: {sqlMessage}";
                return RedirectToAction("TurnirRaspis", "Home");
            }
        }

        [HttpGet]
        public IActionResult IdiNaDrugiTurnir()
        {
            try
            {
                if (GoBL.Models.Turnir.TurnirId_Haupt == GoBL.Models.Turnir.TurnirId2)
                {
                    return RedirectToAction("TurnirRaspis", "Home");
                }
                else
                {
                    return RedirectToAction("TurnirRaspis2", "Home");
                }
            }
            catch (DbUpdateException ex)
            {
                var sqlMessage = ex.InnerException?.Message ?? ex.Message;
                TempData["Error"] = $"Greška: {sqlMessage}";
                return RedirectToAction("TurnirRaspis", "Home");
            }
        }

        public IActionResult Kalendar()
        {
            var turniri = new List<Turnir>
                {
                    new Turnir { Id = 1, Naziv = "European Championship 2026", Lokacija = "Ankara", DatumOd = new DateTime(2026, 7, 25), DatumDo = new DateTime(2026, 8, 02), Link = "https://www.eurogofed.org/EC2026/index.html" },
                    new Turnir { Id = 2, Naziv = "BANJA LUKA OPEN 2026", Lokacija = "Banja Luka", DatumOd = new DateTime(2026, 9, 12), DatumDo = new DateTime(2026, 9, 13), Link = "/Home/TurnirRaspis" },
                    new Turnir { Id = 3, Naziv = "10th Velika Gorica City Go Tournament", Lokacija = "Velika Gorica", DatumOd = new DateTime(2026, 12, 12), DatumDo = new DateTime(2026, 12, 14), Link = "https://gkvg.hr/" },
                };
            return View(turniri);
        }
    }
}
