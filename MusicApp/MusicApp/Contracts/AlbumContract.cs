namespace MusicApp.Contracts;

public class AlbumContract
{
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime Release { get; set; }
    public List<string> Genres { get; set; }
    public string Type { get; set; }

    public string ArtistId { get; set; }
}

public class AlbumCreateContract
{
    public string Name { get; set; }
    public DateTime Release { get; set; }
    public List<string> Genres { get; set; }
    public string Type { get; set; }

    public string ArtistId { get; set; }
}

public class AlbumUpdateContract
{
    public string Name { get; set; }
    public DateTime Release { get; set; }
    public List<string> Genres { get; set; }
    public string Type { get; set; }

    public string ArtistId { get; set; }
}
