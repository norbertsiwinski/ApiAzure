using FluentValidation.TestHelper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurants;
using Xunit;

namespace Restaurants.ApplicationTests.Restaurants.Commands.CreateRestaurants;

public class CreateRestaurantCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
    {
        // Arrange

        var command = new CreateRestaurantCommand()
        {
            Name = "Test",
            Category = "Italian",
            ContactEmail = "test@test.com",
            PostalCode = "12-345"
        };

        var validator = new CreateRestaurantCommandValidator();

        // Act
        var result = validator.TestValidate(command);

        // Assert

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void Validator_ForInvalidCommand_ShouldHaveValidationErrors() 
    {
        // Arrange

        var command = new CreateRestaurantCommand()
        {
            Name = "Te",
            Category = "Ital",
            ContactEmail = "@test.com",
            PostalCode = "1245"
        };

        var validator = new CreateRestaurantCommandValidator();

        // Act
        var result = validator.TestValidate(command);

        // Assert

        result.ShouldHaveValidationErrorFor(c => c.Name);
        result.ShouldHaveValidationErrorFor(c => c.Category);
        result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
    }


    [Theory()]
    [InlineData("Italian")]
    [InlineData("Mexican")]
    [InlineData("American")]
    public void Validator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(string category)
    {
        // Arrange

        var command = new CreateRestaurantCommand() 
        { 
            Category = category,
        };

        var validator = new CreateRestaurantCommandValidator();

        // Act

        var result = validator.TestValidate(command);

        // Assert

        result.ShouldNotHaveValidationErrorFor(c=> c.Category);
    }

    [Theory()]
    [InlineData("102220")]
    [InlineData("102-20")]
    [InlineData("10 200")]
    public void Validator_ForInValidPostalCode_ShouldNotHaveValidationErrorsForPostalCodeProperty(string postalCode)
    {
        // Arrange

        var command = new CreateRestaurantCommand()
        {
            PostalCode = postalCode,
        };

        var validator = new CreateRestaurantCommandValidator();

        // Act

        var result = validator.TestValidate(command);

        // Assert

        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
    }
}