using Enums;

namespace MusicApp.Contracts;

public class UserLikeSongContract
{
    public string UserId { get; set; }
    public string SongId { get; set; }
    public UserLikeSongsTypes LikeStatus { get; set; }
}

public class AllSongsLikedUserContract
{
    public string UserId { get; set; }
    public int PageNumber { get; set; }
    public int PagesCount { get; set; }
    public int PageSize { get; set; }
    public List<ArtistAlbumSingInformationContract> Items { get; set; }
}
public class ArtistAlbumSingInformationContract
{
    public string SondId { get; set; }
    public string SongName { get; set; }
    public int SongDuration { get; set; }
    public string ArtistId { get; set; }
    public string ArtistName { get; set; }
    public string AlbumId { get; set; }
    public string AlbumName { get; set; }
    public DateTime ReleaseDate {  get; set; }
    public DateTime LikedAt {  get; set; }
}
