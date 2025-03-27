using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Models
{
    [Table("albums")]
    public class Album
    {
        [Key]
        public int AlbumId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public int ArtistId { get; set; }

        [ForeignKey("ArtistId")]
        public Artist? Artist { get; set; }

        public ICollection<Track> Tracks { get; set; } = new List<Track>();
    }

    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }

        public string Name { get; set; }

        public ICollection<Album>? Albums { get; set; }
    }
}
