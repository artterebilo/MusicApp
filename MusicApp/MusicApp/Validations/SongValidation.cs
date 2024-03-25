using FluentValidation;
using MusicApp.Contracts;

namespace MusicApp.Validations;

public class SongUpdateValidation: AbstractValidator<SongCreateAndUpdateContract>
{
    public SongUpdateValidation()
    {
        RuleFor(song => song.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(Resources.Validations.SongNameCheckIsNotEmpty)
            .Length(4, 30).WithMessage(Resources.Validations.SongNameCheckCorrectLength);

        RuleFor(song => song.Number)
            .Must(number => number > 0).WithMessage(Resources.Validations.SongNumberPositiveCheck);

        RuleFor(song => song.DurationInSeconds)
            .Must(DurationInSeconds => DurationInSeconds > 0).WithMessage(Resources.Validations.SongDurationInSecondsPositiveCheck);
    }
}
public class SongsCreateAndUpdateValidation: AbstractValidator<List<SongCreateAndUpdateContract>>
{
    public SongsCreateAndUpdateValidation()
    {
        RuleForEach(song => song)
            .ChildRules(song =>
            {
                song.RuleFor(song => song.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(Resources.Validations.SongNameCheckIsNotEmpty)
                .Length(4, 30).WithMessage(Resources.Validations.SongNameCheckCorrectLength);
            });

        RuleForEach(song => song)
            .ChildRules(song =>
            {
                song.RuleFor(song => song.Number)
                .Must(number => number > 0).WithMessage(Resources.Validations.SongNumberPositiveCheck);
            });

        RuleForEach(song => song)
            .ChildRules(song =>
            {
                song.RuleFor(song => song.DurationInSeconds)
                .Must(DurationInSeconds => DurationInSeconds > 0).WithMessage(Resources.Validations.SongDurationInSecondsPositiveCheck);
            });
    }
}


