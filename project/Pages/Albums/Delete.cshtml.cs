using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using project.Models;

namespace project.Pages.Albums;

public class DeleteModel : PageModel
{
    private readonly ChinookDbContext _context;

    public DeleteModel(ChinookDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Album Album { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Album = await _context.Albums
            .Include(a => a.Artist)
            .FirstOrDefaultAsync(a => a.AlbumId == id);

        if (Album == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var album = await _context.Albums
                                  .Include(a => a.Tracks)
                                  .FirstOrDefaultAsync(a => a.AlbumId == id);

        if (album == null)
            return NotFound();

        if (album.Tracks.Any())
        {
            _context.Tracks.RemoveRange(album.Tracks);
        }

        // Remove the album
        _context.Albums.Remove(album);

        await _context.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}
