using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GoBL.Models;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using GoBL.Helpers;
using System.Globalization;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;

namespace GoBL.Controllers;

public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;
    private readonly IStringLocalizer<HomeController> _localizer;

    public HomeController(ILogger<HomeController> logger, IStringLocalizer<HomeController> localizer)
    {
        _logger = logger;
        _localizer = localizer;
    }

    public IActionResult Index()
    {
        // Preuzimanje jezika iz sesije (ako je postavljen)
        //var culture = HttpContext.Session.GetString("Culture") ?? "en"; // Podrazumevano na "en"
        //ViewBag.CurrentCulture = HttpContext.Session.GetString("Culture") ?? System.Globalization.CultureInfo.CurrentUICulture.Name; ;
        TempData["Poruka"] = null;
        if ((bool)ViewData["PostojiRaspisTurnira"])
        {
            return RedirectToAction("TurnirRaspis");
        }
        else
        {
            return RedirectToAction("OKlubu");
        }
    }

    public IActionResult TurnirRaspis_NeMijenjaj()
    {
        try
        {
            if (GoBL.Models.Turnir.TurnirId_Haupt == GoBL.Models.Turnir.TurnirId1)
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


    public IActionResult TurnirRaspis()
    {
        GoBL.Models.Turnir.TurnirId_Haupt = GoBL.Models.Turnir.TurnirId1; // Prebaci na prvi turnir
        var currentCulture = HttpContext.Session.GetString("Culture") ?? System.Globalization.CultureInfo.CurrentUICulture.Name;
        ViewData["CurrentCulture"] = currentCulture;
        if (currentCulture == "sr-Latn")
        {
            ViewData["RaspisHtml"] = HtmlDocsCache.RaspisHtml_sr_latinica;
        }
        else if (currentCulture == "en")
        {
            ViewData["RaspisHtml"] = HtmlDocsCache.RaspisHtml_en;
        }
        else if (currentCulture == "de")
        {
            ViewData["RaspisHtml"] = HtmlDocsCache.RaspisHtml_en;
        }
        else
        {
            ViewData["RaspisHtml"] = HtmlDocsCache.RaspisHtml_en;
        }
        return View();
    }

    public IActionResult TurnirRaspis2()
    {
        GoBL.Models.Turnir.TurnirId_Haupt = GoBL.Models.Turnir.TurnirId2; // Prebaci na prvi turnir
        var currentCulture = HttpContext.Session.GetString("Culture") ?? System.Globalization.CultureInfo.CurrentUICulture.Name;
        ViewData["CurrentCulture"] = currentCulture;
        if (currentCulture == "sr-Latn")
        {
            ViewData["RaspisHtml"] = HtmlDocsCache.Raspis_2_Html_sr_latinica;
        }
        else if (currentCulture == "en")
        {
            ViewData["RaspisHtml"] = HtmlDocsCache.Raspis_2_Html_en;
        }
        else if (currentCulture == "de")
        {
            ViewData["RaspisHtml"] = HtmlDocsCache.Raspis_2_Html_de;
        }
        else
        {
            ViewData["RaspisHtml"] = HtmlDocsCache.Raspis_2_Html_en;
        }
        return View();
    }


    public IActionResult OKlubu()
    {
        return View();
    }
    public IActionResult OGou()
    {
        return View();
    }
    public IActionResult Prijatelji()
    {
        return View();
    }
    public IActionResult GoOrganizacija()
    {
        return View();
    }
    public IActionResult IstorijaKluba()
    {
        return View();
    }




    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }




    [HttpPost]
    public IActionResult ChangeLanguage(string culture)
    {
        // Postavljanje jezika u sesiju
        HttpContext.Session.SetString("Culture", culture);
        //ViewBag.CurrentCulture = HttpContext.Session.GetString("Culture") ?? System.Globalization.CultureInfo.CurrentUICulture.Name; ;
        // Vraćanje korisnika na prethodnu stranicu
        var returnUrl = Request.Headers["Referer"].ToString();
        if (string.IsNullOrEmpty(returnUrl))
        {
            return RedirectToAction("Index", "Home"); // Ako nema Referer, vratimo na početnu stranicu
        }
        return Redirect(returnUrl ?? "/");
    }

    [HttpPost]
    public IActionResult SetLanguage(string culture, string returnUrl)
    {
        HttpContext.Session.SetString("Culture", culture);
        return Redirect(returnUrl);
    }
}
