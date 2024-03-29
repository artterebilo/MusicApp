﻿using DataBase.Models;
using FluentValidation;
using MusicApp.Contracts;
using Utils;

namespace MusicApp.Validations;

public class ArtistCreateValidation : AbstractValidator<ArtistCreateContract>
{
    public ArtistCreateValidation()
    {
        RuleFor(a => a.Name)
            .NotEmpty().WithMessage(Resources.Validations.ArtistNameCheckIsNotEmpty)
            .Length(4, 30).WithMessage(Resources.Validations.ArtistNameCheckLength);

        RuleFor(a => a.Description)
            .NotEmpty().WithMessage(Resources.Validations.ArtistDescriptionCheckIsNotEmpty)
            .Length(10, 1500).WithMessage(Resources.Validations.ArtistDescriptionCheckLength);

        RuleFor(a => a.Genres)
            .Must(list => list.Count > 0 && list.Count < 10).WithMessage(Resources.Validations.ArtistGenresCheckIsNotEmpty);

        RuleForEach(a => a.Genres)
            .Length(3, 20).WithMessage(Resources.Validations.ArtistGenresCheckValidGenre);
    }
}

public class ArtistUpdateValidation : AbstractValidator<ArtistUpdateContract>
{
    public ArtistUpdateValidation()
    {
        RuleFor(a => a.Name)
            .NotEmpty().WithMessage(Resources.Validations.ArtistNameCheckIsNotEmpty)
            .Length(4, 30).WithMessage(Resources.Validations.ArtistNameCheckLength);

        RuleFor(a => a.Description)
            .NotEmpty().WithMessage(Resources.Validations.ArtistDescriptionCheckIsNotEmpty)
            .Length(10, 1500).WithMessage(Resources.Validations.ArtistDescriptionCheckLength);

        RuleFor(a => a.Genres)
            .Must(list => list.Count > 0 && list.Count < 10).WithMessage(Resources.Validations.ArtistGenresCheckIsNotEmpty);

        RuleForEach(a => a.Genres)
            .Length(3, 20).WithMessage(Resources.Validations.ArtistGenresCheckValidGenre);
    }
}

public class AtristsPaginationValidation: AbstractValidator<DefaultPagination>
{
    public AtristsPaginationValidation()
    {
        RuleFor(a => a.PageNumber)
            .Must(a => a > 0).WithMessage(Resources.Validations.PageNumberCheckValidInput);

        RuleFor(a => a.PageSize)
            .Must(a => a > 0).WithMessage(Resources.Validations.PageSizeCheckValidInput);
    }
}
                