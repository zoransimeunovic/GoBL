namespace GoBL.Models
{
    public class Vijest
    {
        public static string de = "de";
        public static string en = "en";
        public static string sr = "sr-Latn";

        public int Id { get; set; }
        public string culture { get; set; }
        public string Naslov { get; set; }
        public DateTime Datum { get; set; }
        public string Tekst { get; set; }
    }
}
