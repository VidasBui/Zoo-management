using Zoo_management.Data.Entities;

namespace Zoo_management.Data.Repositories
{
    public class AnimalsRepository : CRUDRepository<Animal>, IAnimalsRepository
    {
        public AnimalsRepository(ZooDbContext dbContext)
            : base(dbContext,
                   context => context.Animals,
                   x => x.Id)
        {
        }
    }
}
