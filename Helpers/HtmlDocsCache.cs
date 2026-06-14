using System.Xml.Linq;
//using Aspose.Words;
using System.Runtime.Caching;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using System.Text;


using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;


namespace GoBL.Helpers
{
    public static class HtmlDocsCache
    {
        private static readonly Lazy<string> _raspisHtml_en = new Lazy<string>(() =>
        {
            var basePath_en = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "docs", "raspis_en.docx");
            return ConvertWordToHtml(basePath_en);
        });
        private static readonly Lazy<string> _raspisHtml_de = new Lazy<string>(() =>
        {
            var basePath_en = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "docs", "raspis_de.docx");
            return ConvertWordToHtml(basePath_en);
        });

        private static readonly Lazy<string> _raspisHtml_sr_latinica = new Lazy<string>(() =>
        {
            var basePath_sr_latinica = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "docs", "raspis_sr_latinica.docx");
            return ConvertWordToHtml(basePath_sr_latinica);
        });


        private static readonly Lazy<string> _raspis_2_Html_en = new Lazy<string>(() =>
        {
            var basePath_en = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "docs", "raspis_2_en.docx");
            return ConvertWordToHtml(basePath_en);
        });
        private static readonly Lazy<string> _raspis_2_Html_de = new Lazy<string>(() =>
        {
            var basePath_en = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "docs", "raspis_2_en.docx");
            return ConvertWordToHtml(basePath_en);
        });

        private static readonly Lazy<string> _raspis_2_Html_sr_latinica = new Lazy<string>(() =>
        {
            var basePath_sr_latinica = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "docs", "raspis_2_sr_latinica.docx");
            return ConvertWordToHtml(basePath_sr_latinica);
        });


        private static readonly Lazy<string> _ogou = new Lazy<string>(() =>
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "docs", "ogou.docx");
            return ConvertWordToHtml(basePath);
        });

        private static readonly Lazy<string> _istorijakluba = new Lazy<string>(() =>
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "docs", "istorijakluba.docx");
            return ConvertWordToHtml(basePath);
        });

        public static string RaspisHtml_en => _raspisHtml_en.Value;
        public static string RaspisHtml_de => _raspisHtml_de.Value;
        public static string RaspisHtml_sr_latinica => _raspisHtml_sr_latinica.Value;

        public static string Raspis_2_Html_en => _raspis_2_Html_en.Value;
        public static string Raspis_2_Html_de => _raspis_2_Html_de.Value;
        public static string Raspis_2_Html_sr_latinica => _raspis_2_Html_sr_latinica.Value;

        public static string Ogou => _ogou.Value;
        public static string IstorijaKluba => _istorijakluba.Value;

        public static string ConvertWordToHtml(string docxPath)
        {
            try
            {
                var html = new StringBuilder();
                html.Append("<div class=\"docx-content\">");

                // Da li je prethodno emitovan blok bila slika (radi prepoznavanja potpisa ispod slike)
                bool lastWasFigure = false;

                using (WordprocessingDocument doc = WordprocessingDocument.Open(docxPath, false))
                {
                    var body = doc.MainDocumentPart?.Document.Body;
                    if (body == null) return "Dokument nije validan.";

                    foreach (var element in body.ChildElements)
                    {
                        if (element is Paragraph paragraph)
                        {
                            // Prikupi tekst i slike iz paragrafa
                            var textSb = new StringBuilder();
                            var images = new List<string>();

                            foreach (var run in paragraph.Elements<Run>())
                            {
                                var drawing = run.Elements<Drawing>().FirstOrDefault();
                                if (drawing != null)
                                {
                                    var img = ProcessFullSizeImage(doc.MainDocumentPart, drawing);
                                    if (!string.IsNullOrEmpty(img)) images.Add(img);
                                }

                                foreach (var child in run.ChildElements)
                                {
                                    if (child is Text text)
                                    {
                                        textSb.Append(ApplyTextFormatting(run, text.Text));
                                    }
                                    else if (child is Break)
                                    {
                                        textSb.Append("<br />");
                                    }
                                    else if (child is TabChar)
                                    {
                                        textSb.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
                                    }
                                }
                            }

                            string textHtml = textSb.ToString();
                            string plain = paragraph.InnerText?.Trim() ?? string.Empty;
                            bool hasImages = images.Count > 0;
                            bool hasText = !string.IsNullOrWhiteSpace(plain);

                            // Prazan paragraf — preskoči (razmak rješava CSS), zadrži stanje potpisa
                            if (!hasImages && !hasText)
                                continue;

                            // Paragraf sa slikama → galerija (jedna ili više slika u redu)
                            if (hasImages)
                            {
                                string mod = images.Count > 1 ? "docx-figs--multi" : "docx-figs--single";
                                html.Append($"<div class=\"docx-figs {mod}\">");
                                foreach (var img in images) html.Append(img);
                                html.Append("</div>");

                                if (hasText)
                                    html.Append($"<p class=\"docx-caption\">{textHtml}</p>");

                                lastWasFigure = true;
                                continue;
                            }

                            // Naslov sekcije (kratak red ispisan VELIKIM slovima)
                            if (IsHeading(plain))
                            {
                                html.Append($"<h2 class=\"docx-h\">{textHtml}</h2>");
                            }
                            // Kratak tekst odmah ispod slike → potpis
                            else if (lastWasFigure && plain.Length <= 90)
                            {
                                html.Append($"<p class=\"docx-caption\">{textHtml}</p>");
                            }
                            else
                            {
                                html.Append($"<p>{textHtml}</p>");
                            }

                            lastWasFigure = false;
                        }
                    }
                }

                html.Append("</div>");
                return html.ToString();
            }
            catch (Exception ex)
            {
                return $"<p>Greška pri konverziji: {ex.Message}</p>";
            }
        }

        private static string ProcessFullSizeImage(MainDocumentPart mainPart, Drawing drawing)
        {
            var blip = drawing.Descendants<DocumentFormat.OpenXml.Drawing.Blip>().FirstOrDefault();
            if (blip?.Embed != null)
            {
                var imagePart = (ImagePart)mainPart.GetPartById(blip.Embed.Value);
                using (var stream = imagePart.GetStream())
                {
                    byte[] imageBytes = new byte[stream.Length];
                    stream.Read(imageBytes, 0, (int)stream.Length);
                    string imageType = GetImageFormatForPart(imagePart);

                    // Pronalaženje originalnih dimenzija
                    var extents = drawing.Descendants<DocumentFormat.OpenXml.Drawing.Extents>().FirstOrDefault();
                    long widthEmu = extents?.Cx?.Value ?? 0;
                    long heightEmu = extents?.Cy?.Value ?? 0;

                    // Konverzija iz EMU u piksele (1cm = 360000 EMU)
                    int widthPx = (int)(widthEmu / 360000.0 * 96); // 96 DPI
                    int heightPx = (int)(heightEmu / 360000.0 * 96);

                    // Fallback dimenzije
                    if (widthPx == 0) widthPx = 150;
                    if (heightPx == 0) heightPx = (int)(widthPx * 0.75);

                    // width/height kao atributi čuvaju razmjeru; CSS (.docx-img) ih čini responzivnim
                    return $"<img class=\"docx-img\" src=\"data:image/{imageType};base64,{Convert.ToBase64String(imageBytes)}\" " +
                           $"width=\"{widthPx}\" height=\"{heightPx}\" loading=\"lazy\" alt=\"\" />";
                }
            }
            return string.Empty;
        }

        // Prepoznaje kratke naslove sekcija pisane isključivo VELIKIM slovima
        private static bool IsHeading(string text)
        {
            text = text.Trim();
            if (text.Length == 0 || text.Length > 60) return false;

            bool hasLetter = false;
            foreach (char c in text)
            {
                if (char.IsLetter(c))
                {
                    hasLetter = true;
                    if (!char.IsUpper(c)) return false;
                }
            }
            return hasLetter;
        }

        private static string ApplyTextFormatting(Run run, string text)
        {
            var props = run.RunProperties;
            var result = new StringBuilder();

            if (props?.Bold != null) result.Append("<strong>");
            if (props?.Italic != null) result.Append("<em>");
            if (props?.Underline != null) result.Append("<u>");

            var safeText = System.Net.WebUtility.HtmlEncode(text.Replace("\t", "    ")); // 4 razmaka
            result.Append(safeText);

            if (props?.Underline != null) result.Append("</u>");
            if (props?.Italic != null) result.Append("</em>");
            if (props?.Bold != null) result.Append("</strong>");

            return result.ToString();
        }

        private static string GetImageFormatForPart(ImagePart imagePart)
        {
            switch (imagePart.ContentType)
            {
                case "image/png": return "png";
                case "image/jpeg": return "jpeg";
                case "image/gif": return "gif";
                case "image/bmp": return "bmp";
                case "image/webp": return "webp";
                default:
                    // Probaj da prepoznaš format iz ekstenzije
                    string filename = imagePart.Uri.OriginalString;
                    if (filename.EndsWith(".png", StringComparison.OrdinalIgnoreCase)) return "png";
                    if (filename.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                        filename.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase)) return "jpeg";
                    if (filename.EndsWith(".gif", StringComparison.OrdinalIgnoreCase)) return "gif";
                    return "png"; // Default
            }
        }

        //public static string ConvertWordToHtml(string filePath)
        //{
        //    try
        //    {
        //        using (var doc = WordprocessingDocument.Open(filePath, false))
        //        {
        //            var settings = new HtmlConverterSettings()
        //            {
        //                PageTitle = "Naslov"
        //            };
        //            XElement html = HtmlConverter.ConvertToHtml(doc, settings);
        //            return html.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return $"<p>Greška pri konverziji: {ex.Message}</p>";
        //    }
        //}


        //private static string ConvertWordToHtml(string filePath)
        //{
        //    try
        //    {
        //        var doc = new Aspose.Words.Document(filePath);
        //        var options = new Aspose.Words.Saving.HtmlSaveOptions(Aspose.Words.SaveFormat.Html)
        //        {
        //            ExportImagesAsBase64 = true
        //        };
        //        using (var stream = new MemoryStream())
        //        {
        //            doc.Save(stream, options);
        //            stream.Position = 0;
        //            using (var reader = new StreamReader(stream))
        //            {
        //                return reader.ReadToEnd();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return $"<p>Greška pri konverziji: {ex.Message}</p>";
        //    }
        //}
    }
}
