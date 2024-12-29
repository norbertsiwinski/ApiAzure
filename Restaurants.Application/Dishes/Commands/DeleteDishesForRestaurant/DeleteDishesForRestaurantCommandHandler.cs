using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDishesForRestaurant;

public class DeleteDishesForRestaurantCommandHandler(ILogger<GetDishesForRestaurantQueryHandler> logger,
IRestaurantsRepository restaurantsRepository,
IDishesRepository dishesRepository,
IRestaurantAuthorizationService restaurantAuthorizationService) 
    : IRequestHandler<DeleteDishesForRestaurantCommand>
{
    public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogWarning($"Deleting all dishes from restuarant id: {request.RestaurantId}");

        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId) 
            ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        if (!restaurantAuthorizationService.Authorize(restaurant, Domain.Constants.ResourceOperation.Delete))
            throw new ForbidException();

        await dishesRepository.Delete(restaurant.Dishes);
    }
}
