using Microsoft.AspNetCore.Mvc;
using MusicApp.Contracts;
using MusicApp.Services;
using Utils;

namespace MusicApp.Controllers;

[ApiController]
[Route("[controller]")]

public class AlbumsController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<AlbumContract>> GetAlbumsWithPagination([FromQuery] PaginationAlbum @params)
    {
        return AlbumService.GetAlbumForPagination(@params);
    }
}
