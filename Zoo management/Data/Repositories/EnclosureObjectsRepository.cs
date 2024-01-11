using Zoo_management.Data.Entities;

namespace Zoo_management.Data.Repositories
{
    public class EnclosureObjectsRepository : CRUDRepository<EnclosureObject>, IEnclosureObjectsRepository
    {
        public EnclosureObjectsRepository(ZooDbContext dbContext)
            : base(dbContext,
                   context => context.EnclosureObjects,
                   x => x.Id)
        {
        }
    }
}
