using System;
using System.Collections.Generic;
using System.Linq;
using Store.DAL.Model;

namespace Store.DAL
{
    public class DataRepository : CrudRepository, IDataRepository
    {
        public DataRepository(IDataFiller dataFiller) : base(dataFiller) { }

        public IEnumerable<Facture> GetFactures()
        {
            return GetAllEvents().Where(e => e.GetType().IsInstanceOfType(typeof(Facture))).Cast<Facture>();
        }

        public IEnumerable<Return> GetReturns()
        {
            return GetAllEvents().Where(e => e.GetType().IsInstanceOfType(typeof(Return))).Cast<Return>();
        }

        public IEnumerable<Event> GetEventsInTime(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return GetAllEvents().Where(f => f.PurchaseDate >= startDate && f.PurchaseDate <= endDate);
        }

        public IEnumerable<Client> GetClientsFromCity(string city)
        {
            return GetAllClients().Where(c => c.City.Equals(city));
        }

        public IEnumerable<Product> GetTheSameTypeProducts(string type)
        {
            return GetAllProducts().Where(p => p.Type.Equals(type));
        }
    }
}