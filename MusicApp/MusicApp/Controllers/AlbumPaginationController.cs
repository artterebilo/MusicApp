using Microsoft.AspNetCore.Mvc;
using MusicApp.Contracts;
using MusicApp.Services;
using Utils;
using System;
using System.Numerics;
using DataBase.Models;
using MusicApp.Validations;

namespace MusicApp.Controllers;

[ApiController]
[Route("[controller]")]

public class AlbumsController : ControllerBase
{
    static AlbumPaginationContract MapToPaginationContract(AlbumContract albums)
    {
        return new AlbumPaginationContract
        {
            Name = albums.Name,
            ReleaseDate = albums.ReleaseDate,
            ArtistName = ArtistService.GetArtistById(albums.ArtistId).Name,
            Genres = albums.Genres
        };
    }

    [HttpGet]
    public ActionResult<AlbumsPaginationContract> GetAlbumsWithPagination([FromQuery] DefaultPagination inputParams)
    {
        var validator = new AlbumsPaginationValidation();
        var result = validator.Validate(inputParams);
        if (!result.IsValid)
        {
            var messages = result.Errors.Select((e) =>
            {
                return new Error
                {
                    Message = e.ErrorMessage,
                    Field = e.PropertyName
                };
            });

            var errors = new Errors
            {
                ErrorsMessages = messages.ToList(),
            };
            return BadRequest(errors);
        }

        var albums = AlbumService.GetAlbumForPagination(inputParams);
        var totalCount = AlbumService.GetAlbumsCount();

        var paginationAlbums = new AlbumsPaginationContract
        {
            TotalCount = totalCount,
            PageNumber = inputParams.PageNumber,
            PageSize = inputParams.PageSize,
            PageCount = (int)Math.Ceiling((double)totalCount / inputParams.PageSize),
            Albums = albums.Select(MapToPaginationContract).ToList()
        };       

        return paginationAlbums;
    }
}
