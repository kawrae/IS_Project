using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project.Models;

namespace project.Pages.Albums;

public class EditModel : PageModel
{
    private readonly ChinookDbContext _context;

    public EditModel(ChinookDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Album Album { get; set; } = new();

    [BindProperty]
    public List<Track> Tracks { get; set; } = new();

    public SelectList ArtistList { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Album = await _context.Albums.Include(a => a.Artist).Include(a => a.Tracks).FirstOrDefaultAsync(a => a.AlbumId == id);
        if (Album == null) return NotFound();

        Tracks = Album.Tracks.ToList();
        ArtistList = new SelectList(await _context.Artists.OrderBy(a => a.Name).ToListAsync(), "ArtistId", "Name", Album.ArtistId);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            ArtistList = new SelectList(await _context.Artists.OrderBy(a => a.Name).ToListAsync(), "ArtistId", "Name", Album.ArtistId);
            return Page();
        }

        var existingAlbum = await _context.Albums
            .Include(a => a.Tracks)
            .FirstOrDefaultAsync(a => a.AlbumId == Album.AlbumId);

        if (existingAlbum == null)
            return NotFound();

        existingAlbum.Title = Album.Title.Trim();
        existingAlbum.ArtistId = Album.ArtistId;

        var formTracks = Request.Form.Keys
            .Where(k => k.StartsWith("Tracks[") && k.EndsWith("].TrackId"))
            .Select(k => int.Parse(k.Split('[', ']')[1]))
            .Distinct();

        foreach (var index in formTracks)
        {
            var idVal = Request.Form[$"Tracks[{index}].TrackId"];
            var nameVal = Request.Form[$"Tracks[{index}].Name"];

            if (string.IsNullOrWhiteSpace(nameVal)) continue;

            if (idVal == "0")
            {
                _context.Tracks.Add(new Track
                {
                    Name = nameVal.ToString().Trim(),
                    AlbumId = existingAlbum.AlbumId,
                    MediaTypeId = 1,
                    Milliseconds = 180000,
                    UnitPrice = 0.99m
                });
            }
            else
            {
                var trackId = int.Parse(idVal);
                var existingTrack = existingAlbum.Tracks.FirstOrDefault(t => t.TrackId == trackId);
                if (existingTrack != null)
                {
                    existingTrack.Name = nameVal.ToString().Trim();
                }
            }
        }

        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }

    public async Task<IActionResult> OnPostDeleteTrackAsync(int id)
    {
        var track = await _context.Tracks.AsNoTracking().FirstOrDefaultAsync(t => t.TrackId == id);
        if (track != null)
        {
            int albumId = track.AlbumId;
            _context.Tracks.Remove(track);
            await _context.SaveChangesAsync();
            return RedirectToPage(new { id = albumId });
        }

        return NotFound();
    }
}
