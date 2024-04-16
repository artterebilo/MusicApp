using MusicApp.Contracts;
using MusicApp.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Unit.Tests;

public class UserTests
{
    private UserCreateValidation validation;

    [SetUp]
    public void Setup()
    {
        validation = new UserCreateValidation();
    }

    [Test]
    public void UserLogin_MustBeEmpty()
    {
        var user = new UserCreateContract
        {
            Login = "",
            Password = "Ks7220126!",
            Email = "test@gmail.com",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.UserLoginCheckIsNotEmpty.Replace("{PropertyName}", nameof(user.Login)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void UserLogin_MustBeCorrect()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Test11111!",
            Email = "test@gmail.com",
            FirstName = "test",
            LastName = "test",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void UserLogin_MustNotBeUnique()
    {
        var user = new UserCreateContract
        {
            Login = "FickLeWhite",
            Password = "Ks7220126!",
            Email = "test@gmail.com",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.UserLoginShouldBeUnique.Replace("{PropertyName}", nameof(user.Login)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void UserLogin_MustBeUnique()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Ks7220126!",
            Email = "test@gmail.com",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void UserLogin_MustContainAtLeast3Characters()
    {
        var user = new UserCreateContract
        {
            Login = "tes",
            Password = "Ks7220126!",
            Email = "test@gmail.com",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.UserLoginMustContainAtLeast4Characters.Replace("{PropertyName}", nameof(user.Login)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void UserLogin_MustContainAtLeast4Characters()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Ks7220126!",
            Email = "test@gmail.com",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void UserLogin_MustContainAtLeast51Characters()
    {
        var user = new UserCreateContract
        {
            Login = "Antidisestablishmentarianismosuperfantabuloustastic",
            Password = "Ks7220126!",
            Email = "test@gmail.com",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.UserLoginCannotExceed50Characters.Replace("{PropertyName}", nameof(user.Login)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void UserLogin_MustContainAtLeast50Characters()
    {
        var user = new UserCreateContract
        {
            Login = "Supercalifragilisticexpialidociousfantabulousmagni",
            Password = "Ks7220126!",
            Email = "test@gmail.com",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void UserLogin_CannotContainOnlyLettersNumberAndUnderscore()
    {
        var user = new UserCreateContract
        {
            Login = "test$$%^&*(",
            Password = "Ks7220126!",
            Email = "test@gmail.com",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.UserLoginCanContainOnlyLettersNumberAndUnderscore.Replace("{PropertyName}", nameof(user.Login)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void UserLogin_CanContainOnlyLettersNumberAndUnderscore()
    {
        var user = new UserCreateContract
        {
            Login = "test_TEST",
            Password = "Ks7220126!",
            Email = "test@gmail.com",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void UserPassword_MustBeEmpty()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "",
            Email = "test@gmail.com",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.UserPasswordCannotBeEmpty.Replace("{PropertyName}", nameof(user.Password)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void UserPassword_MustBeNotEmpty()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Ks7220126!",
            Email = "test@gmail.com",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void UserPassword_MustContainAtLeast7Characters()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Ks7220!",
            Email = "test@gmail.com",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.UserPasswordMustContainAtLeast8Characters.Replace("{PropertyName}", nameof(user.Password)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void UserPassword_MustContainAtLeast8Characters()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Ks72201!",
            Email = "test@gmail.com",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void UserPassword_MustContainAtLeast51Characters()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Antidisestablishmentarianismosuperfantabuloustast1!",
            Email = "test@gmail.com",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.UserPasswordCannotExceed50Characters.Replace("{PropertyName}", nameof(user.Password)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void UserPassword_MustContainAtLeast50Characters()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Antidisestablishmentarianismosuperfntabuloustast1!",
            Email = "test@gmail.com",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void UserPassword_MustNotContainAtLeastOneUppercaseLetter()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "antidisestablishmentariansmosuperfantabuloustast1!",
            Email = "test@gmail.com",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.UserPasswordMustContainAtLeastOneUppercaseLetter.Replace("{PropertyName}", nameof(user.Password)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void UserPassword_MustContainAtLeastOneUppercaseLetter()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Antidisestablishmentariansmosuperfantabuloustast1!",
            Email = "test@gmail.com",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void UserPassword_MustNotContainAtLeastOneLowercaseLetter()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA1!",
            Email = "test@gmail.com",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.UserPasswordMustContainAtLeastOneLowercaseLetter.Replace("{PropertyName}", nameof(user.Password)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void UserPassword_MustContainAtLeastOneLowercaseLetter()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Antidisestablishmentariansmosuperfantabuloustast1!",
            Email = "test@gmail.com",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void UserPassword_MustNotContainAtLeastOneDigit()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Antidisestablishmentariansmosuperfantabuloustast!",
            Email = "test@gmail.com",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.UserPasswordMustContainAtLeastOneDigit.Replace("{PropertyName}", nameof(user.Password)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void UserPassword_MustContainAtLeastOneDigit()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Antidisestablishmentariansmosuperfantabuloustast1!",
            Email = "test@gmail.com",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void UserPassword_MustNotContainAtLeastOneSpecialCharacter()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Antidisestablishmentariansmosuperfantabuloustast1",
            Email = "test@gmail.com",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.UserPasswordMustContainAtLeastOneSpecialCharacter.Replace("{PropertyName}", nameof(user.Password)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void UserPassword_MustContainAtLeastOneSpecialCharacter()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Antidisestablishmentariansmosuperfantabuloustast1!",
            Email = "test@gmail.com",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void UserEmail_CanBeEmpty()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Antidisestablishmentariansmosuperfantabuloustast1!",
            Email = "",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.UserEmailCannotBeEmpty.Replace("{PropertyName}", nameof(user.Email)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void UserEmail_CannotBeEmpty()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Antidisestablishmentariansmosuperfantabuloustast1!",
            Email = "test@gmail.com",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void UserEmail_InvalidFormat()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Antidisestablishmentariansmosuperfantabuloustast1!",
            Email = "testmail",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.UserEmailInvalidFormat.Replace("{PropertyName}", nameof(user.Email)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void UserEmail_CorrectFormat()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Antidisestablishmentariansmosuperfantabuloustast1!",
            Email = "test@mail.com",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void UserEmail_CannotExceed321Characters()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Antidisestablishmentariansmosuperfantabuloustast1!",
            Email = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa@gmail.com",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.UserEmailCannotExceed320Characters.Replace("{PropertyName}", nameof(user.Email)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void UserEmail_CannotExceed320Characters()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Antidisestablishmentariansmosuperfantabuloustast1!",
            Email = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa@gmail.com",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void UserEmail_MustBeNotUnique()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Antidisestablishmentariansmosuperfantabuloustast1!",
            Email = "art@gmail.com",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.UserEmailShouldBeUnique.Replace("{PropertyName}", nameof(user.Email)), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void UserEmail_MustBeUnique()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Antidisestablishmentariansmosuperfantabuloustast1!",
            Email = "test@gmail.com",
            FirstName = "Artem",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void UserFirstName_CanBeEmpty()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Antidisestablishmentariansmosuperfantabuloustast1!",
            Email = "test@gmail.com",
            FirstName = "",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.UserFirstNameCannotBeEmpty.Replace("{PropertyName}", "First Name"), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void UserFirstName_CannotBeEmpty()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Antidisestablishmentariansmosuperfantabuloustast1!",
            Email = "test@gmail.com",
            FirstName = "Test",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void UserFirstName_CannotExceed51Characters()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Antidisestablishmentariansmosuperfantabuloustast1!",
            Email = "test@gmail.com",
            FirstName = "testtesttesttesttesttesttesttesttesttesttesttesttest",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.UserFirstNameCannotExceed50Characters.Replace("{PropertyName}", "First Name"), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void UserFirstName_CannotExceed50Characters()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Antidisestablishmentariansmosuperfantabuloustast1!",
            Email = "test@gmail.com",
            FirstName = "testtesttesttesttesttesttesttesttesttesttesttest",
            LastName = "Terebilo",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void UserLastName_CanBeEmpty()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Antidisestablishmentariansmosuperfantabuloustast1!",
            Email = "test@gmail.com",
            FirstName = "test",
            LastName = "",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.UserLastNameCannotBeEmpty.Replace("{PropertyName}", "Last Name"), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void UserLastName_CannotBeEmpty()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Antidisestablishmentariansmosuperfantabuloustast1!",
            Email = "test@gmail.com",
            FirstName = "test",
            LastName = "test",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void UserLastName_CannotExceed51Characters()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Antidisestablishmentariansmosuperfantabuloustast1!",
            Email = "test@gmail.com",
            FirstName = "test",
            LastName = "testtesttesttesttesttesttesttesttesttesttesttesttest",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.UserLastNameCannotExceed50Characters.Replace("{PropertyName}", "Last Name"), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void UserLastName_CannotExceed50Characters()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Antidisestablishmentariansmosuperfantabuloustast1!",
            Email = "test@gmail.com",
            FirstName = "test",
            LastName = "testtesttesttesttesttesttesttesttesttesttesttest",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void UserDateOfBirth_CanBeInTheFuture()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Antidisestablishmentariansmosuperfantabuloustast1!",
            Email = "test@gmail.com",
            FirstName = "test",
            LastName = "test",
            DateOfBirth = new DateTime(2025, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.UserDateOfBirthCannotBeInTheFuture.Replace("{PropertyName}", "Date Of Birth"), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void UserDateOfBirth_CanBeNotInTheFuture()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Antidisestablishmentariansmosuperfantabuloustast1!",
            Email = "test@gmail.com",
            FirstName = "test",
            LastName = "test",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(!result.IsValid);
    }

    [Test]
    public void UserDateOfBirth_IsTooOld()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Antidisestablishmentariansmosuperfantabuloustast1!",
            Email = "test@gmail.com",
            FirstName = "test",
            LastName = "test",
            DateOfBirth = new DateTime(1873, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Count() == 1);
        Assert.That(actual: Resources.Validations.UserDateOfBirthIsTooOld.Replace("{PropertyName}", "Date Of Birth"), Is.EqualTo(result.Errors[0].ErrorMessage));
    }

    [Test]
    public void UserDateOfBirth_NotIsTooOld()
    {
        var user = new UserCreateContract
        {
            Login = "test",
            Password = "Antidisestablishmentariansmosuperfantabuloustast1!",
            Email = "test@gmail.com",
            FirstName = "test",
            LastName = "test",
            DateOfBirth = new DateTime(1994, 04, 13)
        };

        var result = validation.Validate(user);

        Assert.IsFalse(!result.IsValid);
    }
}