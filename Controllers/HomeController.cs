using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Borders;
using iText.Kernel.Colors;
using iText.Kernel.Font; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetAdoptionAPI.Data;
using System.IO;
using System.Threading.Tasks;
using PetAdoptionAPI.Models;
using System.Diagnostics;

namespace PetAdoptionAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PetAdoptionContext _context;

        public HomeController(ILogger<HomeController> logger, PetAdoptionContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }
        public IActionResult TnC()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        public async Task<IActionResult> Report()
        {
            // Fetch the data for the report preview
            var users = await _context.Users.ToListAsync();
            var pets = await _context.Pets.ToListAsync();
            var adoptions = await _context.Adoptions
                .Include(a => a.User)
                .Include(a => a.Pet)
                .ToListAsync();

            // Create a ViewModel to pass the data to the view
            var reportData = new ReportViewModel
            {
                Users = users,
                Pets = pets,
                Adoptions = adoptions
            };

            // Pass the data to the view for preview
            return View(reportData);
        }




        [HttpPost]
        public async Task<IActionResult> GenerateReport()
        {
            // Fetch data
            var users = await _context.Users.ToListAsync();
            var pets = await _context.Pets.ToListAsync();
            var adoptions = await _context.Adoptions
                .Include(a => a.User)
                .Include(a => a.Pet)
                .ToListAsync();

            // Create PDF in memory
            using var stream = new MemoryStream();
            var writer = new PdfWriter(stream);
            var pdf = new PdfDocument(writer);
            var document = new iText.Layout.Document(pdf); // Fully qualify 'Document'

            // Use font directly from font file or system font name
            var font = PdfFontFactory.CreateFont("Helvetica-Bold");
            var fontItalic = PdfFontFactory.CreateFont("Helvetica-Oblique");

            // Add title
            var title = new Paragraph("Pet Adoption Report")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(24)
                .SetFontColor(ColorConstants.BLACK)
                .SetFont(font); // Use direct font instead of FontConstants
            document.Add(title);

            var subtitle = new Paragraph($"Generated on: {DateTime.UtcNow:yyyy-MM-dd}")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(10)
                .SetFont(fontItalic); // Use direct font instead of FontConstants
            document.Add(subtitle);

            document.Add(new Paragraph("\n")); // Add spacing

            // Add Users Table
            document.Add(CreateSectionHeader("Users", font));
            var usersTable = CreateTable(new string[] { "ID", "Name", "Email", "Contact", "Role" });
            foreach (var user in users)
            {
                usersTable.AddCell(user.UserID.ToString());
                usersTable.AddCell(user.Name);
                usersTable.AddCell(user.Email);
                usersTable.AddCell(user.PhoneNumber);
                usersTable.AddCell(user.Role);
            }
            document.Add(usersTable);

            // Add Pets Table
            document.Add(CreateSectionHeader("Pets", font));
            var petsTable = CreateTable(new string[] { "ID", "Name", "Age", "Breed", "Adoption Status" });
            foreach (var pet in pets)
            {
                petsTable.AddCell(pet.PetID.ToString());
                petsTable.AddCell(pet.Name);
                petsTable.AddCell(pet.Age);
                petsTable.AddCell(pet.Breed);
                petsTable.AddCell(pet.AdoptionStatus);
            }
            document.Add(petsTable);

            // Add Adoptions Table
            document.Add(CreateSectionHeader("Adoptions", font));
            var adoptionsTable = CreateTable(new string[] { "ID", "User", "Contact","Pet", "Status", "Date" });
            foreach (var adoption in adoptions)
            {
                adoptionsTable.AddCell(adoption.AdoptionID.ToString());
                adoptionsTable.AddCell(adoption.User.Name);
                adoptionsTable.AddCell(adoption.User.PhoneNumber);
                adoptionsTable.AddCell(adoption.Pet.Name);
                adoptionsTable.AddCell(adoption.Status);
                adoptionsTable.AddCell(adoption.RequestDate.ToString("yyyy-MM-dd"));
            }
            document.Add(adoptionsTable);

            // Close the document
            document.Close();

            // Return the PDF
            return File(stream.ToArray(), "application/pdf", "PetAdoptionReport.pdf");
        }




        [HttpPost]
        public async Task<IActionResult> GenerateReportUser()
        {
            // Fetch data
            var users = await _context.Users.ToListAsync();
           
            var adoptions = await _context.Adoptions
                .Include(a => a.User)
                
                .ToListAsync();

            // Create PDF in memory
            using var stream = new MemoryStream();
            var writer = new PdfWriter(stream);
            var pdf = new PdfDocument(writer);
            var document = new iText.Layout.Document(pdf); // Fully qualify 'Document'

            // Use font directly from font file or system font name
            var font = PdfFontFactory.CreateFont("Helvetica-Bold");
            var fontItalic = PdfFontFactory.CreateFont("Helvetica-Oblique");

            // Add title
            var title = new Paragraph("User Lists Report")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(24)
                .SetFontColor(ColorConstants.BLACK)
                .SetFont(font); // Use direct font instead of FontConstants
            document.Add(title);

            var subtitle = new Paragraph($"Generated on: {DateTime.UtcNow:yyyy-MM-dd}")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(10)
                .SetFont(fontItalic); // Use direct font instead of FontConstants
            document.Add(subtitle);

            document.Add(new Paragraph("\n")); // Add spacing

            // Add Users Table
            document.Add(CreateSectionHeader("Users", font));
            var usersTable = CreateTable(new string[] { "ID", "Name", "Email", "Contact","Role" });
            foreach (var user in users)
            {
                usersTable.AddCell(user.UserID.ToString());
                usersTable.AddCell(user.Name);
                usersTable.AddCell(user.Email);
                usersTable.AddCell(user.PhoneNumber);
                usersTable.AddCell(user.Role);
            }
            document.Add(usersTable);

            
           
            // Close the document
            document.Close();

            // Return the PDF
            return File(stream.ToArray(), "application/pdf", "UserListsReport.pdf");
        }

        [HttpPost]
        public async Task<IActionResult> GenerateReportPet()
        {
            // Fetch data
            var pets = await _context.Pets.ToListAsync();

            var adoptions = await _context.Adoptions
                .Include(a => a.Pet)

                .ToListAsync();

            // Create PDF in memory
            using var stream = new MemoryStream();
            var writer = new PdfWriter(stream);
            var pdf = new PdfDocument(writer);
            var document = new iText.Layout.Document(pdf); // Fully qualify 'Document'

            // Use font directly from font file or system font name
            var font = PdfFontFactory.CreateFont("Helvetica-Bold");
            var fontItalic = PdfFontFactory.CreateFont("Helvetica-Oblique");

            // Add title
            var title = new Paragraph("Pet Lists Report")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(24)
                .SetFontColor(ColorConstants.BLACK)
                .SetFont(font); // Use direct font instead of FontConstants
            document.Add(title);

            var subtitle = new Paragraph($"Generated on: {DateTime.UtcNow:yyyy-MM-dd}")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(10)
                .SetFont(fontItalic); // Use direct font instead of FontConstants
            document.Add(subtitle);

            document.Add(new Paragraph("\n")); // Add spacing

            // Add Pets Table
            document.Add(CreateSectionHeader("Pets", font));
            var petsTable = CreateTable(new string[] { "ID", "Name", "Age", "Breed", "Adoption Status" });
            foreach (var pet in pets)
            {
                petsTable.AddCell(pet.PetID.ToString());
                petsTable.AddCell(pet.Name);
                petsTable.AddCell(pet.Age);
                petsTable.AddCell(pet.Breed);
                petsTable.AddCell(pet.AdoptionStatus);
            }
            document.Add(petsTable);


            // Close the document
            document.Close();

            // Return the PDF
            return File(stream.ToArray(), "application/pdf", "PetListsReport.pdf");
        }

        [HttpPost]
        public async Task<IActionResult> GenerateReportAdoption()
        {
            // Fetch data
            var users = await _context.Users.ToListAsync();
            var pets = await _context.Pets.ToListAsync();
            var adoptions = await _context.Adoptions
                .Include(a => a.User)
                .Include(a => a.Pet)
                .ToListAsync();

            // Create PDF in memory
            using var stream = new MemoryStream();
            var writer = new PdfWriter(stream);
            var pdf = new PdfDocument(writer);
            var document = new iText.Layout.Document(pdf); // Fully qualify 'Document'

            // Use font directly from font file or system font name
            var font = PdfFontFactory.CreateFont("Helvetica-Bold");
            var fontItalic = PdfFontFactory.CreateFont("Helvetica-Oblique");

            // Add title
            var title = new Paragraph("Adoption Lists Report")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(24)
                .SetFontColor(ColorConstants.BLACK)
                .SetFont(font); // Use direct font instead of FontConstants
            document.Add(title);

            var subtitle = new Paragraph($"Generated on: {DateTime.UtcNow:yyyy-MM-dd}")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(10)
                .SetFont(fontItalic); // Use direct font instead of FontConstants
            document.Add(subtitle);

            document.Add(new Paragraph("\n")); // Add spacing

            // Add Adoptions Table
            document.Add(CreateSectionHeader("Adoptions", font));
            var adoptionsTable = CreateTable(new string[] { "ID", "User","Contact", "Pet", "Status", "Date" });
            foreach (var adoption in adoptions)
            {
                adoptionsTable.AddCell(adoption.AdoptionID.ToString());
                adoptionsTable.AddCell(adoption.User.Name);
                adoptionsTable.AddCell(adoption.User.PhoneNumber);
                adoptionsTable.AddCell(adoption.Pet.Name);
                adoptionsTable.AddCell(adoption.Status);
                adoptionsTable.AddCell(adoption.RequestDate.ToString("yyyy-MM-dd"));
            }
            document.Add(adoptionsTable);


            // Close the document
            document.Close();

            // Return the PDF
            return File(stream.ToArray(), "application/pdf", "AdoptionListsReport.pdf");
        }

        private iText.Layout.Element.Paragraph CreateSectionHeader(string title, PdfFont font)
        {
            return new iText.Layout.Element.Paragraph(title) // Fully qualify 'Paragraph'
                .SetFontSize(18)
                .SetFontColor(ColorConstants.DARK_GRAY)
                .SetFont(font) // Use passed font for header
                .SetUnderline()
                .SetTextAlignment(TextAlignment.LEFT);
        }




        private iText.Layout.Element.Table CreateTable(string[] headers)
        {
            var table = new iText.Layout.Element.Table(headers.Length).UseAllAvailableWidth(); // Fully qualify 'Table'
            foreach (var header in headers)
            {
                table.AddHeaderCell(new iText.Layout.Element.Cell() // Fully qualify 'Cell'
                    .Add(new iText.Layout.Element.Paragraph(header) // Fully qualify 'Paragraph'
                        .SetFontColor(ColorConstants.WHITE)
                        .SetBackgroundColor(ColorConstants.GRAY))
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetBorder(Border.NO_BORDER));
            }
            return table;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
