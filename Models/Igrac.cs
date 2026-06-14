
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoBL.Models
{
    public class Igrac
    {
        [Key]
        public int igracId { get; set; }  // Primarni ključ  

        [Column("turnirid")]
        public int TurnirId { get; set; }

        [Column("ime")]
        public string Ime { get; set; } = "";

        [Column("prezime")]
        public string Prezime { get; set; } = "";

        [Column("kategorija")]
        public string Kategorija { get; set; } = "";

        [Column("klub")]
        public string Klub { get; set; } = "";

        [Column("drzava")]
        public string Drzava { get; set; } = "";

        [Column("datumRodjenja")]
        public DateTime? DatumRodjenja { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("napomena")]
        public string? Napomena { get; set; }

        [Column("datumPrijave")]
        public DateTime DatumPrijave { get; set; }


        internal bool MladjiOd18()
        {
            if (DatumRodjenja == null)
                return false;
            var age = DateTime.Now.Year - DatumRodjenja.Value.Year;
            if (DateTime.Now < DatumRodjenja.Value.AddYears(age))
                age--;
            return age < 18;
        }

        internal bool ImaNapomena()
        {
            return !string.IsNullOrEmpty(Napomena);
        }

    }
}
