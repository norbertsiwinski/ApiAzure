using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurants;

public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string> _categoriesList = new() { "Italian", "American", "Mexican" };

    public CreateRestaurantCommandValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 100);

        RuleFor(dto => dto.Category)
            .Must(_categoriesList.Contains)
            .WithMessage("Category is wrong");
        //.Custom((value, context) =>
        //{
        //    var isValidCategory = _categoriesList.Contains(value);
        //    if (!isValidCategory)
        //    {
        //        context.AddFailure("Category","Category is wrong");
        //    }
        //});

        RuleFor(dto => dto.ContactEmail)
            .EmailAddress().WithMessage("Provide valid email");

        RuleFor(dto => dto.PostalCode)
            .Matches(@"^\d{2}-\d{3}$").WithMessage("Provide (XX-XXX)");
    }
}