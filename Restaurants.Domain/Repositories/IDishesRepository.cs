using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Repositories;

public interface IDishesRepository
{
    Task<Guid> Create(Dish entity);
    Task Delete(IEnumerable<Dish> entities);
}
