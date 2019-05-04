namespace Conduit.ApplicationCore.Entities
{
    public class BaseEntity<TKey>
    {
        public virtual TKey Id { get; set; }
    }
}
