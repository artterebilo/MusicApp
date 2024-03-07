using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repositories;

public static class ArtistRepository
{
    public static List<ArtistModel> GetAllArtists()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            return db.Artists.ToList();
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
            var updateArtist = db.Artists.FirstOrDefault(art => art.Id == artist.Id);
                        
                updateArtist.Name = artist.Name;
                updateArtist.Description = artist.Description;
                updateArtist.Genres = artist.Genres;

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
