using Enums.AlbumTypes;
using MusicApp.Contracts;
using MusicApp.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace MusicApp.Unit.Tests;

public class AlbumTests
{
    private AlbumCreateAndUpdateValidation validation;
    private AlbumsPaginationValidation paginationValidation;

    [SetUp]
    public void Setup()
    {
        validation = new AlbumCreateAndUpdateValidation();
        paginationValidation = new AlbumsPaginationValidation();
    }

    [Test]
    public void AlbumName_MustBeEmpty()
    {
        var album = new AlbumCreateAndUpdateContract
        {
            Name = "",
            ReleaseDate = DateTime.Now,
            Genres = ["Rock"],
            Type = AlbumTypes.EP
        };

        var result = validation.Validate(album);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.AlbumNameCheckIsNotEmpty.Replace("{PropertyName}", nameof(album.Name)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void AlbumName_MustBeCorrect()
    {
        var album = new AlbumCreateAndUpdateContract
        {
            Name = "Artem",
            ReleaseDate = DateTime.Now,
            Genres = ["Rock"],
            Type = AlbumTypes.EP
        };

        var result = validation.Validate(album);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void AlbumName_MustBeInCorrectLength()
    {
        var album = new AlbumCreateAndUpdateContract
        {
            Name = "Art",
            ReleaseDate = DateTime.Now,
            Genres = ["Rock"],
            Type = AlbumTypes.EP
        };

        var result = validation.Validate(album);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.AlbumNameCheckLength.Replace("{PropertyName}", nameof(album.Name)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void AlbumName_MustBeCorrectLength()
    {
        var album = new AlbumCreateAndUpdateContract
        {
            Name = "Arte",
            ReleaseDate = DateTime.Now,
            Genres = ["Rock"],
            Type = AlbumTypes.EP
        };

        var result = validation.Validate(album);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void AlbumReleaseDate_MustBeEmpty()
    {
        var album = new AlbumCreateAndUpdateContract
        {
            Name = "Arte",
            ReleaseDate = null,
            Genres = ["Rock"],
            Type = AlbumTypes.EP
        };

        var result = validation.Validate(album);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.AlbumReleaseCheckIsNotEmpty.Replace("{PropertyName}", "Release Date"), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void AlbumReleaseDate_MustBeCorrect()
    {
        var album = new AlbumCreateAndUpdateContract
        {
            Name = "Arte",
            ReleaseDate = DateTime.Now,
            Genres = ["Rock"],
            Type = AlbumTypes.EP
        };

        var result = validation.Validate(album);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void AlbumReleaseDate_MustBeMoreRealTime()
    {
        var album = new AlbumCreateAndUpdateContract
        {
            Name = "Arte",
            ReleaseDate = DateTime.Parse("2025-07-13 00:00:00.0000006"),
            Genres = ["Rock"],
            Type = AlbumTypes.EP
        };

        var result = validation.Validate(album);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.AlbumReleaseShouldNotBeFuture.Replace("{PropertyName}", "Release Date"), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void AlbumReleaseDate_MustBeLessDateTimeNow()
    {
        var album = new AlbumCreateAndUpdateContract
        {
            Name = "Arte",
            ReleaseDate = DateTime.Parse("2023-07-13 00:00:00.0000006"),
            Genres = ["Rock"],
            Type = AlbumTypes.EP
        };

        var result = validation.Validate(album);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void AlbumGenres_MustBeEmpty()
    {
        var album = new AlbumCreateAndUpdateContract
        {
            Name = "Arte",
            ReleaseDate = DateTime.Parse("2023-07-13 00:00:00.0000006"),
            Genres = [],
            Type = AlbumTypes.EP
        };

        var result = validation.Validate(album);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.AlbumGenresCheckIsNotEmpty.Replace("{PropertyName}", nameof(album.Genres)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void AlbumGenres_MustBeNotEmpty()
    {
        var album = new AlbumCreateAndUpdateContract
        {
            Name = "Arte",
            ReleaseDate = DateTime.Parse("2023-07-13 00:00:00.0000006"),
            Genres = ["Rock"],
            Type = AlbumTypes.EP
        };

        var result = validation.Validate(album);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void AlbumGenres_CheckInCorrectLength()
    {
        var album = new AlbumCreateAndUpdateContract
        {
            Name = "Arte",
            ReleaseDate = DateTime.Parse("2023-07-13 00:00:00.0000006"),
            Genres = ["Ro"],
            Type = AlbumTypes.EP
        };

        var result = validation.Validate(album);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.AlbumGenresCheckValidGenre.Replace("{PropertyName}", nameof(album.Genres)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void AlbumGenres_CheckCorrectLength()
    {
        var album = new AlbumCreateAndUpdateContract
        {
            Name = "Arte",
            ReleaseDate = DateTime.Parse("2023-07-13 00:00:00.0000006"),
            Genres = ["Pop"],
            Type = AlbumTypes.EP
        };

        var result = validation.Validate(album);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void AlbumType_MustBeUndefined()
    {
        var album = new AlbumCreateAndUpdateContract
        {
            Name = "Arte",
            ReleaseDate = DateTime.Parse("2023-07-13 00:00:00.0000006"),
            Genres = ["Pop"],
            Type = AlbumTypes.Undefined
        };

        var result = validation.Validate(album);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.AlbumTypeCheckIsNotEmpty.Replace("{PropertyName}", nameof(album.Type)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void AlbumType_MustBeNotUndefined()
    {
        var album = new AlbumCreateAndUpdateContract
        {
            Name = "Arte",
            ReleaseDate = DateTime.Parse("2023-07-13 00:00:00.0000006"),
            Genres = ["Pop"],
            Type = AlbumTypes.EP
        };

        var result = validation.Validate(album);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void AlbumPageNumber_MustBeLessThanOne()
    {
        var albumPagination = new AlbumPagination
        {
            PageNumber = 0,
            PageSize = 1,
            OrderBy = "desc",
            SortBy = "releasedate"
        };

        var result = paginationValidation.Validate(albumPagination);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.PageNumberCheckValidInput.Replace("{PropertyName}", "Page Number"), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void AlbumPageNumber_MustBeLessThanZero()
    {
        var albumPagination = new AlbumPagination
        {
            PageNumber = 1,
            PageSize = 1,
            OrderBy = "desc",
            SortBy = "releasedate"
        };

        var result = paginationValidation.Validate(albumPagination);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void AlbumPageSize_MustBeLessThanOne()
    {
        var albumPagination = new AlbumPagination
        {
            PageNumber = 1,
            PageSize = 0,
            OrderBy = "desc",
            SortBy = "releasedate"
        };

        var result = paginationValidation.Validate(albumPagination);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.PageSizeCheckValidInput.Replace("{PropertyName}", "Page Size"), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void AlbumPageSize_MustBeLessThanZero()
    {
        var albumPagination = new AlbumPagination
        {
            PageNumber = 1,
            PageSize = 1,
            OrderBy = "desc",
            SortBy = "releasedate"
        };

        var result = paginationValidation.Validate(albumPagination);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void AlbumOrderBy_MustBeNotEqualDescOrAsc()
    {
        var albumPagination = new AlbumPagination
        {
            PageNumber = 1,
            PageSize = 1,
            OrderBy = "desca",
            SortBy = "releasedate"
        };

        var result = paginationValidation.Validate(albumPagination);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(Resources.Validations.OrderByCheckValues.Replace("{PropertyName}", nameof(albumPagination.OrderBy)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void AlbumOrderBy_MustBeEqualDescOrAsc()
    {
        var albumPagination = new AlbumPagination
        {
            PageNumber = 1,
            PageSize = 1,
            OrderBy = "desc",
            SortBy = "releasedate"
        };

        var result = paginationValidation.Validate(albumPagination);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void AlbumSortBy_MustBeEmpty()
    {
        var albumPagination = new AlbumPagination
        {
            PageNumber = 1,
            PageSize = 1,
            OrderBy = "desc",
            SortBy = ""
        };

        var result = paginationValidation.Validate(albumPagination);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(Resources.Validations.AlbumSortByCheckIsNotEmpty.Replace("{PropertyName}", "Sort By"), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void AlbumSortBy_MustBeCorrect()
    {
        var albumPagination = new AlbumPagination
        {
            PageNumber = 1,
            PageSize = 1,
            OrderBy = "desc",
            SortBy = "releasedate"
        };

        var result = paginationValidation.Validate(albumPagination);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void AlbumSortBy_MustBeNotEqualPropertyAlbum()
    {
        var albumPagination = new AlbumPagination
        {
            PageNumber = 1,
            PageSize = 1,
            OrderBy = "desc",
            SortBy = "releasedates"
        };

        var result = paginationValidation.Validate(albumPagination);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(Resources.Validations.SortByCheckCorrectValues.Replace("{0}", "id, name, releasedate, genres, type, artistid, artist, songs"), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void AlbumSortBy_MustBeEqualPropertyAlbum()
    {
        var albumPagination = new AlbumPagination
        {
            PageNumber = 1,
            PageSize = 1,
            OrderBy = "desc",
            SortBy = "releasedate"
        };

        var result = paginationValidation.Validate(albumPagination);

        Assert.IsFalse(!result.IsValid);

    }
}