using DataBase.Models;
using DataBase.Repositories;
using Enums;
using MusicApp.Contracts;
using Utils;

namespace MusicApp.Services;

public class UserLikeSongService
{
    public static UserLikeSongsModel MapFromUserLikeSongContractToUserLikeSongModel(UserLikeSongContract user)
    {
        if (user is null)
        {
            return null;
        }

        return new UserLikeSongsModel
        {
            UserId = user.UserId,
            SongId = user.SongId,
            LikeStatus = user.LikeStatus
        };
    } 


    public static void UpdateCreateDeleteLike(UserLikeSongContract user)
    {
        var mapUser = MapFromUserLikeSongContractToUserLikeSongModel(user);
        var likeUser = UserLikeSongRepository.GetLikeByUserIdAndSongId(mapUser);

        if (likeUser is null && user.LikeStatus != UserLikeSongsTypes.NotSelected)
        {
            UserLikeSongRepository.CreateUserLikeOrDislike(mapUser);
            return;
        }

        if (likeUser.LikeStatus != UserLikeSongsTypes.NotSelected && user.LikeStatus == UserLikeSongsTypes.NotSelected)
        {
            UserLikeSongRepository.DeleteUserLike(mapUser);
            return;
        }
        UserLikeSongRepository.UpdateUserLikeStatus(mapUser);
    }

    public static AllLikeSongsUserContract GetAllSongsLikedUser(LikePagination inputParams, string id)
    {
        var songs = UserLikeSongRepository.GetAllSongsLikedUser(inputParams, id);

        var items = new List<ArtistAlbumSingInformationContract>();

        foreach( var song in songs)
        {
            var item = new ArtistAlbumSingInformationContract
            {
                SondId = song.SongId,
                SongName = song.Song.Name,
                SongDuration = song.Song.DurationInSeconds,
                ArtistId = song.Song.Album.ArtistId,
                ArtistName = song.Song.Album.Artist.Name,
                AlbumId = song.Song.AlbumId,
                AlbumName = song.Song.Album.Name,
                ReleaseDate = song.Song.Album.ReleaseDate,
                LikedAt = song.LikedAt
            };

            items.Add(item);
        }

        var getSongs = new AllLikeSongsUserContract
        {
            UserId = id,
            PageNumber = inputParams.PageNumber,
            PagesCount = (int)Math.Ceiling((double)songs.Count() / inputParams.PageSize),
            PageSize = inputParams.PageSize,
            Items = items
        };

        return getSongs;
    }

    public static bool checkIfThereIsUser(string id)
    {
        var user = UserService.GetUserById(id);

        if (user is null)
        {
            return false;
        }

        return true;
    }
    public static bool checkIfThereIsSong(string id)
    {
        var user = SongService.GetSongById(id);

        if (user is null)
        {
            return false;
        }

        return true;
    }
}

