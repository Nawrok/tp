using System;
using System.Linq;
using Store.DAL;
using Store.DAL.Model;

namespace Store.Tests.Filler
{
    internal class RandomDataFiller : IDataFiller
    {
        private static readonly Random Random = new Random();
        private readonly int _clientNumber;
        private readonly int _eventNumber;
        private readonly int _productNumber;

        public RandomDataFiller(int clientNumber, int productNumber, int eventNumber)
        {
            _clientNumber = clientNumber;
            _productNumber = productNumber;
            _eventNumber = eventNumber;
        }

        public void Fill(ICrudRepository repository)
        {
            for (var i = 0; i < _clientNumber; i++)
            {
                var client = new Client(
                    GetRandStr(6),
                    GetRandStr(8),
                    $"{GetRandStr(5)}.{i}@{GetRandStr(2)}.com",
                    GetRandStr(6));
                repository.AddClient(client);
            }

            for (var i = 0; i < _productNumber; i++)
            {
                var product = new Product(
                    Guid.NewGuid(),
                    $"Product_{i}",
                    GetRandStr(10),
                    GetRandStr(5));
                repository.AddProduct(product);

                var offer = new Offer(
                    product,
                    new decimal(Random.NextDouble()) * 21.37m,
                    new decimal(Random.NextDouble()),
                    Random.Next(5, 100));
                repository.AddOffer(offer);
            }

            for (var i = 0; i < 2 * _eventNumber / 3; i++)
            {
                var facture = new Facture(
                    Guid.NewGuid(),
                    repository.GetAllClients().ToArray()[Random.Next(0, _clientNumber)],
                    repository.GetAllOffers().ToArray()[Random.Next(0, _productNumber)],
                    DateTimeOffset.Now.AddDays(Random.Next(-360, -30)).AddHours(Random.Next(-12, 12)),
                    Random.Next(1, 20));
                repository.AddEvent(facture);
            }

            for (var i = 0; i < _eventNumber / 3; i++)
            {
                var factures = repository.GetAllEvents().Where(e => e is Facture).Cast<Facture>().ToArray();
                var f = factures[Random.Next(0, factures.Length)];
                var returned = new Return(Guid.NewGuid(), f, f.Date.AddDays(Random.Next(1, 30)), Random.Next(1, f.BoughtProducts + 1));
                repository.AddEvent(returned);
            }
        }

        private static string GetRandStr(int len)
        {
            const string chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz";
            return new string(Enumerable.Repeat(chars, len)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}