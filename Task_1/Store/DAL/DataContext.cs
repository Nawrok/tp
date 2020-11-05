using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Store.DAL.Model;

namespace Store.DAL
{
    public class DataContext
    {
        public List<Client> Clients { get; } = new List<Client>();
        public Dictionary<Guid, Product> Products { get; } = new Dictionary<Guid, Product>();
        public List<Offer> Offers { get; } = new List<Offer>();
        public ObservableCollection<Facture> Factures { get; } = new ObservableCollection<Facture>();
    }
}