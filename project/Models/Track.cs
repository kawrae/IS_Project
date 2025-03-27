using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Models;

[Table("tracks")]
public class Track
{
    [Key]
    public int TrackId { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public int AlbumId { get; set; }

    [ForeignKey("AlbumId")]
    public Album? Album { get; set; }

    public int MediaTypeId { get; set; }

    public int? Milliseconds { get; set; }
    public int? Bytes { get; set; }
    public decimal? UnitPrice { get; set; }
}
