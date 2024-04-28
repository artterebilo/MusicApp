using FluentValidation;
using MusicApp.Contracts;
using MusicApp.Services;
using Utils;

namespace MusicApp.Validations;

public class UserLikeValidation : AbstractValidator<UserLikeSongContract> 
{
    public UserLikeValidation()
    {
        RuleFor(user => user.SongId)
            .Must(UserLikeSongService.checkIfThereIsSong).WithMessage("This song not found");
    }
}

public class LikePaginationValidation : AbstractValidator<LikePagination>
{
    public LikePaginationValidation()
    {
        RuleFor(p => p.PageSize)
            .Must(x => x > 0).WithMessage("Incorrect PageSize");

        RuleFor(p => p.PageNumber)
            .Must(x => x > 0).WithMessage("Incorrect PageNumber");

        RuleFor(p => p.OrderBy)
            .Must(a => a == "desc" || a == "asc").WithMessage("Need desc or asc");

        var properties = typeof(ArtistAlbumSingInformationContract)
            .GetProperties()
            .Select(property => property.Name.ToLower())
            .ToList();

        RuleFor(p => p.SortBy)
            .Must(p => properties.Contains(p.ToLower())).WithMessage("rrrr");
    }
}