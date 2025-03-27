using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using project.Models;

namespace project.Pages.Albums;

public class IndexModel : PageModel
{
    private readonly ChinookDbContext _context;

    public IndexModel(ChinookDbContext context)
    {
        _context = context;
    }

    public IList<Album> Albums { get; set; } = new List<Album>();

    [BindProperty(SupportsGet = true)]
    public string? SearchTerm { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? SortField { get; set; }

    [BindProperty(SupportsGet = true)]
    public bool Descending { get; set; }

    public async Task OnGetAsync()
    {
        var query = _context.Albums.Include(a => a.Artist).Include(a => a.Tracks).AsQueryable();

        if (!string.IsNullOrEmpty(SearchTerm))
        {
            var lowered = SearchTerm.ToLower();
            query = query.Where(a =>
                a.Title.ToLower().Contains(lowered) ||
                a.Artist!.Name.ToLower().Contains(lowered));
        }

        query = (SortField, Descending) switch
        {
            ("Title", true) => query.OrderByDescending(a => a.Title),
            ("Artist", true) => query.OrderByDescending(a => a.Artist!.Name),
            ("Tracks", true) => query.OrderByDescending(a => a.Tracks.Count),
            ("Artist", false) => query.OrderBy(a => a.Artist!.Name),
            ("Tracks", false) => query.OrderBy(a => a.Tracks.Count),
            _ => query.OrderBy(a => a.Title),
        };

        Albums = await query.ToListAsync();
    }
}
