using DataBase.Models;
using DataBase.Repositories;
using MusicApp.Models;
using Utils;

namespace MusicApp.Services;

public class ArtistService
{
    private static ArtistContract MapToContract(ArtistModel artist)
    {
        return new ArtistContract
        {
            Id = artist.Id,
            Name = artist.Name,
            Description = artist.Description,
            Genres = artist.Genres.Split(',').ToList(),
            CreatedAt = artist.CreatedAt,
        };
    }

    private static ArtistModel MapToModel(ArtistContract artist)
    {
        return new ArtistModel
        {
            Id = artist.Id,
            Name = artist.Name,
            Description = artist.Description,
            Genres = String.Join(",", artist.Genres),
            CreatedAt = artist.CreatedAt,
        };
    }

    public static List<ArtistContract> GetAllArtists()
    {
        return ArtistRepository
            .GetAllArtists()
            .Select(MapToContract)
            .ToList();
    }

    public static ArtistContract CreateArtist(ArtistCreateContract artist)
    {
        var newArtist = new ArtistModel()
        {
            Id = GenerateId.Id(),
            Name = artist.Name,
            Description = artist.Description,
            Genres = String.Join(",", artist.Genres),
            CreatedAt = DateTime.Now
        };

        ArtistRepository.CreateArtist(newArtist);

        return MapToContract(newArtist);
    }

    public static ArtistContract GetArtistById(string id)
    {
        var artist = ArtistRepository.GetArtistById(id);

        return MapToContract(artist);
    }
    public static void UpdateArtist(string id, ArtistUpdateContract updateArtist)
    {
        var artist = ArtistService.GetArtistById(id);

        if (artist.Id == id)
        {
            artist.Name = updateArtist.Name;
            artist.Description = updateArtist.Description;
            artist.Genres = updateArtist.Genres;
        }

        ArtistRepository.UpdateArtist(MapToModel(artist));
    }

    public static void DeleteArtist(string id)
    {
        ArtistRepository.DeleteArtist(id);
    }
}
