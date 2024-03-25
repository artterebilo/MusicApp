using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models;

public class ArtistModel
{
    [Required]
    public string Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    public string Genres { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    public List<AlbumModel> Albums { get; set; } = new();
}
