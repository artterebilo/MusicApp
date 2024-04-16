using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models;

public class SongModel
{
    [Required]
    public string Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public int Number { get; set; }

    [Required]
    public int DurationInSeconds { get; set; }

    [Required]
    public string FeaturingArtistIds { get; set; }

    [Required]
    public string AlbumId { get; set; }

    public AlbumModel Album { get; set; }
}


