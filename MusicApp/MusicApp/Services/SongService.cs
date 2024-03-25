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
    private static SongModel MapFromSongUpdateContractToSongModel(string id, SongCreateAndUpdateContract song)
    {
        if (song is null)
        {
            return null;
        }

        return new SongModel
        {
            Id = id,
            Name = song.Name,
            Number = song.Number,
            DurationInSeconds = song.DurationInSeconds,
            FeaturingArtistIds = String.Join(",", song.FeaturingArtistIds)
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
    public static List<SongContract> CreateSongs(string albumId, List<SongCreateAndUpdateContract> songs)
    {
        var songsToCreate = songs
            .Select(song => new SongModel
            {
                Id = IdGenerator.Id(),
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
    public static void UpdateSongs(List<SongCreateAndUpdateContract> songs)
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
    public static void UpdateSong(string id, SongCreateAndUpdateContract song)
    {
        SongRepository.UpdateSong(id, MapFromSongUpdateContractToSongModel(id, song));
    }
    public static void DeleteSong(string id)
    {
        SongRepository.DeleteSong(id);
    }
}
