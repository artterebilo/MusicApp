using MusicApp.Contracts;
using MusicApp.Validations;
using Utils;

namespace MusicApp.Unit.Tests;
public class ArtistTests
{
    private ArtistCreateAndUpdateValidation validation;
    private AtristsPaginationValidation paginationValidation;

    [SetUp]
    public void Setup()
    {
        validation = new ArtistCreateAndUpdateValidation();
        paginationValidation = new AtristsPaginationValidation();
    }

    [Test]
    public void ArtistName_MustBeEmpty()
    {
        // Arrange
        var artist = new ArtistCreateAndUpdateContract
        {
            Name = "",
            Description = "Description",
            Genres = new List<string> { "Rock", "Pop" }
        };

        // Act
        var result = validation.Validate(artist);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.ArtistNameCheckIsNotEmpty.Replace("{PropertyName}", nameof(artist.Name)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void ArtistName_MustBeCorrect()
    {
        var artist = new ArtistCreateAndUpdateContract
        {
            Name = "Artem",
            Description = "Description",
            Genres = new List<string> { "Rock", "Pop" }
        };

        var result = validation.Validate(artist);

        Assert.IsTrue(result.IsValid);
    }

    [Test]
    public void ArtistName_MustBeInCorrectLength()
    {
        var artist = new ArtistCreateAndUpdateContract
        {
            Name = "Art",
            Description = "Description",
            Genres = new List<string> { "Rock", "Pop" }
        };

        var result = validation.Validate(artist);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.ArtistNameCheckLength.Replace("{PropertyName}", nameof(artist.Name)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void ArtistName_MustBeCorrectLength()
    {
        var artist = new ArtistCreateAndUpdateContract
        {
            Name = "Arte",
            Description = "Description",
            Genres = new List<string> { "Rock", "Pop" }
        };

        var result = validation.Validate(artist);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void ArtistDescription_MustBeEmpty()
    {
        // Arrange
        var artist = new ArtistCreateAndUpdateContract
        {
            Name = "Artem",
            Description = "",
            Genres = new List<string> { "Rock", "Pop" }
        };

        // Act
        var result = validation.Validate(artist);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.ArtistDescriptionCheckIsNotEmpty.Replace("{PropertyName}", nameof(artist.Description)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void ArtistDescription_MustBeCorrect()
    {
        var artist = new ArtistCreateAndUpdateContract
        {
            Name = "Artem",
            Description = "aaaaaaaaaa",
            Genres = new List<string> { "Rock", "Pop" }
        };

        var result = validation.Validate(artist);

        Assert.IsTrue(result.IsValid);
    }

    [Test]
    public void ArtistDescription_MustBeInCorrectLength()
    {
        var artist = new ArtistCreateAndUpdateContract
        {
            Name = "Artem",
            Description = "aaaaaaaaa",
            Genres = new List<string> { "Rock", "Pop" }
        };

        var result = validation.Validate(artist);   

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.ArtistDescriptionCheckLength.Replace("{PropertyName}", nameof(artist.Description)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void ArtistDescription_MustBeCorrectLength()
    {
        var artist = new ArtistCreateAndUpdateContract
        {
            Name = "Artem",
            Description = "aaaaaaaaaa",
            Genres = new List<string> { "Rock", "Pop" }
        };

        var result = validation.Validate(artist);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void ArtistGenres_MustBeEmpty()
    {
        var artist = new ArtistCreateAndUpdateContract
        {
            Name = "Artem",
            Description = "aaaaaaaaaa",
            Genres = new List<string>()
        };

        var result = validation.Validate(artist);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count == 1);
        Assert.That(actual: Resources.Validations.ArtistGenresCheckIsNotEmpty.Replace("{PropertyName}", nameof(artist.Genres)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void ArtistGenres_MustBeCorrect()
    {
        var artist = new ArtistCreateAndUpdateContract
        {
            Name = "Artem",
            Description = "aaaaaaaaaa",
            Genres = new List<string> { "Rock" }
        };

        var result = validation.Validate(artist);

        Assert.IsTrue(result.IsValid);
    }

    [Test]
    public void ArtistGenres_ElementMustBeLengthInCorrect()
    {
        var artist = new ArtistCreateAndUpdateContract
        {
            Name = "Artem",
            Description = "aaaaaaaaaa",
            Genres = new List<string> { "Ro" }
        };

        var result = validation.Validate(artist);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count == 1);
        Assert.That(actual: Resources.Validations.ArtistGenresCheckValidGenre.Replace("{PropertyName}", nameof(artist.Genres)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void ArtistGenres_ElementMustBeLengthCorrect()
    {
        var artist = new ArtistCreateAndUpdateContract
        {
            Name = "Artem",
            Description = "aaaaaaaaaa",
            Genres = new List<string> { "Pop" }
        };

        var result = validation.Validate(artist);

        Assert.IsTrue(result.IsValid);
    }

    [Test]
    public void ArtistPageNumber_MustLessThanOne()
    {
        var artistPagination = new DefaultPagination
        {
            PageNumber = 0,
            PageSize = 1
        };

        var result = paginationValidation.Validate(artistPagination);

        Assert.IsFalse(result.IsValid);
        Assert.That(actual: Resources.Validations.PageNumberCheckValidInput.Replace("{PropertyName}", "Page Number"), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void ArtistPageNumber_MustMoreThanZero()
    {
        var artistPagination = new DefaultPagination
        {
            PageNumber = 1,
            PageSize = 1
        };

        var result = paginationValidation.Validate(artistPagination);

        Assert.IsTrue(result.IsValid);
    }

    [Test]
    public void ArtistPageSize_MustLessThanOne()
    {
        var artistPagination = new DefaultPagination
        {
            PageNumber = 1,
            PageSize = 0
        };

        var result = paginationValidation.Validate(artistPagination);

        Assert.IsFalse(result.IsValid);
        Assert.That(actual: Resources.Validations.PageSizeCheckValidInput.Replace("{PropertyName}", "Page Size"), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void ArtistPageSize_MustMoreThanZero()
    {
        var artistPagination = new DefaultPagination
        {
            PageNumber = 1,
            PageSize = 1
        };

        var result = paginationValidation.Validate(artistPagination);

        Assert.IsTrue(result.IsValid);
    }
}
 