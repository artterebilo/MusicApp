using DataBase.Models;
using DataBase.Repositories;
using MusicApp.Contracts;
using Utils;

namespace MusicApp.Services;

public class SongService
{
    private static SongContract MapToContract(SongModel song)
    {
        if (song == null)
        {
            return null;
        }

        return new SongContract
        {
            Id = song.Id,
            Name = song.Name,
            Number = song.Number,
            DurationInSeconds = song.DurationInSeconds,
            FeaturingArtistIds = song.FeaturingArtistIds.Split(',').ToList(),
            AlbumId = song.AlbumId
        };
    }
    private static SongModel MapToModel(SongContract song)
    {
        if (song == null)
        {
            return null;
        }

        return new SongModel
        {
            Id = song.Id,
            Name = song.Name,
            Number = song.Number,
            DurationInSeconds = song.DurationInSeconds,
            FeaturingArtistIds = String.Join(",", song.FeaturingArtistIds),
            AlbumId = song.AlbumId
        };
    }
    public static List<SongContract> GetAllSongsForAlbum(string albumId)
    {
        return SongRepository
            .GetAllSongsForAlbum(albumId)
            .Select(MapToContract)
            .ToList();
    }
    public static SongContract GetSongById(string id)
    {
        return MapToContract(SongRepository.GetSongById(id));
    }
    public static List<SongContract> CreateSongs(string albumId, List<SongCreateContract> songs)
    {
        var songsToCreate = songs
            .Select(song => new SongModel
            {
                Id = GenerateId.Id(),
                Name = song.Name,
                Number = song.Number,
                DurationInSeconds = song.DurationInSeconds,
                FeaturingArtistIds = String.Join(",", song.FeaturingArtistIds),
                AlbumId = albumId
            })
            .ToList();

        SongRepository.CreateSong(songsToCreate);

        return songsToCreate
            .Select(MapToContract)
            .ToList();
    }
    public static void UpdateSongs(List<SongUpdateContract> songs)
    {
        var updateSongs = songs
            .Select(song => new SongModel
            {
                Name = song.Name,
                Number = song.Number,
                DurationInSeconds = song.DurationInSeconds,
                FeaturingArtistIds = String.Join(',', song.FeaturingArtistIds)
            })
            .ToList();

        SongRepository.UpdateSongs(updateSongs);
    }
    public static void UpdateSong(string id, SongUpdateContract song)
    {
        var updateSong = new SongModel
        {
            Name = song.Name,
            Number = song.Number,
            DurationInSeconds = song.DurationInSeconds,
            FeaturingArtistIds = String.Join(',', song.FeaturingArtistIds),
        };

        SongRepository.UpdateSong(id, updateSong);
    }
    public static void DeleteSong(string id)
    {
        SongRepository.DeleteSong(id);
    }
}
