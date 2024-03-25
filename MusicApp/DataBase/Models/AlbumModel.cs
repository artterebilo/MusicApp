using Enums.AlbomTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models;

public class AlbumModel
{
    [Required]
    public string Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public DateTime Release { get; set; }
    public string Genres { get; set; }
    public AlbumTypes Type { get; set; }

    [Required]
    public string ArtistId { get; set; }
    public ArtistModel Artist { get; set; }

    public List<SongModel> Songs { get; set; } = new();
}