namespace ProvaPub.Models
{
	public class Customer: Entity
    {
		public ICollection<Order> Orders { get; set; }
	}
}
