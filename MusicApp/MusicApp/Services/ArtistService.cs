using DataBase.Models;
using DataBase.Repositories;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Contracts;
using Utils;

namespace MusicApp.Services;

public class ArtistService
{
    private static ArtistContract MapToContract(ArtistModel artist)
    {
        if (artist == null)
        {
            return null;
        }

        return new ArtistContract
        {
            Id = artist.Id,
            Name = artist.Name,
            Description = artist.Description,
            Genres = artist.Genres.Split(',').ToList(),
            CreatedAt = artist.CreatedAt,
            ArtistId = artist.Id,
        };
    }
    private static ArtistModel MapToModel(ArtistContract artist)
    {
        if (artist is null)
        {
            return null;
        }

        return new ArtistModel
        {
            Id = artist.Id,
            Name = artist.Name,
            Description = artist.Description,
            Genres = String.Join(",", artist.Genres),
            CreatedAt = artist.CreatedAt,
        };
    }
    private static ArtistModel MapFromArtistUpdateContractToArtistModel(string id, ArtistCreateAndUpdateContract artist)
    {
        if (artist is null)
        {
            return null;
        }

        return new ArtistModel
        {
            Id = id,
            Name = artist.Name,
            Description = artist.Description,
            Genres = String.Join(",", artist.Genres),
            
        };
    }

    public static List<ArtistContract> GetAllArtists(PaginationParams inputParams)
    {
        return ArtistRepository
            .GetAllArtists(inputParams)
            .Select(MapToContract)
            .ToList();
    }
    public static ArtistContract GetArtistById(string id)
    {
        return MapToContract(ArtistRepository.GetArtistById(id));
    }
    public static ArtistContract CreateArtist(ArtistCreateAndUpdateContract artist)
    {
        var newArtist = new ArtistModel()
        {
            Id = IdGenerator.Id(),
            Name = artist.Name,
            Description = artist.Description,
            Genres = String.Join(",", artist.Genres),
            CreatedAt = DateTime.Now
        };

        ArtistRepository.CreateArtist(newArtist);

        return MapToContract(newArtist);
    }
    public static void UpdateArtist(string id, ArtistCreateAndUpdateContract updateArtist)
    {
        ArtistRepository.UpdateArtist(MapFromArtistUpdateContractToArtistModel(id, updateArtist));
    }
    public static void DeleteArtist(string id)
    {
        ArtistRepository.DeleteArtist(id);
    }
}
 