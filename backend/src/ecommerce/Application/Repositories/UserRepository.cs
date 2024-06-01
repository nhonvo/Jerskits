using ecommerce.Infrastructure.Data;
using ecommerce.Infrastructure.Interface;

namespace ecommerce.Application.Repositories;

public class UserRepository(ApplicationDbContext context) : GenericRepository<User>(context), IUserRepository
{
}
