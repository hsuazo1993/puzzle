using DataAccess.Models;

namespace DataAccess.Utils
{
    public static class FurnitureSeed
    {
        public static ICollection<Furniture> Data = new List<Furniture>()
        {
            new Furniture { Id = 1, Name = "Table", Description = "Wooden table with 4 chairs", Price = 200.00M },
            new Furniture { Id = 2, Name = "Sofa", Description = "Leather sofa with 3 seats", Price = 500.00M },
            new Furniture { Id = 3, Name = "Armchair", Description = "Comfortable armchair for living room", Price = 250.00M },
            new Furniture { Id = 4, Name = "Bed", Description = "King size bed with memory foam mattress", Price = 800.00M },
            new Furniture { Id = 5, Name = "Dresser", Description = "Wooden dresser with 6 drawers", Price = 300.00M }
        };
    }
}
