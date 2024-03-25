using FluentValidation;
using MusicApp.Contracts;

namespace MusicApp.Validations;

public class SongUpdateValidation: AbstractValidator<SongUpdateContract>
{
    public SongUpdateValidation()
    {
        RuleFor(song => song.Name)
            .NotEmpty()
                .WithMessage(Resources.Validations.SongNameCheckIsNotEmpty)
                .Length(4, 30)
                .WithMessage(Resources.Validations.SongNameCheckCorrectLength);

        RuleFor(song => song.Number)
            .Must(number => number > 0)
                .WithMessage(Resources.Validations.SongNumberPositiveCheck);

        RuleFor(song => song.DurationInSeconds)
            .Must(DurationInSeconds => DurationInSeconds > 0)
                .WithMessage(Resources.Validations.SongDurationInSecondsPositiveCheck);
    }
}
public class SongsCreateValidation: AbstractValidator<List<SongCreateContract>>
{
    public SongsCreateValidation()
    {
        RuleForEach(song => song)
            .ChildRules(song =>
            {
                song.RuleFor(song => song.Name)
                .NotEmpty()
                .WithMessage(Resources.Validations.SongNameCheckIsNotEmpty)
                .Length(4, 30)
                .WithMessage(Resources.Validations.SongNameCheckCorrectLength);
            });

        RuleForEach(song => song)
            .ChildRules(song =>
            {
                song.RuleFor(song => song.Number)
                .Must(number => number > 0)
                .WithMessage(Resources.Validations.SongNumberPositiveCheck);
            });

        RuleForEach(song => song)
            .ChildRules(song =>
            {
                song.RuleFor(song => song.DurationInSeconds)
                .Must(DurationInSeconds => DurationInSeconds > 0)
                .WithMessage(Resources.Validations.SongDurationInSecondsPositiveCheck);
            });
    }
}

public class SongsUpdateValidation: AbstractValidator<List<SongUpdateContract>>
{
    public SongsUpdateValidation()
    {
        RuleForEach(song => song)

            .ChildRules(song =>
            {
                song.RuleFor(song => song.Name)
                .NotEmpty()
                .WithMessage(Resources.Validations.SongNameCheckIsNotEmpty)
                .Length(4, 30)
                .WithMessage(Resources.Validations.SongNameCheckCorrectLength);
            });

        RuleForEach(song => song)
            .ChildRules(song =>
            {
                song.RuleFor(song => song.Number)
                .Must(number => number > 0)
                .WithMessage(Resources.Validations.SongNumberPositiveCheck);
            });

        RuleForEach(song => song)
            .ChildRules(song =>
            {
                song.RuleFor(song => song.DurationInSeconds)
                .Must(DurationInSeconds => DurationInSeconds > 0)
                .WithMessage(Resources.Validations.SongDurationInSecondsPositiveCheck);
            });
    }
}

