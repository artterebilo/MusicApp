using Enums.AlbumTypes;
using System.ComponentModel.DataAnnotations;

namespace MusicApp.Contracts;

public class AlbumContract
{
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public List<string> Genres { get; set; }
    public AlbumTypes Type { get; set; }
    public string ArtistId { get; set; }
}

public class AlbumCreateAndUpdateContract
{
    public string Name { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public List<string> Genres { get; set; }
    public AlbumTypes Type { get; set; }
}

public class AlbumPaginationContract
{
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string ArtistName { get; set; }
    public List<string> Genres { get; set; }
}
public class AlbumsPaginationContract
{
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int PageCount { get; set; }
    public List<AlbumPaginationContract> Albums { get; set; }    
}

