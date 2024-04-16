using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace DataBase.Repositories;

public static class ArtistRepository
{
    public static List<ArtistModel> GetAllArtists(PaginationParams inputParams)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            return db.Artists
                .Skip((inputParams.PageNumber - 1) * inputParams.PageSize)
                .Take(inputParams.PageSize)
                .ToList();
        }
    }

    public static ArtistModel GetArtistById(string id)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            return db.Artists.FirstOrDefault(art => art.Id == id);
        }
    }

    public static void CreateArtist(ArtistModel artist)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            ArtistModel newArtist = new ArtistModel
            {
                Id = artist.Id,
                Name = artist.Name,
                Description = artist.Description,
                Genres = artist.Genres,
                CreatedAt = artist.CreatedAt
            };

            db.Artists.Add(newArtist);
            db.SaveChanges();
        }
    }

    public static void UpdateArtist(ArtistModel artist)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            db.Artists
                .Where(a => a.Id == artist.Id)
                .ExecuteUpdate(a => a
                    .SetProperty(a => a.Name, artist.Name)
                    .SetProperty(a => a.Description, artist.Description)
                    .SetProperty(a => a.Genres, artist.Genres));
            db.SaveChanges();
        }
    }

    public static void DeleteArtist(string id)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            var artist = db.Artists.FirstOrDefault(art => art.Id == id);

            db.Artists.Remove(artist);
            db.SaveChanges();
        }
    }
}
