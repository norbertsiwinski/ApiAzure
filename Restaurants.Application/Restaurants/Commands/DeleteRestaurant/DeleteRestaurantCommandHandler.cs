using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger, IRestaurantsRepository restaurantsRepository) : IRequestHandler<DeleteRestaurantCommand>
{
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting restaurant {RestaurantId}", request.Id);
        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);

        if (restaurant is null)
            throw new NotFoundException($"Restaurant with {request.Id} not found");

        await restaurantsRepository.Delete(restaurant);
    }
}