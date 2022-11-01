using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout;
using GatePass.Core.PassAggregate;
using iText.IO.Image;

namespace GatePass.UI.Services;

public class GeneratePDFService : IGeneratePDFService
{
    private readonly IWebHostEnvironment _appEnvironment;

    public GeneratePDFService(IWebHostEnvironment appEnvironment)
    {
        _appEnvironment = appEnvironment;
    }

    public byte[] GenerateSinglePassPdf(SinglePass singlePass)
    {
        MemoryStream ms = new MemoryStream();

        PdfWriter writer = new PdfWriter(ms);
        PdfDocument pdfDoc = new PdfDocument(writer);
        Document document = new Document(pdfDoc, PageSize.A5.Rotate(), false);
        writer.SetCloseStream(false);

        document.SetTopMargin(14);

        Paragraph header = new Paragraph($"NEEPCO, {singlePass.Location?.Name}")
          .SetTextAlignment(TextAlignment.CENTER)
          .SetFontSize(18);

        document.Add(header);

        Paragraph subheader = new Paragraph("Single Day Pass")
          .SetTextAlignment(TextAlignment.CENTER)
          .SetFontSize(14);
        document.Add(subheader);

        // Adding logo to the pdf file
        var logoUrl = System.IO.Path.Combine(_appEnvironment.WebRootPath, "logo.png");

        Image logo = new Image(ImageDataFactory.Create(logoUrl))
                        .SetWidth(50)
                        .SetHeight(50)
                        .SetFixedPosition(30, 350);

        document.Add(logo);

        // empty line
        document.Add(new Paragraph(""));

        // Line separator
        LineSeparator ls = new LineSeparator(new SolidLine());
        document.Add(ls);

        // empty line
        document.Add(new Paragraph(""));

        // Adding visitor image to the pdf
        var visitorImageUrl = System.IO.Path.Combine(
                _appEnvironment.WebRootPath,
                "photos",
                singlePass.Visitor?.PhotoName ?? "placeholder.jpg");

        Image visitorImage = new Image(ImageDataFactory.Create(visitorImageUrl))
                        .SetWidth(120)
                        .SetHeight(110)
                        .SetFixedPosition(430, 215);

        document.Add(visitorImage);

        var fullName = singlePass.Visitor?.FirstName + " " + singlePass.Visitor?.LastName;
        document.Add(createParagraphWithTab("Visitor Name: ", fullName));
        document.Add(createParagraphWithTab("Phone No: ", singlePass.Visitor?.Phone));

        document.Add(createParagraphWithTab("Officer To Visit: ", singlePass.OfficerToVisit));
        document.Add(createParagraphWithTab("Department: ", singlePass.Department));
        document.Add(createParagraphWithTab("No of Companions: ", singlePass.NoOfCompanions.ToString()));

        document.Add(createParagraphWithTab("Visit Date: ", singlePass.VisitDate.ToString("dd MMMM yyyy")));

        var outTime = singlePass.OutTime.Equals(TimeSpan.Zero) ? "_ _" : singlePass.OutTime.ToString(@"hh\:mm");
        document.Add(createParagraphWithTab("In Time & Out Time: ", singlePass.InTime.ToString(@"hh\:mm"), outTime));

        document.Add(createParagraphWithTab("Visitor Address: ", singlePass.Visitor?.Address));

        var signature = new Paragraph("Signature of the Officer Visited")
                            .SetTextAlignment(TextAlignment.RIGHT)
                            .SetMarginTop(30);

        document.Add(signature);

        document.Close();
        byte[] byteInfo = ms.ToArray();
        ms.Write(byteInfo, 0, byteInfo.Length);
        ms.Close();

        return byteInfo;
    }

    public byte[] GenerateMultiPassPdf(MultiplePass multiplePass)
    {
        MemoryStream ms = new MemoryStream();

        PdfWriter writer = new PdfWriter(ms);
        PdfDocument pdfDoc = new PdfDocument(writer);
        Document document = new Document(pdfDoc, PageSize.A5.Rotate(), false);
        writer.SetCloseStream(false);

        document.SetTopMargin(14);

        Paragraph header = new Paragraph($"NEEPCO, {multiplePass.Location?.Name}")
          .SetTextAlignment(TextAlignment.CENTER)
          .SetFontSize(18);

        document.Add(header);

        Paragraph subheader = new Paragraph("Multiple Day Pass")
          .SetTextAlignment(TextAlignment.CENTER)
          .SetFontSize(14);
        document.Add(subheader);

        // Adding logo to the pdf file
        var logoUrl = System.IO.Path.Combine(_appEnvironment.WebRootPath, "logo.png");

        Image logo = new Image(ImageDataFactory.Create(logoUrl))
                        .SetWidth(50)
                        .SetHeight(50)
                        .SetFixedPosition(30, 350);

        document.Add(logo);

        // empty line
        document.Add(new Paragraph(""));

        // Line separator
        LineSeparator ls = new LineSeparator(new SolidLine());
        document.Add(ls);

        // empty line
        document.Add(new Paragraph(""));

        // Adding visitor image to the pdf
        var visitorImageUrl = System.IO.Path.Combine(
                _appEnvironment.WebRootPath,
                "photos",
                multiplePass.Visitor?.PhotoName!);

        Image visitorImage = new Image(ImageDataFactory.Create(visitorImageUrl))
                        .SetWidth(120)
                        .SetHeight(110)
                        .SetFixedPosition(440, 215);

        document.Add(visitorImage);

        var fullName = multiplePass.Visitor?.FirstName + " " + multiplePass.Visitor?.LastName;
        document.Add(createParagraphWithTab("Visitor Name: ", fullName));
        document.Add(createParagraphWithTab("Phone No: ", multiplePass.Visitor?.Phone));
        
        document.Add(createParagraphWithTab("From Date: ", multiplePass.FromDate.ToString("dd MMM yyyy")));
        document.Add(createParagraphWithTab("End Date: ", multiplePass.TillDate.ToString("dd MMM yyyy")));
        document.Add(createParagraphWithTab("Department: ", multiplePass.Department));
        document.Add(createParagraphWithTab("Purpose: ", multiplePass.Purpose));
        document.Add(createParagraphWithTab("Visitor Address: ", multiplePass.Visitor?.Address));

        var signature = new Paragraph("Signature of HOD")
                            .SetTextAlignment(TextAlignment.RIGHT)
                            .SetMarginTop(50);

        document.Add(signature);

        document.Close();
        byte[] byteInfo = ms.ToArray();
        ms.Write(byteInfo, 0, byteInfo.Length);
        ms.Close();

        return byteInfo;
    }

    private static Paragraph createParagraphWithTab(string key, string? value1, string? value2 = null)
    {
        Paragraph p = new Paragraph();
        p.AddTabStops(new TabStop(200f, TabAlignment.LEFT));
        p.Add(key);
        p.Add(new Tab());
        p.Add(value1);
        if (value2 != null)
        {
            p.Add(new Tab());
            p.Add(value2);
        }
        return p;
    }
}
