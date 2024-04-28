using FluentValidation;
using MusicApp.Contracts;
using MusicApp.Services;

namespace MusicApp.Validations;

public abstract class UserCommonValidation<T> : AbstractValidator<T> where T : IBaseUserModel
{
    public UserCommonValidation()
    {
        RuleFor(user => user.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(Resources.Validations.UserPasswordCannotBeEmpty)
            .MinimumLength(8).WithMessage(Resources.Validations.UserPasswordMustContainAtLeast8Characters)
            .MaximumLength(50).WithMessage(Resources.Validations.UserPasswordCannotExceed50Characters)
            .Matches("[A-Z]").WithMessage(Resources.Validations.UserPasswordMustContainAtLeastOneUppercaseLetter)
            .Matches("[a-z]").WithMessage(Resources.Validations.UserPasswordMustContainAtLeastOneLowercaseLetter)
            .Matches("[0-9]").WithMessage(Resources.Validations.UserPasswordMustContainAtLeastOneDigit)
            .Matches("[^a-zA-Z0-9]").WithMessage(Resources.Validations.UserPasswordMustContainAtLeastOneSpecialCharacter);

        RuleFor(user => user.FirstName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(Resources.Validations.UserFirstNameCannotBeEmpty)
            .MaximumLength(50).WithMessage(Resources.Validations.UserFirstNameCannotExceed50Characters);

        RuleFor(user => user.LastName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(Resources.Validations.UserLastNameCannotBeEmpty)
            .MaximumLength(50).WithMessage(Resources.Validations.UserLastNameCannotExceed50Characters);

        RuleFor(user => user.DateOfBirth)
            .Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Today).WithMessage(Resources.Validations.UserDateOfBirthCannotBeInTheFuture)
            .GreaterThanOrEqualTo(DateTime.Today.AddYears(-150)).WithMessage(Resources.Validations.UserDateOfBirthIsTooOld);
    }
}

public class UserCreateValidation : UserCommonValidation<UserCreateContract>
{
    public UserCreateValidation()
    {
        RuleFor(user => user.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(Resources.Validations.UserEmailCannotBeEmpty)
            .EmailAddress().WithMessage(Resources.Validations.UserEmailInvalidFormat)
            .MaximumLength(320).WithMessage(Resources.Validations.UserEmailCannotExceed320Characters)
            .Must(UserService.IsUniqueEmail).WithMessage(Resources.Validations.UserEmailShouldBeUnique);

        RuleFor(user => user.Login)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage(Resources.Validations.UserLoginCheckIsNotEmpty)
                    .Must(UserService.IsUniqueLogin).WithMessage(Resources.Validations.UserLoginShouldBeUnique)
                    .MinimumLength(4).WithMessage(Resources.Validations.UserLoginMustContainAtLeast4Characters)
                    .MaximumLength(50).WithMessage(Resources.Validations.UserLoginCannotExceed50Characters)
                    .Matches("^[a-zA-Z0-9_]*$").WithMessage(Resources.Validations.UserLoginCanContainOnlyLettersNumberAndUnderscore);
    }
}

public class UserUpdateValidation : UserCommonValidation<UserUpdateContract>
{
    public UserUpdateValidation()
    {

    }
}

public class UserUpdateEmailValidation : AbstractValidator<UserUpdateEmailContract>
{
    public UserUpdateEmailValidation()
    {
        RuleFor(user => user.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(Resources.Validations.UserEmailCannotBeEmpty)
            .EmailAddress().WithMessage(Resources.Validations.UserEmailInvalidFormat)
            .MaximumLength(320).WithMessage(Resources.Validations.UserEmailCannotExceed320Characters)
            .Must(UserService.IsUniqueEmail).WithMessage(Resources.Validations.UserEmailShouldBeUnique);
    }
}

