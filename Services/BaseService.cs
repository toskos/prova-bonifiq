using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class BaseService<TEntity> where TEntity : Entity
    {
        private readonly TestDbContext _ctx;

        public BaseService(TestDbContext ctx)
        {
            _ctx = ctx;
        }

        public ResultRecord<TEntity> List(int page)
        {
            return new ResultRecord<TEntity>() { HasNext = false, TotalCount = 10, RecordSet = _ctx.Set<TEntity>().ToList().Skip(10*page).Take(10) };
        }
    }
}
