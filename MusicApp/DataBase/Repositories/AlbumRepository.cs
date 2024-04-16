using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace DataBase.Repositories;

public static class AlbumRepository
{
    public static int GetAlbumsCount()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            return db.Albums
                .Count();
        }
    }
    public static List<AlbumModel> GetAlbumsForPagination(AlbumPagination @params)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            return db.Albums
                .Where(x => x.Genres.Contains(@params.Genre)) // || @params.Genre == null)
                .OrderBy(x => @params.SortBy)
                .ThenBy(x => @params.OrderBy)
                .Skip((@params.PageNumber - 1) * @params.PageSize)
                .Take(@params.PageSize)
                .ToList();
        }
    }
    public static List<AlbumModel> GetAllAlbums(string artistId)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            return db.Albums
                .Where(x => x.ArtistId == artistId)
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
                ReleaseDate = album.ReleaseDate,
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
            db.Albums
                .Where(a => a.Id == album.Id)
                .ExecuteUpdate(a => a
                    .SetProperty(a => a.Name, album.Name)
                    .SetProperty(a => a.ReleaseDate, album.ReleaseDate)
                    .SetProperty(a => a.Genres, album.Genres)
                    .SetProperty(a => a.Type, album.Type)
                    .SetProperty(a => a.ArtistId, album.ArtistId));
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
