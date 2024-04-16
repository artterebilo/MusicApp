using MusicApp.Contracts;
using MusicApp.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Unit.Tests;


public class SongsTests
{
    private SongUpdateValidation validation;
    private SongsCreateAndUpdateValidation songValidation;

    [SetUp]
    public void Setup()
    {
        validation = new SongUpdateValidation();
        songValidation = new SongsCreateAndUpdateValidation();
    }

    [Test]
    public void SongName_MustBeEmpty()
    {
        var song = new SongCreateAndUpdateContract
        {
            Name = "",
            Number = 1,
            DurationInSeconds = 1,
            FeaturingArtistIds = []
        };

        var result = validation.Validate(song);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.SongNameCheckIsNotEmpty.Replace("{PropertyName}", nameof(song.Name)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void SongName_MustBeCorrect()
    {
        var song = new SongCreateAndUpdateContract
        {
            Name = "Artem",
            Number = 1,
            DurationInSeconds = 1,
            FeaturingArtistIds = []
        };

        var result = validation.Validate(song);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void SongName_MustBeInCorrectLength()
    {
        var song = new SongCreateAndUpdateContract
        {
            Name = "art",
            Number = 1,
            DurationInSeconds = 1,
            FeaturingArtistIds = []
        };

        var result = validation.Validate(song);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.SongNameCheckCorrectLength.Replace("{PropertyName}", nameof(song.Name)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void SongName_MustBeCorrectLength()
    {
        var song = new SongCreateAndUpdateContract
        {
            Name = "arte",
            Number = 1,
            DurationInSeconds = 1,
            FeaturingArtistIds = []
        };

        var result = validation.Validate(song);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void SongNumber_MustBeLessOne()
    {
        var song = new SongCreateAndUpdateContract
        {
            Name = "Artem",
            Number = 0,
            DurationInSeconds = 1,
            FeaturingArtistIds = []
        };

        var result = validation.Validate(song);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.SongNumberPositiveCheck.Replace("{PropertyName}", nameof(song.Number)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void SongNumber_MustBeMoreThanZero()
    {
        var song = new SongCreateAndUpdateContract
        {
            Name = "Artem",
            Number = 1,
            DurationInSeconds = 1,
            FeaturingArtistIds = []
        };

        var result = validation.Validate(song);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void SongDurationInSeconds_MustBeLessOne()
    {
        var song = new SongCreateAndUpdateContract
        {
            Name = "Artem",
            Number = 1,
            DurationInSeconds = 0,
            FeaturingArtistIds = []
        };

        var result = validation.Validate(song);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.SongDurationInSecondsPositiveCheck.Replace("{PropertyName}", "Duration In Seconds"), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void SongDurationInSeconds_MustBeMoreThanZero()
    {
        var song = new SongCreateAndUpdateContract
        {
            Name = "Artem",
            Number = 1,
            DurationInSeconds = 1,
            FeaturingArtistIds = []
        };

        var result = validation.Validate(song);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void SongsName_MustBeEmpty()
    {
        var songs = new List<SongCreateAndUpdateContract>
        {
            new SongCreateAndUpdateContract
            {
                Name = "",
                Number = 1,
                DurationInSeconds = 1,
                FeaturingArtistIds = []
            }
        };

        var result = songValidation.Validate(songs);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.SongNameCheckIsNotEmpty.Replace("{PropertyName}", "Name"), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void SongsName_MustBeCorrect()
    {
        var songs = new List<SongCreateAndUpdateContract>
        {
            new SongCreateAndUpdateContract
            {
                Name = "artem",
                Number = 1,
                DurationInSeconds = 1,
                FeaturingArtistIds = []
            }
        };

        var result = songValidation.Validate(songs);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void SongsName_MustBeInCorrectLength()
    {
        var songs = new List<SongCreateAndUpdateContract>
        {
            new SongCreateAndUpdateContract
            {
                Name = "art",
                Number = 1,
                DurationInSeconds = 1,
                FeaturingArtistIds = []
            }
        };

        var result = songValidation.Validate(songs);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.SongNameCheckCorrectLength.Replace("{PropertyName}", "Name"), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void SongsName_MustBeCorrectLength()
    {
        var songs = new List<SongCreateAndUpdateContract>
        {
            new SongCreateAndUpdateContract
            {
                Name = "arte",
                Number = 1,
                DurationInSeconds = 1,
                FeaturingArtistIds = []
            }
        };

        var result = songValidation.Validate(songs);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void SongsNumber_MustBeLessThanOne()
    {
        var songs = new List<SongCreateAndUpdateContract>
        {
            new SongCreateAndUpdateContract
            {
                Name = "arte",
                Number = 0,
                DurationInSeconds = 1,
                FeaturingArtistIds = []
            }
        };

        var result = songValidation.Validate(songs);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.SongNumberPositiveCheck.Replace("{PropertyName}", "Number"), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void SongsNumber_MustBeMoreThanZero()
    {
        var songs = new List<SongCreateAndUpdateContract>
        {
            new SongCreateAndUpdateContract
            {
                Name = "arte",
                Number = 1,
                DurationInSeconds = 1,
                FeaturingArtistIds = []
            }
        };

        var result = songValidation.Validate(songs);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void SongsDurationInSeconds_MustBeLessThanOne()
    {
        var songs = new List<SongCreateAndUpdateContract>
        {
            new SongCreateAndUpdateContract
            {
                Name = "arte",
                Number = 1,
                DurationInSeconds = 0,
                FeaturingArtistIds = []
            }
        };

        var result = songValidation.Validate(songs);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.SongDurationInSecondsPositiveCheck.Replace("{PropertyName}", "Duration In Seconds"), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void SongsDurationInSeconds_MustBeMoreThanZero()
    {
        var songs = new List<SongCreateAndUpdateContract>
        {
            new SongCreateAndUpdateContract
            {
                Name = "arte",
                Number = 1,
                DurationInSeconds = 1,
                FeaturingArtistIds = []
            }
        };

        var result = songValidation.Validate(songs);

        Assert.IsFalse(!result.IsValid);
    }
}