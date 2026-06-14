using GoBL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace GoBL.Controllers
{
    public class VijestiController : BaseController
    {

        static private List<Vijest> Vijesti_All()
        {
            var vijesti = new List<Vijest>
            {
        new Vijest { Id = 1, culture = Vijest.sr,
            Naslov = "Održan međunarodni turnir u Zelenkovcu",
            Datum = new DateTime(2025, 6, 24),
            Tekst = "U prelijepom planinskom izletištu <a href=\"https://www.zelenkovac.com/\" target=\"_blank\">Zelenkovac</a> kod Mrkonjić Grada, od 20. do 22.juna 2025.godine, održan je međunarodni turnir u igri Go. Na turniru je učestvovalo ukupno 20 takmičara iz šest zemalja sa dva kontinenta. Pored takmičarkih mečeva, turnir je bio prilika za međusobno druženje i uživanje u netaknutoj prirodi. Sigurni smo da su svi učesnici ponijeli nezaboravne utiske i s radošću iščekuju naredno okupljanje. <a href=\"https://europeangodatabase.eu/EGD/Tournament_Card.php?&key=T250621C\" target=\"_blank\">Rezultati.</a>" },

        new Vijest { Id = 1, culture = Vijest.de,
            Naslov = "Internationales Turnier in Zelenkovac abgehalten",
            Datum = new DateTime(2025, 6, 24),
            Tekst = "Im wunderschönen Bergausflugsort <a href=\"https://www.zelenkovac.com/\" target=\"_blank\">Zelenkovac</a> bei Mrkonjić Grad fand vom 20. bis 22. Juni 2025 ein internationales Go-Turnier statt. Insgesamt nahmen 20 Spielerinnen und Spieler aus sechs Ländern auf zwei Kontinenten teil. Neben den Wettkampfpartien bot das Turnier auch Gelegenheit zum gegenseitigen Kennenlernen und zum Genießen der unberührten Natur. Wir sind sicher, dass alle Teilnehmenden unvergessliche Eindrücke mitgenommen haben und sich schon jetzt auf das nächste Treffen freuen. <a href=\"https://europeangodatabase.eu/EGD/Tournament_Card.php?&key=T250621C\" target=\"_blank\">Ergebnisse.</a>" },

        new Vijest { Id = 1, culture = Vijest.en,
            Naslov = "International tournament held in Zelenkovac",
            Datum = new DateTime(2025, 6, 24),
            Tekst = "In the beautiful mountain resort <a href=\"https://www.zelenkovac.com/\" target=\"_blank\">Zelenkovac</a> near Mrkonjić Grad, an international Go tournament was held from June 20 to 22, 2025. A total of 20 players from six countries on two continents took part in the tournament. In addition to the competitive matches, the event provided an opportunity for socializing and enjoying the unspoiled nature. We are sure that all participants took away unforgettable impressions and are already looking forward to the next gathering. <a href=\"https://europeangodatabase.eu/EGD/Tournament_Card.php?&key=T250621C\" target=\"_blank\">Results.</a>" },

        new Vijest { Id = 2,culture = Vijest.sr,
            Naslov = "Ugostili smo profesionalnog igrač a, Banja Luka Go Open 2025",
            Datum = new DateTime(2025, 9, 15),
            Tekst = "U Prostorijama <a href=\" https://ki.unibl.org/\" target=\"_blank\"> Konfučijevog Instituta Univerziteta </a> u Banja Luci od 12. do 14. septembra 2025.godine održan je je Banja Luka Open Go Turnir. Ovaj turnir je po mnogo čemu bio poseban ko što je veliki broj učesnika, dobra organizacija, odlični domaćini i učešće profesionalnog  go igrač iz Francuske Benjamin Drean-Guenaizia. Svakako će se ovaj turnir dugo pamtiti. <a href=\"https://europeangodatabase.eu/EGD/Tournament_Card.php?&key=T250913C\" target=\"_blank\"> Rezulati </a>." },

        new Vijest { Id = 2,culture = Vijest.de,
            Naslov = "Wir haben einen professionellen Spieler beim Banja Luka Go Open 2025 zu Gast gehabt",
            Datum = new DateTime(2025, 9, 15),
            Tekst = "In den Räumlichkeiten des <a href=\"https://ki.unibl.org/\" target=\"_blank\">Konfuzius-Instituts der Universität</a> in Banja Luka fand vom 12. bis 14. September 2025 das Banja Luka Open Go-Turnier statt. Dieses Turnier war in vielerlei Hinsicht besonders – durch die große Teilnehmerzahl, die gute Organisation, die hervorragenden Gastgeber sowie die Teilnahme des professionellen Go-Spielers aus Frankreich, Benjamin Dréan-Guénaïzia. Dieses Turnier wird sicherlich noch lange in Erinnerung bleiben. <a href=\"https://europeangodatabase.eu/EGD/Tournament_Card.php?&key=T250913C\" target=\"_blank\">Ergebnisse</a>." },

        new Vijest { Id = 2,culture = Vijest.en,
            Naslov = "We had a professional player as a guest at the Banja Luka Go Open 2025",
            Datum = new DateTime(2025, 9, 15),
            Tekst = "On the premises of the <a href=\"https://ki.unibl.org/\" target=\"_blank\">Confucius Institute of the University</a> in Banja Luka, the Banja Luka Open Go Tournament was held from September 12 to 14, 2025. This tournament was special in many ways – due to the large number of participants, good organization, excellent hosts, and the participation of the professional Go player from France, Benjamin Dréan-Guénaïzia. The tournament will certainly be remembered for a long time. <a href=\"https://europeangodatabase.eu/EGD/Tournament_Card.php?&key=T250913C\" target=\"_blank\">Results</a>." },

        new Vijest { Id = 3,culture = Vijest.sr,
            Naslov = "Go turnir \"Zelenkovac 2026\": Barišić i Dubaković na vrhu nakon 5 kola",
            Datum = new DateTime(2026, 6, 11),
            Tekst = "Na međunarodnom Go turniru \"Zelenkovac 2026\", nakon odigranih pet kola, prvo mjesto dijele Dragan Barišić i Dragan Dubaković sa po 25 bodova. Odmah iza njih nalazi se kineski predstavnik Zhou Guanwen sa 24 boda, koliko imaju i Maja Logar, Tadej Turk i Ivan Dubaković. Turnir je okupio 23 takmičara iz više zemalja regiona i svijeta, uključujući Bosnu i Hercegovinu, Srbiju, Sloveniju, Austriju, Kinu i Australiju. Nezavisne novine su napisale <a href=\"https://www.nezavisne.com/novosti/banjaluka/Go-turnir-Zelenkovac-2026-Barisic-i-Dubakovic-na-vrhu-nakon-5-kola/968673/\" target=\"_blank\">izvjestaj</a> o ovom turniru." },

        new Vijest { Id = 3,culture = Vijest.en,
            Naslov = "Go tournament \"Zelenkovac 2026\": Barišić and Dubaković on top after 5 rounds",
            Datum = new DateTime(2026, 6, 11),
            Tekst = "At the international Go tournament \"Zelenkovac 2026\", after five rounds played, the first place is shared by Dragan Barišić and Dragan Dubaković with 25 points each. Right behind them is the Chinese representative Zhou Guanwen with 24 points, the same number as Maja Logar, Tadej Turk and Ivan Dubaković. The tournament brought together 23 competitors from several countries of the region and the world, including Bosnia and Herzegovina, Serbia, Slovenia, Austria, China and Australia. Nezavisne novine wrote a <a href=\"https://www.nezavisne.com/novosti/banjaluka/Go-turnir-Zelenkovac-2026-Barisic-i-Dubakovic-na-vrhu-nakon-5-kola/968673/\" target=\"_blank\">report</a> about this tournament." },

        new Vijest { Id = 3,culture = Vijest.de,
            Naslov = "Go-Turnier „Zelenkovac 2026“: Barišić und Dubaković nach 5 Runden an der Spitze",
            Datum = new DateTime(2026, 6, 11),
            Tekst = "Beim internationalen Go-Turnier „Zelenkovac 2026“ teilen sich nach fünf gespielten Runden Dragan Barišić und Dragan Dubaković mit jeweils 25 Punkten den ersten Platz. Direkt dahinter folgt der chinesische Vertreter Zhou Guanwen mit 24 Punkten, ebenso wie Maja Logar, Tadej Turk und Ivan Dubaković. An dem Turnier nahmen 23 Spieler aus verschiedenen Ländern der Region und der Welt teil, darunter Bosnien und Herzegowina, Serbien, Slowenien, Österreich, China und Australien. Nezavisne novine verfasste einen <a href=\"https://www.nezavisne.com/novosti/banjaluka/Go-turnir-Zelenkovac-2026-Barisic-i-Dubakovic-na-vrhu-nakon-5-kola/968673/\" target=\"_blank\">Bericht</a> über dieses Turnier." },

    };

            return vijesti;
        }

        public IActionResult Vijesti()
        {
            var currentCulture = HttpContext.Session.GetString("Culture") ?? System.Globalization.CultureInfo.CurrentUICulture.Name;
            var vijesti = Vijesti_All().Where(i => i.culture == currentCulture).ToList();
            return View(vijesti);
        }
    }
}
