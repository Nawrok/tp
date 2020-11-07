using System;
using Store.DAL;
using Store.DAL.Model;

namespace Store.Tests.Filler
{
    internal class ConstDataFiller : IDataFiller
    {
        public void Fill(ICrudRepository repository)
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

            var o1 = new Offer(p1, 14.50m, 0.05m, 40);
            var o2 = new Offer(p2, 450.00m, 0.23m, 5);
            var o3 = new Offer(p3, 1500.00m, 0.23m, 2);
            repository.AddOffer(o1);
            repository.AddOffer(o2);
            repository.AddOffer(o3);

            var f1 = new Facture(Guid.NewGuid(), c2, o1, 5, DateTimeOffset.Now.AddDays(-7));
            var f2 = new Facture(Guid.NewGuid(), c1, o2, 1, DateTimeOffset.Now);
            var f3 = new Facture(Guid.NewGuid(), c1, o3, 3, DateTimeOffset.Now);
            var f4 = new Facture(Guid.NewGuid(), c1, o1, 5, DateTimeOffset.Now);
            //var r1 = new Return(f1, DateTimeOffset.Now);
            repository.AddEvent(f1);
            repository.AddEvent(f2);
            repository.AddEvent(f3);
            repository.AddEvent(f4);
            //repository.AddEvent(r1);
        }
    }
}