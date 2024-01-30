namespace ProvaPub.Models
{
    public class ResultRecord<TEntity> where TEntity : Entity
    {
        public IEnumerable<TEntity>? RecordSet { get; set; }

        public int TotalCount { get; set; }

        public bool HasNext { get; set; }
    }
}
