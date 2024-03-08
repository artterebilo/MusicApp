namespace MusicApp.Contracts;

public class ArtistContract
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> Genres { get; set; }
    public DateTime CreatedAt { get; set; }
    public string ArtistId { get; set; }
}

public class ArtistCreateContract
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> Genres { get; set; }
    public string ArtistId { get; set; }
}

public class ArtistUpdateContract
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> Genres { get; set; }
    public string ArtistId { get; set; }
}
