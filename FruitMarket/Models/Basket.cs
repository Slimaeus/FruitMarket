using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FruitMarket.Models
{
    public class Basket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        public int Amount { get; set; }

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public ICollection<Fruit>? Fruits { get; set; }
    }
}
