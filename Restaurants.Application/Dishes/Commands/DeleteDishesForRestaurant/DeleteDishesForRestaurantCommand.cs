using MediatR;

namespace Restaurants.Application.Dishes.Commands.DeleteDishesForRestaurant;

public class DeleteDishesForRestaurantCommand(Guid restuarantId) : IRequest
{
    public Guid RestaurantId { get; } = restuarantId;
}
