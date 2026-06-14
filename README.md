# GoBL — Go klub Banja Luka

Web sajt Go kluba iz Banje Luke.

🌐 **[goklubbanjaluka.org](https://goklubbanjaluka.org)**

Sadrži informacije o klubu i igri Go, istoriju, kalendar turnira, raspise sa
online prijavom igrača, vijesti i listu prijatelja kluba. Sajt je višejezičan
(srpski-latinica, engleski, njemački).

## Tehnologije

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core + Pomelo MySQL provider
- Lokalizacija (`sr-Latn`, `en`, `de`) preko `.resx` resursa i sesije
- DocumentFormat.OpenXml — Word (`.docx`) dokumenti se konvertuju u HTML
- Bootstrap 5 + prilagođeni stil (`wwwroot/css/site.css`)

## Konfiguracija

`appsettings.json` **nije u gitu** jer sadrži lozinku baze. Napravi ga iz šablona
`appsettings.example.json` i upiši pravi connection string ka MySQL bazi.

U git se ne commituju: `appsettings.json`, `bin/`, `obj/`, `.vs/`, `*.user`
(vidi `.gitignore`).
