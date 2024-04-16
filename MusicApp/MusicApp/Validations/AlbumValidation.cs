using FluentValidation;
using MusicApp.Contracts;
using Enums.AlbumTypes;
using Utils;
using DataBase.Models;
using Microsoft.IdentityModel.Tokens;

namespace MusicApp.Validations;

public class AlbumCreateAndUpdateValidation : AbstractValidator<AlbumCreateAndUpdateContract>
{
    public AlbumCreateAndUpdateValidation()
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(Resources.Validations.AlbumNameCheckIsNotEmpty)
            .Length(4, 30).WithMessage(Resources.Validations.AlbumNameCheckLength);

        RuleFor(x => x.ReleaseDate)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(Resources.Validations.AlbumReleaseCheckIsNotEmpty)
            .LessThan(x => DateTime.Now).WithMessage(Resources.Validations.AlbumReleaseShouldNotBeFuture);

        RuleFor(x => x.Genres)
            .Must(list => list.Count > 0 && list.Count < 10).WithMessage(Resources.Validations.AlbumGenresCheckIsNotEmpty);

        RuleForEach(x => x.Genres)
            .Length(3, 20).WithMessage(Resources.Validations.AlbumGenresCheckValidGenre);

        RuleFor(x => x.Type)
            .Must(x => x != AlbumTypes.Undefined).WithMessage(Resources.Validations.AlbumTypeCheckIsNotEmpty);
    }
}

public class AlbumsPaginationValidation : AbstractValidator<AlbumPagination>
{
    public AlbumsPaginationValidation()
    {
        RuleFor(a => a.PageNumber)
            .Must(a => a > 0).WithMessage(Resources.Validations.PageNumberCheckValidInput);

        RuleFor(a => a.PageSize)
            .Must(a => a > 0).WithMessage(Resources.Validations.PageSizeCheckValidInput);

        RuleFor(a => a.OrderBy)
            .Must(a => a == "desc" || a == "asc").WithMessage(Resources.Validations.OrderByCheckValues);


        var albumProperties = typeof(AlbumModel)
            .GetProperties()
            .Select(property => property.Name.ToLower())
            .ToList();

        RuleFor(a => a.SortBy)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(Resources.Validations.AlbumSortByCheckIsNotEmpty)
            .Must(a => albumProperties.Contains(a.ToLower())).WithMessage(string.Format(Resources.Validations.SortByCheckCorrectValues, string.Join(", ", albumProperties)));
    }
}