
using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repositories;

public static class SongRepository
{
    public static List<SongModel> GetAllSongsForAlbum(string albumId)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            return db.Songs
                .Where(x => x.AlbumId == albumId)
                .OrderBy(x => x.Number)
                .ToList();
        }
    }

    public static SongModel GetSongById(string id)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            return db.Songs.FirstOrDefault(s => s.Id == id);
        }
    }

    public static void CreateSong(List<SongModel> songs)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            db.Songs.AddRange(songs);
            db.SaveChanges();
        }
    }

    public static void UpdateSongs(List<SongModel> songs)
    {
        using (ApplicationContext db = new ApplicationContext ())
        {
            foreach (var song in songs)
            {
                db.Songs
                    .Where(s => s.Number == song.Number)
                    .ExecuteUpdate(s => s
                        .SetProperty(s => s.Name, song.Name)             
                        .SetProperty(s => s.DurationInSeconds, song.DurationInSeconds)
                        .SetProperty(s => s.FeaturingArtistIds, song.FeaturingArtistIds));
                db.SaveChanges();
            }
            
        }
    }

    public static void UpdateSong(string id, SongModel song)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            db.Songs
                .Where(s => s.Id == id)
                .ExecuteUpdate(s => s
                    .SetProperty(s => s.Name, song.Name)
                    .SetProperty(s => s.Number, song.Number)
                    .SetProperty(s => s.DurationInSeconds, song.DurationInSeconds)
                    .SetProperty(s => s.FeaturingArtistIds, song.FeaturingArtistIds));
            db.SaveChanges();
        }
    }

    public static void DeleteSong(string id)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            var song = db.Songs.FirstOrDefault(s => s.Id == id);

            db.Songs.Remove(song);

            db.SaveChanges();
        }
    }
}
