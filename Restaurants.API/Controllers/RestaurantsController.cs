using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurants;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurants")]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
    {
        var restaurants = await mediator.Send(new GetAllRestaurantQuery());
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
        if (restaurant is null)
            return NotFound();
        
        return Ok(restaurant);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRestaurant(Guid id)
    {
        var isDeleted = await mediator.Send(new DeleteRestaurantCommand(id));
        if (isDeleted)
            return NoContent();

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant(CreateRestaurantCommand command)
    {
        Guid id = await mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id }, null);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRestaurant(Guid id, UpdateRestaurantCommand command)
    {
        command.Id = id;
        var isUpdated = await mediator.Send(command);
        if (isUpdated)
            return NoContent();

        return NotFound();
    }
}