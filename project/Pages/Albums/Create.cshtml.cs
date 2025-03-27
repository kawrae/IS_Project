using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project.Models;

namespace project.Pages.Albums;

public class CreateModel : PageModel
{
    private readonly ChinookDbContext _context;

    public CreateModel(ChinookDbContext context)
    {
        _context = context;
    }



    [BindProperty]
    public Album Album { get; set; } = new();

    [BindProperty]
    public List<string> TrackNames { get; set; } = new();
    [BindProperty]
    public List<decimal> TrackPrices { get; set; } = new();
    [BindProperty]
    public string? NewArtistName { get; set; }




    public SelectList ArtistList { get; set; }



    public async Task<IActionResult> OnGetAsync()
    {
        TrackNames.Add("");
        TrackPrices.Add(0.99m);

        ArtistList = new SelectList(await _context.Artists.OrderBy(a => a.Name).ToListAsync(), "ArtistId", "Name");
        return Page();
    }


    public async Task<IActionResult> OnPostAsync()
    {
        if (!string.IsNullOrWhiteSpace(NewArtistName))
        {
            var existing = await _context.Artists.FirstOrDefaultAsync(a => a.Name.ToLower() == NewArtistName.ToLower());
            if (existing == null)
            {
                var newArtist = new Artist { Name = NewArtistName.Trim() };
                _context.Artists.Add(newArtist);
                await _context.SaveChangesAsync();
                Album.ArtistId = newArtist.ArtistId;
            }
            else
            {
                Album.ArtistId = existing.ArtistId;
            }
        }

        if (!ModelState.IsValid)
        {
            ArtistList = new SelectList(await _context.Artists.OrderBy(a => a.Name).ToListAsync(), "ArtistId", "Name");
            return Page();
        }

        Album.Title = Album.Title?.Trim();

        _context.Albums.Add(Album);
        await _context.SaveChangesAsync();

        for (int i = 0; i < TrackNames.Count; i++)
        {
            var trackName = TrackNames[i];
            var trackPrice = TrackPrices.ElementAtOrDefault(i);

            if (!string.IsNullOrWhiteSpace(trackName))
            {
                var track = new Track
                {
                    Name = trackName.Trim(),
                    MediaTypeId = 1,
                    Milliseconds = 180000,
                    UnitPrice = trackPrice > 0 ? trackPrice : 0.99m,
                    AlbumId = Album.AlbumId
                };

                _context.Tracks.Add(track);
            }
        }

        await _context.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}
