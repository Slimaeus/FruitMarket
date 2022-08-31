using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FruitMarket.Models
{
    public class Fruit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Color { get; set; }

        public int Price { get; set; }

        public int BasketId { get; set; }
        public Basket? Basket { get; set; }
    }
}
