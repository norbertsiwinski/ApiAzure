using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Restaurants.Commands.CreateRestaurants;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Xunit;

namespace Restaurants.ApplicationTests.Restaurants.Commands.CreateRestaurants;

public class CreateRestaurantCommandHandlerTests
{
    [Fact()]
    public async Task Handle_ForValidCommand_ReturnsCreatedRestaurantId()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();
        var mapperMock = new Mock<IMapper>();

        var command = new CreateRestaurantCommand();
        var restaurant = new Restaurant();

        mapperMock.Setup(m => m.Map<Restaurant>(command)).Returns(restaurant);

        var restaurantRepostoryMock = new Mock<IRestaurantsRepository>();
        restaurantRepostoryMock.Setup(repo => repo.Create(It.IsAny<Restaurant>()))
            .ReturnsAsync(guid);

        var userContextMock = new Mock<IUserContext>();
        var currentUser = new CurrentUser("owner-id", "test@test.com", [], null, null);
        userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

        var commandHandler = new CreateRestaurantCommandHandler(loggerMock.Object, 
            mapperMock.Object, 
            restaurantRepostoryMock.Object,
            userContextMock.Object);

        // Act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(guid);
        restaurant.OwnerId.Should().Be("owner-id");
        restaurantRepostoryMock.Verify(r => r.Create(restaurant), Times.Once());
    }
}