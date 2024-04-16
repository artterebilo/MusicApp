using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;

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

public class ArtistCreateAndUpdateContract 
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> Genres { get; set; }
    public string ArtistId { get; set; }
}

public class ArtistPaginationContract
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> Genres { get; set; }
}
public class ArtistsPaginationContract
{
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int PageCount { get; set; }

    public List<ArtistPaginationContract> Artists { get; set; }
}
