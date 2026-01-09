using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using eTravelAgencija.Model.ResponseObject;

namespace eTravelAgencija.Services.Utils.Pdf
{
    public static class BillPdf
    {
        public static byte[] Generate(BillResponse bill)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    // ================= HEADER =================
                    page.Header().Column(col =>
                    {
                        col.Item().Text("RAČUN")
                            .FontSize(22)
                            .Bold()
                            .AlignCenter();

                        col.Item().PaddingTop(10).Row(row =>
                        {
                            row.RelativeItem().Text($"Broj rezervacije: {bill.ReservationId}");
                            row.ConstantItem(200)
                                .AlignRight()
                                .Text($"Datum: {bill.CreatedAt:dd.MM.yyyy}");
                        });

                        col.Item().PaddingVertical(10)
                            .LineHorizontal(1);
                    });

                    // ================= CONTENT =================
                    page.Content().Column(col =>
                    {
                        col.Spacing(10);

                        // ===== PODACI O PUTNIKU =====
                        col.Item().Text("Podaci o putniku").Bold();
                        BuildRow(col, "Ime i prezime", bill.UserFullName);

                        col.Item().PaddingVertical(6)
                            .LineHorizontal(0.5f);

                        // ===== PODACI O PUTOVANJU =====
                        col.Item().Text("Detalji putovanja").Bold();
                        BuildRow(col, "Ponuda", bill.OfferTitle);
                        BuildRow(col, "Hotel", $"{bill.HotelName} ({bill.HotelStars}★)");
                        BuildRow(col, "Tip sobe", bill.RoomType);

                        col.Item().PaddingVertical(10)
                            .LineHorizontal(1);

                        // ===== FINANSIJE =====
                        col.Item().Text("Obračun").Bold();

                        BuildRow(col, "Putovanje", $"{bill.TravelPrice:F2} KM");
                        BuildRow(col, "Boravišna taksa", $"{bill.ResidenceTax:F2} KM");

                        if (bill.Insurance > 0)
                        {
                            BuildRow(col, "Putničko osiguranje", $"{bill.Insurance:F2} KM");
                        }

                        if (bill.IsDiscountUsed)
                        {
                            BuildRow(
                                col,
                                "Vaučer iskorišten",
                                $"-{bill.DiscountPercent:F0}%"
                            );
                        }

                        col.Item().PaddingVertical(10)
                            .LineHorizontal(1);

                        col.Item()
                            .AlignRight()
                            .Text($"UKUPNO: {bill.Total:F2} KM")
                            .FontSize(16)
                            .Bold();
                    });

                    // ================= FOOTER =================
                    page.Footer().PaddingTop(30).Column(col =>
                    {
                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Column(left =>
                            {
                                left.Item().Text("_________________________");
                                left.Item().Text("Potpis putnika")
                                    .AlignCenter()
                                    .FontSize(10);
                            });

                            row.RelativeItem().AlignRight().Column(right =>
                            {
                                right.Item().Text("_________________________");
                                right.Item().Text("Odgovorno lice")
                                    .AlignCenter()
                                    .FontSize(10);
                            });
                        });

                        col.Item().PaddingTop(20).AlignCenter().Text(text =>
                        {
                            text.Span("eTravelAgencija © ");
                            text.Span(DateTime.UtcNow.Year.ToString());
                        });
                    });
                });
            });

            return document.GeneratePdf();
        }

        private static void BuildRow(ColumnDescriptor col, string label, string value)
        {
            col.Item().Row(row =>
            {
                row.RelativeItem().Text(label);
                row.ConstantItem(200).AlignRight().Text(value).Bold();
            });
        }
    }
}
