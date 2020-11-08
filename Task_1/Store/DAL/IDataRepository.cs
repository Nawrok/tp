using System;
using System.Collections.Generic;
using Store.DAL.Model;

namespace Store.DAL
{
    public interface IDataRepository : ICrudRepository
    {
        IEnumerable<Facture> GetAllFactures();
        IEnumerable<Return> GetAllReturns();
        IEnumerable<Event> GetEventsInTime(DateTimeOffset startDate, DateTimeOffset endDate);
        IEnumerable<Client> GetClientsFromCity(string city);
        IEnumerable<Product> GetTheSameTypeProducts(string type);
    }
}