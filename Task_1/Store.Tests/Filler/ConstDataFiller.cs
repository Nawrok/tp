using Store.DAL;
using Store.DAL.Model;
using System;

namespace Store.Tests.Filler
{
    internal class ConstDataFiller : IDataFiller
    {
        public void Fill(IDataRepository repository)
        {
            Client c1 = new Client("Maciej", "Błażewicz", "mblazewicz@o2.pl", "Rawa Mazowiecka");
            Client c2 = new Client("Sebastian", "Nawrocki", "snawrocki@interia.pl", "Łódź");
            repository.AddClient(c1);
            repository.AddClient(c2);

            Product p1 = new Product(Guid.NewGuid(), "Granat", "Jadalny owoc, najczęściej koloru czerwonego", "Artykuły spożywcze");
            Product p2 = new Product(Guid.NewGuid(), "Klawiatura mechaniczna", "Klawiatura z niskimi czasami opóźnień", "Elektronika");
            Product p3 = new Product(Guid.NewGuid(), "Granat", "Pocisk rażący odłamkami i energią wybuchu", "Broń");
            repository.AddProduct(p1);
            repository.AddProduct(p2);
            repository.AddProduct(p3);

            Offer o1 = new Offer(Guid.NewGuid(), p1, 14.50m, 0.05m, 40);
            Offer o2 = new Offer(Guid.NewGuid(), p2, 450.00m, 0.23m, 5);
            Offer o3 = new Offer(Guid.NewGuid(), p3, 1500.00m, 0.23m, 2);
            Offer o4 = new Offer(Guid.NewGuid(), p1, 9.99m, 0.05m, 20);
            repository.AddOffer(o1);
            repository.AddOffer(o2);
            repository.AddOffer(o3);
            repository.AddOffer(o4);

            Facture f1 = new Facture(Guid.NewGuid(), c2, o1, DateTimeOffset.Now.AddDays(-7));
            Facture f2 = new Facture(Guid.NewGuid(), c1, o2, DateTimeOffset.Now);
            Facture f3 = new Facture(Guid.NewGuid(), c1, o4, DateTimeOffset.Now);
            repository.AddFacture(f1);
            repository.AddFacture(f2);
            repository.AddFacture(f3);
        }
    }
}