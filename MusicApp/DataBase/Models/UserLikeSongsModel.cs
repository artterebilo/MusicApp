using Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models;

public class UserLikeSongsModel
{
    [Required]
    public string UserId { get; set; }
    public UserModel User { get; set; }

    [Required]
    public string SongId { get; set; }
    public SongModel Song { get; set; }

    [Required] 
    public UserLikeSongsTypes LikeStatus { get; set; }

    [Required]
    public DateTime LikedAt { get; set; }
}


