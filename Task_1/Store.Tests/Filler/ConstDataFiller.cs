using System;
using Store.DAL;
using Store.DAL.Model;

namespace Store.Tests.Filler
{
    internal class ConstDataFiller : IDataFiller
    {
        public void Fill(IDataRepository repository)
        {
            var c1 = new Client("Maciej", "Błażewicz", "mblazewicz@o2.pl", "Rawa Mazowiecka");
            var c2 = new Client("Sebastian", "Nawrocki", "snawrocki@interia.pl", "Łódź");
            repository.AddClient(c1);
            repository.AddClient(c2);

            var p1 = new Product(Guid.NewGuid(), "Granat", "Jadalny owoc, najczęściej koloru czerwonego", "Artykuły spożywcze");
            var p2 = new Product(Guid.NewGuid(), "Klawiatura mechaniczna", "Klawiatura z niskimi czasami opóźnień", "Elektronika");
            var p3 = new Product(Guid.NewGuid(), "Granat", "Pocisk rażący odłamkami i energią wybuchu", "Broń");
            repository.AddProduct(p1);
            repository.AddProduct(p2);
            repository.AddProduct(p3);

            var o1 = new Offer(Guid.NewGuid(), p1, 14.50m, 0.05m, 40);
            var o2 = new Offer(Guid.NewGuid(), p2, 450.00m, 0.23m, 5);
            var o3 = new Offer(Guid.NewGuid(), p3, 1500.00m, 0.23m, 2);
            var o4 = new Offer(Guid.NewGuid(), p1, 9.99m, 0.05m, 20);
            repository.AddOffer(o1);
            repository.AddOffer(o2);
            repository.AddOffer(o3);
            repository.AddOffer(o4);

            var e1 = new Facture(Guid.NewGuid(), c2, o1, 5, DateTimeOffset.Now.AddDays(-7));
            var e2 = new Facture(Guid.NewGuid(), c1, o2, 1, DateTimeOffset.Now);
            var e3 = new Facture(Guid.NewGuid(), c1, o4, 3, DateTimeOffset.Now);
            repository.AddEvent(e1);
            repository.AddEvent(e2);
            repository.AddEvent(e3);
        }
    }
}