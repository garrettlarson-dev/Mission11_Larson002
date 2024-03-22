using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission11.Models;

namespace Mission11.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BookstoreContext _bookstoreContext;
    public static int PageSize = 10; // Number of books per page

    public HomeController(ILogger<HomeController> logger, BookstoreContext context)
    {
        _logger = logger;
        _bookstoreContext = context;
    }

    public IActionResult Index(int? page)
    {
        int currentPage = page ?? 1;
        // Assuming you have an IQueryable<Book> available as _context.Books
        var paginatedBooks = _bookstoreContext.Books
            .OrderBy(b => b.Title)
            .Skip((currentPage - 1) * PageSize)
            .Take(PageSize)
            .ToList();
        int totalBooks = _bookstoreContext.Books.Count();
        ViewBag.TotalBooks = totalBooks;
        ViewBag.CurrentPage = currentPage;
        // If you have a model for pagination, you should pass the total number of books to it to generate page links.
        // We'll assume here that you're only passing the paginated list to the view.
            
        return View(paginatedBooks);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}