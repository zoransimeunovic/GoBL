namespace GoBL.Models
{
    public class Turnir
    {
        //  Potrebno za svaki turnir u organizaciji kluba unijeti podatke u SQL i ovdje staviti odgovarajuci ID
        public static int TurnirId_Haupt { get; set; } = 3;

        public static int TurnirId1 { get; set; } = 3;
        public static int TurnirId2 { get; set; } = 3;

        public int Id { get; set; }
        public string Naziv { get; set; } = "";
        public string Lokacija { get; set; } = "";
        public DateTime DatumOd { get; set; }
        public DateTime DatumDo { get; set; }
        public string Link { get; set; } = "";
    }
}
