using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repositories;

public static class AlbumRepository
{
    public static List<AlbumModel> GetAllAlbums(string id)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            return db.Albums
                .Where(x => x.ArtistId == id)
                .ToList();
        }
    }

    public static void CreateAlbum(AlbumModel album)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            AlbumModel newAlbum = new AlbumModel
            {
                Id = album.Id,
                Name = album.Name,
                Release = album.Release,
                Genres = album.Genres,
                Type = album.Type,
                ArtistId = album.ArtistId
            };

            db.Albums.Add(newAlbum);
            db.SaveChanges();
        }
    }

    public static AlbumModel GetAlbumById(string id)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            return db.Albums.FirstOrDefault(a => a.Id == id);
        }
    }

    public static void UpdateAlbum(AlbumModel album)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            var updateAlbum = db.Albums.FirstOrDefault(alb => alb.Id == album.Id);

            updateAlbum.Name = album.Name;
            updateAlbum.Release = album.Release;
            updateAlbum.Genres = album.Genres;
            updateAlbum.Type = album.Type;
            updateAlbum.ArtistId = album.ArtistId;

            db.SaveChanges();
        }
    }

    public static void DeleteAlbum(string id)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            var album = db.Albums.FirstOrDefault(a => a.Id == id);

            db.Albums.Remove(album);
            db.SaveChanges();
        }
    }
}
