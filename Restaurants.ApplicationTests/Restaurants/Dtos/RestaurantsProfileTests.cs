using AutoMapper;
using FluentAssertions;
using Restaurants.Application.Restaurants.Commands.CreateRestaurants;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Xunit;

namespace Restaurants.ApplicationTests.Restaurants.Dtos
{
    public class RestaurantsProfileTests
    {
        private IMapper _mapper;

        public RestaurantsProfileTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RestaurantsProfile>();
            });

            _mapper = configuration.CreateMapper();
        }

        [Fact()]
        public void CreateMap_ForRestaurantToRestaurantDto_MapsCorrectly()
        {
            // Arrange

            var restaurant = new Restaurant()
            {
                Id = new Guid(),
                Name = "test",
                Description = "test DESC",
                Category = "test",
                HasDelivery = true,
                ContactEmail = "test@test.com",
                ContactNumber = "123456789",
                Address = new Address()
                {
                    City = "test",
                    Street = "test",
                    PostalCode = "12345",
                }
            };

            // Act

            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

            // Assert

            restaurantDto.Should().NotBeNull();
            restaurantDto.Id.Should().Be(restaurant.Id);
            restaurantDto.Name.Should().Be(restaurant.Name);
            restaurantDto.Description.Should().Be(restaurant.Description);
            restaurantDto.Name.Should().Be(restaurant.Name);
            restaurantDto.City.Should().Be(restaurant.Address.City);
        }

        [Fact()]
        public void CreateMap_ForCreateRestaurantToRestaurant_MapsCorrectly()
        {
            // Arrange

            var command = new CreateRestaurantCommand()
            {
                Name = "test",
                Description = "test DESC",
                Category = "test",
                HasDelivery = true,
                ContactEmail = "test@test.com",
                ContactNumber = "123456789",
                City = "test",
                Street = "test",
                PostalCode = "12345",
            };

            // Act

            var restaurant = _mapper.Map<Restaurant>(command);

            // Assert

            restaurant.Should().NotBeNull();
            restaurant.Name.Should().Be(command.Name);
            restaurant.Description.Should().Be(command.Description);
            restaurant.Name.Should().Be(command.Name);
            restaurant.Address.City.Should().Be(command.City);
        }

        [Fact()]
        public void CreateMap_ForUpdateRestaurantCommandToRestaurant_MapsCorrectly()
        {
            // Arrange

            var command = new UpdateRestaurantCommand()
            {
                Name = "test",
                Description = "test DESC",
                HasDelivery = true,
            };

            // Act

            var restaurant = _mapper.Map<Restaurant>(command);

            // Assert

            restaurant.Should().NotBeNull();
            restaurant.Name.Should().Be(command.Name);
            restaurant.Description.Should().Be(command.Description);
            restaurant.Name.Should().Be(command.Name);
        }
    }
}