using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurants;

public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger, 
    IMapper mapper, 
    IRestaurantsRepository restaurantsRepository,
    IUserContext userContextService) 
    : IRequestHandler<CreateRestaurantCommand, Guid>
{
    public async Task<Guid> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a new restaurant {@Restaurant}", request);

        var user = userContextService.GetCurrentUser();
        var restaurant = mapper.Map<Restaurant>(request);
        restaurant.OwnerId = user!.Id;

        Guid id = await restaurantsRepository.Create(restaurant);
        return id;
    }
}