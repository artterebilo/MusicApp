using DataBase.Models;
using Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace DataBase.Repositories;

public static class UserLikeSongRepository
{
    public static UserLikeSongsModel GetLikeByUserIdAndSongId(UserLikeSongsModel user)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            return db.UsersLikesSongs.FirstOrDefault(l => l.UserId == user.UserId && l.SongId == user.SongId);
        }
    }
    public static void CreateUserLikeOrDislike(UserLikeSongsModel userLike)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            var UserSets = new UserLikeSongsModel
            {
                UserId = userLike.UserId,
                SongId = userLike.SongId,
                LikeStatus = userLike.LikeStatus,
                LikedAt = DateTime.Now,
            };
            db.UsersLikesSongs.Add(UserSets);
            db.SaveChanges();
        }
    }

    public static void UpdateUserLikeStatus(UserLikeSongsModel userLike)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            db.UsersLikesSongs
                .Where(e => e.UserId == userLike.UserId && e.SongId == userLike.SongId)
                .ExecuteUpdate(u => u
                .SetProperty(u => u.LikeStatus, userLike.LikeStatus));
            db.SaveChanges();
        }
    }

    public static void DeleteUserLike(UserLikeSongsModel userLike)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            var existingLike = db.UsersLikesSongs.FirstOrDefault(u => u.UserId == userLike.UserId && u.SongId == userLike.SongId);
            db.UsersLikesSongs.Remove(existingLike);
            db.SaveChanges();
        }
    }

    public static List<UserLikeSongsModel> GetAllSongsLikedUser(LikePagination inputParams, string userId)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            return db.UsersLikesSongs
                .Include(u => u.Song.Album.Artist)
                .Where(u => u.UserId == userId && u.LikeStatus == UserLikeSongsTypes.Like)
                .OrderBy(u => inputParams.OrderBy)
                .Skip((inputParams.PageNumber -1) * inputParams.PageSize)
                .Take(inputParams.PageSize)
                .ToList();               
        }
    }
}
