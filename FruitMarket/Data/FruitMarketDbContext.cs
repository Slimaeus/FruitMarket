using FruitMarket.Models;
using Microsoft.EntityFrameworkCore;

namespace FruitMarket.Data
{
    public class FruitMarketDbContext : DbContext
    {
        public FruitMarketDbContext(DbContextOptions<FruitMarketDbContext> options) : base(options)
        {

        }

        public DbSet<Fruit> Fruits { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Customer> Customers { get; set; }
        //public DbSet<Market> Markets { get; set; }
    }
}
