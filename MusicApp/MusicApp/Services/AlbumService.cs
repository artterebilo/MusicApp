using DataBase.Models;
using DataBase.Repositories;
using MusicApp.Contracts;
using Utils;

namespace MusicApp.Services;

public static class AlbumService
{
    private static AlbumContract MapToContract(AlbumModel Album)
    {
        if (Album == null)
        {
            return null;
        }

        return new AlbumContract
        {
            Id = Album.Id,
            Name = Album.Name,
            ReleaseDate = Album.ReleaseDate,
            Genres = Album.Genres.Split(',').ToList(),
            Type = Album.Type,
            ArtistId = Album.ArtistId
        };
    }

    private static AlbumModel MapToModel(AlbumContract Album)
    {
        if (Album == null)
        {
            return null;
        }

        return new AlbumModel
        {
            Id = Album.Id,
            Name = Album.Name,
            ReleaseDate = Album.ReleaseDate,
            Genres = String.Join(",", Album.Genres),
            Type = Album.Type,
            ArtistId = Album.ArtistId
        };
    }

    public static int GetAlbumsCount()
    {
        return AlbumRepository.GetAlbumsCount();
    }
    public static List<AlbumContract> GetAlbumForPagination(DefaultPagination @params)
    {
        return AlbumRepository
            .GetAlbumsForPagination(@params)
            .Select(MapToContract)
            .ToList();
    }
    public static List<AlbumContract> GetAllAlbumsByArtist(string id)
    {
        return AlbumRepository
            .GetAllAlbums(id)
            .Select(MapToContract)
            .ToList();
    }

    public static AlbumContract CreateAlbum(string artistId, AlbumCreateAndUpdateContract artist)
    {
        var newAlbum = new AlbumModel()
        {
            Id = IdGenerator.Id(),
            Name = artist.Name,
            ReleaseDate = artist.ReleaseDate.Value,
            Genres = String.Join(",", artist.Genres),
            Type = artist.Type,
            ArtistId = artistId
        };

        AlbumRepository.CreateAlbum(newAlbum);        

        return MapToContract(newAlbum);
    }

    public static AlbumContract GetAlbumById(string id)
    {
        return MapToContract(AlbumRepository.GetAlbumById(id));
    }

    public static void UpdateAlbum(string id, AlbumCreateAndUpdateContract updateAlbum)
    {
        var album = AlbumService.GetAlbumById(id);

        album.Name = updateAlbum.Name;
        album.ReleaseDate = updateAlbum.ReleaseDate.Value;
        album.Genres = updateAlbum.Genres;
        album.Type = updateAlbum.Type;

        AlbumRepository.UpdateAlbum(MapToModel(album));
    }

    public static void DeleteAlbum(string id)
    {
        AlbumRepository.DeleteAlbum(id);
    }
}
