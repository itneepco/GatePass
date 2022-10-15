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

        Paragraph header = new Paragraph("North Eastern Electric Power Corporation")
          .SetTextAlignment(TextAlignment.CENTER)
          .SetFontSize(20);

        document.Add(header);

        Paragraph subheader = new Paragraph("Single Day Pass")
          .SetTextAlignment(TextAlignment.CENTER)
          .SetFontSize(15);
        document.Add(subheader);

        // Adding logo to the pdf file
        var logoUrl = System.IO.Path.Combine(_appEnvironment.WebRootPath, "logo.png");

        Image logo = new Image(ImageDataFactory.Create(logoUrl))
                        .SetWidth(45)
                        .SetHeight(45)
                        .SetFixedPosition(30, 340);

        document.Add(logo);

        // empty line
        document.Add(new Paragraph(""));

        // Line separator
        LineSeparator ls = new LineSeparator(new SolidLine());
        document.Add(ls);

        // empty line
        document.Add(new Paragraph(""));

        var fullName = singlePass.Visitor!.FirstName + " " + singlePass.Visitor!.LastName;
        document.Add(createParagraphWithTab("Visitor Name: ", fullName));
        document.Add(createParagraphWithTab("Phone No: ", singlePass.Visitor!.Phone));
        document.Add(createParagraphWithTab("Visitor Address: ", singlePass.Visitor!.Address));

        document.Add(createParagraphWithTab("Officer To Visit: ", singlePass.OfficerToVisit));
        document.Add(createParagraphWithTab("Department: ", singlePass.Department));
        document.Add(createParagraphWithTab("No of Companions: ", singlePass.NoOfCompanions.ToString()));

        document.Add(createParagraphWithTab("Visit Date: ", singlePass.VisitDate.ToString("dd MMMM yyyy")));
        document.Add(createParagraphWithTab("In Time & Out Time: ", singlePass.InTime.ToString(@"hh\:mm"), singlePass.OutTime.ToString(@"hh\:mm")));

        var signature = new Paragraph("Signature of the Officer Visited")
                            .SetTextAlignment(TextAlignment.RIGHT)
                            .SetMarginTop(30);

        document.Add(signature);

        // Adding visitor image to the pdf
        var visitorImageUrl = System.IO.Path.Combine(
                _appEnvironment.WebRootPath, 
                "photos", 
                singlePass.Visitor!.PhotoName!);

        Image visitorImage = new Image(ImageDataFactory.Create(visitorImageUrl))
                        .SetWidth(120)
                        .SetHeight(110)
                        .SetFixedPosition(430, 190);

        document.Add(visitorImage);

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

        Paragraph header = new Paragraph("North Eastern Electric Power Corporation")
          .SetTextAlignment(TextAlignment.CENTER)
          .SetFontSize(20);

        document.Add(header);

        Paragraph subheader = new Paragraph("Multiple Day Pass")
          .SetTextAlignment(TextAlignment.CENTER)
          .SetFontSize(15);
        document.Add(subheader);

        // Adding logo to the pdf file
        var logoUrl = System.IO.Path.Combine(_appEnvironment.WebRootPath, "logo.png");

        Image logo = new Image(ImageDataFactory.Create(logoUrl))
                        .SetWidth(45)
                        .SetHeight(45)
                        .SetFixedPosition(30, 340);

        document.Add(logo);

        // empty line
        document.Add(new Paragraph(""));

        // Line separator
        LineSeparator ls = new LineSeparator(new SolidLine());
        document.Add(ls);

        // empty line
        document.Add(new Paragraph(""));

        var fullName = multiplePass.Visitor!.FirstName + " " + multiplePass.Visitor!.LastName;
        document.Add(createParagraphWithTab("Visitor Name: ", fullName));
        document.Add(createParagraphWithTab("Phone No: ", multiplePass.Visitor!.Phone));
        document.Add(createParagraphWithTab("Visitor Address: ", multiplePass.Visitor!.Address));

        document.Add(createParagraphWithTab("From Date: ", multiplePass.FromDate.ToString("dd MMM yyyy")));
        document.Add(createParagraphWithTab("End Date: ", multiplePass.TillDate.ToString("dd MMM yyyy")));
        document.Add(createParagraphWithTab("Department: ", multiplePass.Department));
        document.Add(createParagraphWithTab("Purpose: ", multiplePass.Purpose));

        // Adding visitor image to the pdf
        var visitorImageUrl = System.IO.Path.Combine(
                _appEnvironment.WebRootPath,
                "photos",
                multiplePass.Visitor!.PhotoName!);

        Image visitorImage = new Image(ImageDataFactory.Create(visitorImageUrl))
                        .SetWidth(120)
                        .SetHeight(110)
                        .SetFixedPosition(440, 190);

        document.Add(visitorImage);

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

    private static Paragraph createParagraphWithTab(string key, string value1, string value2 = null)
    {
        Paragraph p = new Paragraph();
        p.AddTabStops(new TabStop(200f, TabAlignment.LEFT));
        p.Add(key);
        p.Add(new Tab());
        p.Add(value1);
        if (value2 != null && value2.Equals("00.00"))
        {
            p.Add(new Tab());
            p.Add(value2);
        }
        return p;
    }
}
