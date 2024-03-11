using Enums.AlbomTypes;
using System.ComponentModel.DataAnnotations;

namespace MusicApp.Contracts;

public class AlbumContract
{
    public string Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; }
    public DateTime Release { get; set; }

    [StringLength(100)]
    public List<string> Genres { get; set; }

    public AlbumTypes Type { get; set; }
    public string ArtistId { get; set; }
}

public class AlbumCreateContract
{
    public string Name { get; set; }
    public DateTime Release { get; set; }
    public List<string> Genres { get; set; }
    public AlbumTypes Type { get; set; }

}

public class AlbumUpdateContract
{
    public string Name { get; set; }
    public DateTime Release { get; set; }
    public List<string> Genres { get; set; }
    public AlbumTypes Type { get; set; }

}
