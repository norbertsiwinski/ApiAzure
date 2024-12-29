using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;
using System.Net;

namespace Restaurants.Infrastructure.Seeders;

internal class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
{
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
        {

            if (!dbContext.Restaurants.Any())
            {
                var restaurants = GetRestaurants();
                dbContext.Restaurants.AddRange(restaurants);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }
        }
    }


    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles =
            [
                new (UserRoles.User)
                {
                    NormalizedName = UserRoles.User.ToUpper()
                },
                new (UserRoles.Owner)
                {
                    NormalizedName = UserRoles.Owner.ToUpper()
                },
                new (UserRoles.Admin)
                {
                    NormalizedName = UserRoles.Admin.ToUpper()
                }
            ];
        return roles;
    }

    private IEnumerable<Restaurant> GetRestaurants()
    {
        return new List<Restaurant>
    {
        new Restaurant
        {
            Id = Guid.NewGuid(),
            Name = "Italiano Pizza",
            Description = "Authentic Italian pizzeria offering delicious handmade pizzas.",
            Category = "Italian",
            HasDelivery = true,
            ContactEmail = "contact@italianopizza.com",
            ContactNumber = "123-456-789",
            Address = new Address
            {
                City = "Rome",
                Street = "Via Roma 10",
                PostalCode = "00100"
            },
            Dishes = new List<Dish>
            {
                new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Margherita",
                    Description = "Classic pizza with tomato sauce, mozzarella, and basil.",
                    Price = 8.99m,
                    RestaurantId = Guid.NewGuid()
                },
                new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Pepperoni",
                    Description = "Spicy pepperoni with mozzarella cheese and tomato sauce.",
                    Price = 9.99m,
                    RestaurantId = Guid.NewGuid()
                }
            }
        },
        new Restaurant
        {
            Id = Guid.NewGuid(),
            Name = "Sushi World",
            Description = "Fresh sushi and Japanese cuisine with a modern twist.",
            Category = "Japanese",
            HasDelivery = false,
            ContactEmail = "info@sushiworld.com",
            ContactNumber = "987-654-321",
            Address = new Address
            {
                City = "Tokyo",
                Street = "Shibuya 5-10",
                PostalCode = "150-0001"
            },
            Dishes = new List<Dish>
            {
                new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Salmon Nigiri",
                    Description = "Fresh salmon served on vinegared rice.",
                    Price = 5.99m,
                    RestaurantId = Guid.NewGuid()
                },
                new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "California Roll",
                    Description = "Crab, avocado, and cucumber wrapped in seaweed and rice.",
                    Price = 7.99m,
                    RestaurantId = Guid.NewGuid()
                }
            }
        },
        new Restaurant
        {
            Id = Guid.NewGuid(),
            Name = "Burger King",
            Description = "Famous fast-food burgers and fries with quick delivery.",
            Category = "Fast Food",
            HasDelivery = true,
            ContactEmail = "contact@burgerking.com",
            ContactNumber = "555-123-456",
            Address = new Address
            {
                City = "New York",
                Street = "Broadway 200",
                PostalCode = "10001"
            }
        }
    };
    }

}