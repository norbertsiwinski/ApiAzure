using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger, 
    IRestaurantsRepository restaurantsRepository,
    IDishesRepository dishRepository,
    IRestaurantAuthorizationService restaurantAuthorizationService,
    IMapper mapper) : IRequestHandler<CreateDishCommand, Guid>
{
    public async Task<Guid> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new dish: {@DishRequest}", request);
        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId) 
            ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        if (!restaurantAuthorizationService.Authorize(restaurant, Domain.Constants.ResourceOperation.Create))
            throw new ForbidException();

        var dish = mapper.Map<Dish>(request);
        return await dishRepository.Create(dish);
    }
}
