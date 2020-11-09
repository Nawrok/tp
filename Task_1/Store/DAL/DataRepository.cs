using System;
using System.Collections.Generic;
using System.Linq;
using Store.DAL.Model;

namespace Store.DAL
{
    public class DataRepository : CrudRepository, IDataRepository
    {
        public DataRepository(IDataFiller dataFiller) : base(dataFiller) { }

        public IEnumerable<Facture> GetAllFactures()
        {
            return GetAllEvents().Where(e => e is Facture).Cast<Facture>();
        }

        public IEnumerable<Return> GetAllReturns()
        {
            return GetAllEvents().Where(e => e is Return).Cast<Return>();
        }

        public IEnumerable<Event> GetEventsInTime(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return GetAllEvents().Where(e => e.Date >= startDate && e.Date <= endDate);
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