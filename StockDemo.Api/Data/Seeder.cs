using Microsoft.EntityFrameworkCore;
using StockDemo.Api.Models;

namespace StockDemo.Api.Data
{
    public class Seeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<StockDemoDbContext>();
            if (await context.Products.AnyAsync()) return;

            var labels = new[] { "Plaquettes de frein", "Filtre à huile", "Courroie de distribution",
            "Filtre à air", "Amortisseur", "Bougie d'allumage", "Pompe à eau", "Radiateur",
            "Alternateur", "Démarreur" };

            var brands = new[] { "Bosch", "Gates", "Fram", "Monroe", "NGK", "Denso", "ACDelco", "Moog" };

            var vehicles = new[] { "Honda Civic 2019", "Ford F-150 2020", "Toyota Camry 2018",
            "Dodge Ram 2015", "Chevrolet Silverado 2021", "Hyundai Elantra 2020", "Toyota Sienna 2025" };

            List<Product> products = [];

            for (int i = 0; i < 50; i++)
            {
                var label = labels[i % labels.Length];
                var vehicle = vehicles[i % vehicles.Length];
                var brand = brands[i % brands.Length];

                products.Add(new Product
                {
                    Label = $"{label} {vehicle}",
                    Reference = $"REF-{i + 1:D4}",
                    Brand = brand,
                    Price = Math.Round(Random.Shared.NextDouble() * 150 + 10, 2),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });
            }

            var customers = new List<Customer>
            {
                new() { Firstname = "Jean", Lastname = "Tremblay", CompanyName = "Garage Pro Drummondville", Email = "jean.tremblay@garagepro.qc.ca", Phone = "450-555-0192", Address = "123 rue Principale", City = "Drummondville", Province = "QC", PostalCode = "J2B 1A1", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { Firstname = "Mario", Lastname = "Gagnon", CompanyName = "Auto Mario Gagnon", Email = "mario.gagnon@gagnongarage.ca", Phone = "418-555-0147", Address = "456 rue des Érables", City = "Sainte-Marie", Province = "QC", PostalCode = "G6E 2B2", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { Firstname = "Sophie", Lastname = "Bouchard", CompanyName = "Garage Bouchard", Email = "sophie.bouchard@garagebouchard.ca", Phone = "418-555-0178", Address = "789 boul. Guillaume-Couture", City = "Lévis", Province = "QC", PostalCode = "G6V 3C3", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { Firstname = "Michel", Lastname = "Lavoie", CompanyName = "Atelier Lavoie", Email = "michel.lavoie@atelierlavoie.qc.ca", Phone = "514-555-0234", Address = "321 rue Saint-Denis", City = "Montréal", Province = "QC", PostalCode = "H2X 1K1", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { Firstname = "Isabelle", Lastname = "Côté", CompanyName = "Mécano Côté", Email = "isabelle.cote@mecanocote.qc.ca", Phone = "418-555-0312", Address = "654 avenue Cartier", City = "Québec", Province = "QC", PostalCode = "G1R 2S6", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { Firstname = "Patrick", Lastname = "Fortin", CompanyName = "Garage Fortin", Email = "patrick.fortin@garagefortin.qc.ca", Phone = "819-555-0421", Address = "987 rue King Ouest", City = "Sherbrooke", Province = "QC", PostalCode = "J1H 1R5", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { Firstname = "Nathalie", Lastname = "Bergeron", CompanyName = "Auto Bergeron", Email = "nathalie.bergeron@autobergeron.ca", Phone = "450-555-0533", Address = "147 boulevard Taschereau", City = "Longueuil", Province = "QC", PostalCode = "J4J 1M3", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { Firstname = "François", Lastname = "Pelletier", CompanyName = "Mécanique Pelletier", Email = "francois.pelletier@mecapelletier.qc.ca", Phone = "418-555-0644", Address = "258 rue Racine Est", City = "Chicoutimi", Province = "QC", PostalCode = "G7H 1R8", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { Firstname = "Lucie", Lastname = "Morin", CompanyName = "Garage Morin & Fils", Email = "lucie.morin@garagemorinfils.ca", Phone = "819-555-0755", Address = "369 rue Saint-Joseph", City = "Trois-Rivières", Province = "QC", PostalCode = "G9A 2H4", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new() { Firstname = "Alain", Lastname = "Dubois", CompanyName = "Centre Auto Dubois", Email = "alain.dubois@centreautodubois.qc.ca", Phone = "450-555-0866", Address = "741 rue Principale", City = "Saint-Jean-sur-Richelieu", Province = "QC", PostalCode = "J3B 2B5", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            };

            await context.Products.AddRangeAsync(products);
            await context.Customers.AddRangeAsync(customers);


            var warehouses = new List<Warehouse>
            {
                new() {Name = "Montreal Warehouse", City="Montreal", CreatedAt=DateTime.UtcNow,UpdatedAt=DateTime.UtcNow},
                new() {Name = "Québec Warehouse", City="Québec", CreatedAt=DateTime.UtcNow,UpdatedAt=DateTime.UtcNow}
            };

            await context.Warehouses.AddRangeAsync(warehouses);

            await context.SaveChangesAsync();

            var savedWarehouses = await context.Warehouses.ToListAsync();
            var savedProducts = await context.Products.ToListAsync();

            var stocks = new List<Stock>();
            foreach (var product in savedProducts)
            {
                foreach (var warehouse in savedWarehouses)
                {
                    stocks.Add(new Stock
                    {
                        ProductId = product.Id,
                        WarehouseId = warehouse.Id,
                        Quantity = Random.Shared.Next(0, 50),
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    });
                }
            }

            await context.Stocks.AddRangeAsync(stocks);
            await context.SaveChangesAsync();
        }
    }
}