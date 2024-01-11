using Zoo_management.Data.Entities;

namespace Zoo_management.Data.Repositories
{
    public class EnclosuresRepository : CRUDRepository<Enclosure>, IEnclosuresRepository
    {

        public EnclosuresRepository(ZooDbContext dbContext)
            : base(dbContext,
                   context => context.Enclosures,  
                   x => x.Id)        
        {
        }
    }
}
