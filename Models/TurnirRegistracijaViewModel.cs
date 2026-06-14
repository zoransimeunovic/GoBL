//using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations;

namespace GoBL.Models
{
    public class TurnirRegistracijaViewModel
    {

        [Required(ErrorMessageResourceName = "RequiredIme", ErrorMessageResourceType = typeof(Resources.Views.Turnir.Registracija))]
        [Display(Name = "lblIme", ResourceType = typeof(Resources.Views.Turnir.Registracija))]
        public string Ime { get; set; } ="";

        [Required(ErrorMessageResourceName = "RequiredPrezime", ErrorMessageResourceType = typeof(Resources.Views.Turnir.Registracija))]
        [Display(Name = "lblPrezime", ResourceType = typeof(Resources.Views.Turnir.Registracija))]
        public string Prezime { get; set; } = "";

        [Required(ErrorMessageResourceName = "RequiredKategorija", ErrorMessageResourceType = typeof(Resources.Views.Turnir.Registracija))]
        [Display(Name = "lblKategorija", ResourceType = typeof(Resources.Views.Turnir.Registracija))]
        public string Kategorija { get; set; } = "";

        [Required(ErrorMessageResourceName = "RequiredKlub", ErrorMessageResourceType = typeof(Resources.Views.Turnir.Registracija))]
        [Display(Name = "lblKlub", ResourceType = typeof(Resources.Views.Turnir.Registracija))]
        public string Klub { get; set; }= "";

        [Required(ErrorMessageResourceName = "RequiredDrzava", ErrorMessageResourceType = typeof(Resources.Views.Turnir.Registracija))]
        [Display(Name = "lblDrazava", ResourceType = typeof(Resources.Views.Turnir.Registracija))]
        public string Drzava { get; set; } ="";

        [Required(ErrorMessageResourceName = "RequiredEmail", ErrorMessageResourceType = typeof(Resources.Views.Turnir.Registracija))]
        [EmailAddress]
        [Display(Name = "lblEmail", ResourceType = typeof(Resources.Views.Turnir.Registracija))]
        public string? Email { get; set; }

        [Display(Name = "lblDatumRodjenja", ResourceType = typeof(Resources.Views.Turnir.Registracija))]
        public DateTime? DatumRodjenja { get; set; }

        [Display(Name = "lblNapomena", ResourceType = typeof(Resources.Views.Turnir.Registracija))]
        public string? Napomena { get; set; }

        //public List<string> Kategorije => new List<string>
        //    {
        //        "9p","8p","7p","6p","5p","4p","3p","2p","1p",
        //        "9d","8d","7d","6d","5d","4d","3d","2d","1d",
        //        "1k","2k","3k","4k","5k","6k","7k","8k","9k","10k",
        //        "11k","12k","13k","14k","15k","16k","17k","18k","19k","20k",
        //        "21k","22k","23k","24k","25k"
        //    };
    }
}
