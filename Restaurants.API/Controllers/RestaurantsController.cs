using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurants;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Commands.UploadRestaurantLogo;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Constants;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurants")]
[Authorize]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll([FromQuery] GetAllRestaurantQuery query)
    {
        var restaurants = await mediator.Send(query);
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    //[Authorize(Policy = PolicyNames.HasNationality)]
    public async Task<IActionResult> Get(Guid id)
    {
        var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));

        return Ok(restaurant);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRestaurant(Guid id)
    {
        await mediator.Send(new DeleteRestaurantCommand(id));

        return NoContent();
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.Owner)]
    public async Task<IActionResult> CreateRestaurant(CreateRestaurantCommand command)
    {
        Guid id = await mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id }, null);
    }

    [HttpPost("{id}/logo")]
    public async Task<IActionResult> UploadLogo([FromRoute]Guid id, IFormFile file)
    {
        using var stream = file.OpenReadStream();

        var command = new UploadRestaurantLogoCommand()
        {
            RestaurantId = id,
            FileName = $"{id}-{file.FileName}",
            File = stream
        };

        await mediator.Send(command);
         return NoContent();
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRestaurant(Guid id, UpdateRestaurantCommand command)
    {
        command.Id = id;
        await mediator.Send(command);

        return NotFound();
    }
}