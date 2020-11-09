using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.DAL;
using Store.DAL.Model;
using Store.Tests.Filler;

namespace Store.Tests
{
    [TestClass]
    public class DataRepositoryTest
    {
        private const int ClientNumber = 5;
        private const int ProductNumber = 10;
        private const int EventNumber = 15;
        private Client _client;
        private IDataRepository _dataRepository;
        private Facture _facture;
        private Offer _offer;
        private Product _product;

        [TestInitialize]
        public void TestInitialize()
        {
            _dataRepository = new DataRepository(new RandomDataFiller(ClientNumber, ProductNumber, EventNumber));
            _client = new Client("Marcin", "Krasucki", "mkrasucki@gmail.com", "Piotrków Trybunalski");
            _product = new Product(Guid.NewGuid(), "Kierownica", "Taka do komputera", "Elektronika");
            _offer = new Offer(_product, 300.00m, 0.23m, 2);
            _facture = new Facture(Guid.NewGuid(), _client, _offer, DateTimeOffset.Now, 1);
        }

        [TestMethod]
        public void Init_Test()
        {
            Assert.AreEqual(ClientNumber, _dataRepository.GetAllClients().Count());
            Assert.AreEqual(ProductNumber, _dataRepository.GetAllProducts().Count());
            Assert.AreEqual(ProductNumber, _dataRepository.GetAllOffers().Count());
            Assert.AreEqual(EventNumber, _dataRepository.GetAllEvents().Count());
        }

        [TestMethod]
        public void AddClient_Test_Successful()
        {
            _dataRepository.AddClient(_client);
            Assert.AreEqual(ClientNumber + 1, _dataRepository.GetAllClients().Count());
            Assert.AreEqual("mkrasucki@gmail.com", _dataRepository.GetClient("mkrasucki@gmail.com").Email);
        }

        [TestMethod]
        public void AddClient_Test_AlreadyAddedClient()
        {
            _dataRepository.AddClient(_client);
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.AddClient(_client));
        }

        [TestMethod]
        public void AddEvent_Test_ClientOrOfferNotInRepo()
        {
            Assert.ThrowsException<InvalidDataException>(() => _dataRepository.AddEvent(_facture));
        }

        [TestMethod]
        public void AddEvent_Test_Successful()
        {
            _dataRepository.AddClient(_client);
            _dataRepository.AddProduct(_product);
            _dataRepository.AddOffer(_offer);
            _dataRepository.AddEvent(_facture);
            Assert.AreEqual(EventNumber + 1, _dataRepository.GetAllEvents().Count());
            Assert.AreEqual(_facture.Id, _dataRepository.GetEvent(_facture.Id).Id);
        }

        [TestMethod]
        public void AddEvent_Test_SuccessfulFacture()
        {
            AddEvent_Test_Successful();
            Assert.AreEqual(2 * EventNumber / 3 + 1, _dataRepository.GetAllFactures().Count());
        }

        [TestMethod]
        public void AddEvent_Test_SuccessfulReturn()
        {
            AddEvent_Test_Successful();
            var r1 = new Return(Guid.NewGuid(), _facture, DateTimeOffset.Now, _facture.BoughtProducts);
            _dataRepository.AddEvent(r1);
            Assert.AreEqual(r1.Id, _dataRepository.GetEvent(r1.Id).Id);
            Assert.AreEqual(EventNumber / 3 + 1, _dataRepository.GetAllReturns().Count());
        }

        [TestMethod]
        public void AddEvent_Test_AlreadyAddedEvent()
        {
            _dataRepository.AddClient(_client);
            _dataRepository.AddProduct(_product);
            _dataRepository.AddOffer(_offer);
            _dataRepository.AddEvent(_facture);
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.AddEvent(_facture));
        }

        [TestMethod]
        public void AddOffer_Test_ProductNotInRepo()
        {
            Assert.ThrowsException<InvalidDataException>(() => _dataRepository.AddOffer(_offer));
        }

        [TestMethod]
        public void AddOffer_Test_Successful()
        {
            _dataRepository.AddProduct(_product);
            _dataRepository.AddOffer(_offer);
            Assert.AreEqual(ProductNumber + 1, _dataRepository.GetAllOffers().Count());
            Assert.AreEqual(_product.Id, _dataRepository.GetOffer(_offer.Product.Id).Product.Id);
        }

        [TestMethod]
        public void AddOffer_Test_AlreadyAddedOffer()
        {
            _dataRepository.AddProduct(_product);
            _dataRepository.AddOffer(_offer);
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.AddOffer(_offer));
        }

        [TestMethod]
        public void AddProduct_Test_Successful()
        {
            _dataRepository.AddProduct(_product);
            Assert.AreEqual(ProductNumber + 1, _dataRepository.GetAllProducts().Count());
            Assert.AreEqual(_product.Id, _dataRepository.GetProduct(_product.Id).Id);
        }

        [TestMethod]
        public void AddProduct_Test_AlreadyAddedProduct()
        {
            _dataRepository.AddProduct(_product);
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.AddProduct(_product));
        }

        [TestMethod]
        public void DeleteClient_Test_ClientNotInRepo()
        {
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.DeleteClient(_client));
        }

        [TestMethod]
        public void DeleteClient_Test_Successful()
        {
            AddClient_Test_Successful();
            _dataRepository.DeleteClient(_client);
            Assert.AreEqual(ClientNumber, _dataRepository.GetAllClients().Count());
            Assert.AreNotEqual("mkrasucki@gmail.com", _dataRepository.GetAllClients().Any(c => c.Email.Equals("mkrasucki@gmail.com")));
        }

        [TestMethod]
        public void DeleteEvent_Test_EventNotInRepo()
        {
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.DeleteEvent(_facture));
        }

        [TestMethod]
        public void DeleteEvent_Test_Successful()
        {
            AddEvent_Test_SuccessfulFacture();
            _dataRepository.DeleteEvent(_facture);
            Assert.AreEqual(EventNumber, _dataRepository.GetAllEvents().Count());
            Assert.AreNotEqual(_facture.Id, _dataRepository.GetAllEvents().Any(e => e.Id.Equals(_facture.Id)));
        }

        [TestMethod]
        public void DeleteOffer_Test_ProductNotInRepo()
        {
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.DeleteOffer(_offer));
        }

        [TestMethod]
        public void DeleteOffer_Test_OfferNotInRepo()
        {
            AddProduct_Test_Successful();
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.DeleteOffer(_offer));
        }

        [TestMethod]
        public void DeleteOffer_Test_Successful()
        {
            AddOffer_Test_Successful();
            _dataRepository.DeleteOffer(_offer);
            Assert.AreEqual(ProductNumber, _dataRepository.GetAllOffers().Count());
            Assert.AreNotEqual(_product.Id, _dataRepository.GetAllOffers().Any(o => o.Product.Id.Equals(_product.Id)));
        }

        [TestMethod]
        public void DeleteProduct_Test_ProductNotInRepo()
        {
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.DeleteProduct(_product));
        }

        [TestMethod]
        public void DeleteProduct_Test_OfferInRepo()
        {
            AddOffer_Test_Successful();
            Assert.ThrowsException<InvalidDataException>(() => _dataRepository.DeleteProduct(_product));
        }

        [TestMethod]
        public void DeleteProduct_Test_Successful()
        {
            AddProduct_Test_Successful();
            _dataRepository.DeleteProduct(_product);
            Assert.AreEqual(ProductNumber, _dataRepository.GetAllProducts().Count());
            Assert.AreNotEqual(_product.Id, _dataRepository.GetAllProducts().Any(p => p.Id.Equals(_product.Id)));
        }

        [TestMethod]
        public void GetAllClients_Test()
        {
            Assert.AreEqual(ClientNumber, _dataRepository.GetAllClients().Count());
        }

        [TestMethod]
        public void GetAllEvents_Test()
        {
            Assert.AreEqual(EventNumber, _dataRepository.GetAllEvents().Count());
        }

        [TestMethod]
        public void GetAllOffers_Test()
        {
            Assert.AreEqual(ProductNumber, _dataRepository.GetAllOffers().Count());
        }

        [TestMethod]
        public void GetAllProducts_Test()
        {
            Assert.AreEqual(ProductNumber, _dataRepository.GetAllProducts().Count());
        }

        [TestMethod]
        public void GetClient_Test_Successful()
        {
            AddClient_Test_Successful();
            Assert.AreEqual("mkrasucki@gmail.com", _dataRepository.GetClient("mkrasucki@gmail.com").Email);
        }

        [TestMethod]
        public void GetClient_Test_ClientNotInRepo()
        {
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.GetClient(_client.Email));
        }

        [TestMethod]
        public void GetEvent_Test_Successful()
        {
            AddEvent_Test_Successful();
            Assert.AreEqual(_facture.Id, _dataRepository.GetEvent(_facture.Id).Id);
        }

        [TestMethod]
        public void GetEvent_Test_EventNotInRepo()
        {
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.GetEvent(_facture.Id));
        }

        [TestMethod]
        public void GetOffer_Test_Successful()
        {
            AddOffer_Test_Successful();
            Assert.AreEqual(_product.Id, _dataRepository.GetOffer(_offer.Product.Id).Product.Id);
        }

        [TestMethod]
        public void GetOffer_Test_OfferNotInRepo()
        {
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.GetOffer(_offer.Product.Id));
        }

        [TestMethod]
        public void GetProduct_Test_Successful()
        {
            AddProduct_Test_Successful();
            Assert.AreEqual(_product.Id, _dataRepository.GetProduct(_product.Id).Id);
        }

        [TestMethod]
        public void GetProduct_Test_ProductNotInRepo()
        {
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.GetProduct(_product.Id));
        }

        [TestMethod]
        public void UpdateClient_Test_ClientNotInRepo()
        {
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateClient("mkrasucki@gmail.com", _client));
        }

        [TestMethod]
        public void UpdateClient_Test_Successful()
        {
            AddClient_Test_Successful();
            _dataRepository.UpdateClient("mkrasucki@gmail.com", _client);
        }

        [TestMethod]
        public void UpdateClient_Test_InvalidEmail()
        {
            AddClient_Test_Successful();
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateClient("mkrasucki@gmail.pl", _client));
        }

        [TestMethod]
        public void UpdateClient_Test_InvalidClient()
        {
            AddClient_Test_Successful();
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateClient("mkrasucki@gmail.com", _dataRepository.GetAllClients().First()));
        }

        [TestMethod]
        public void UpdateEvent_Test_ClientOrOfferNotInRepo()
        {
            Assert.ThrowsException<InvalidDataException>(() => _dataRepository.UpdateEvent(_facture.Id, _facture));
        }

        [TestMethod]
        public void UpdateEvent_Test_EventNotInRepo()
        {
            AddClient_Test_Successful();
            AddOffer_Test_Successful();
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateEvent(_facture.Id, _facture));
        }

        [TestMethod]
        public void UpdateEvent_Test_Successful()
        {
            AddEvent_Test_Successful();
            _dataRepository.UpdateEvent(_facture.Id, _facture);
        }

        [TestMethod]
        public void UpdateEvent_Test_InvalidId()
        {
            AddEvent_Test_Successful();
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateEvent(Guid.NewGuid(), _facture));
        }

        [TestMethod]
        public void UpdateEvent_Test_InvalidEvent()
        {
            AddEvent_Test_Successful();
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateEvent(_facture.Id, _dataRepository.GetAllEvents().First()));
        }

        [TestMethod]
        public void UpdateOffer_Test_ProductNotInRepo()
        {
            Assert.ThrowsException<InvalidDataException>(() => _dataRepository.UpdateOffer(_offer.Product.Id, _offer));
        }

        [TestMethod]
        public void UpdateOffer_Test_OfferNotInRepo()
        {
            AddProduct_Test_Successful();
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateOffer(_offer.Product.Id, _offer));
        }

        [TestMethod]
        public void UpdateOffer_Test_Successful()
        {
            AddOffer_Test_Successful();
            _dataRepository.UpdateOffer(_offer.Product.Id, _offer);
        }

        [TestMethod]
        public void UpdateOffer_Test_InvalidId()
        {
            AddOffer_Test_Successful();
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateOffer(Guid.NewGuid(), _offer));
        }

        [TestMethod]
        public void UpdateOffer_Test_InvalidOffer()
        {
            AddOffer_Test_Successful();
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateOffer(_offer.Product.Id, _dataRepository.GetAllOffers().First()));
        }

        [TestMethod]
        public void UpdateProduct_Test_ProductNotInRepo()
        {
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateProduct(_product.Id, _product));
        }

        [TestMethod]
        public void UpdateProduct_Test_Successful()
        {
            AddProduct_Test_Successful();
            _dataRepository.UpdateProduct(_product.Id, _product);
        }

        [TestMethod]
        public void UpdateProduct_Test_InvalidId()
        {
            AddProduct_Test_Successful();
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateProduct(Guid.NewGuid(), _product));
        }

        [TestMethod]
        public void UpdateProduct_Test_InvalidProduct()
        {
            AddProduct_Test_Successful();
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateProduct(_product.Id, _dataRepository.GetAllProducts().First()));
        }

        [TestMethod]
        public void GetFactures_Test()
        {
            Assert.AreEqual(2 * EventNumber / 3, _dataRepository.GetAllFactures().Count());
        }

        [TestMethod]
        public void GetReturns_Test()
        {
            Assert.AreEqual(EventNumber / 3, _dataRepository.GetAllReturns().Count());
        }

        [TestMethod]
        public void GetEventsInTime_Test()
        {
            Assert.AreEqual(0, _dataRepository.GetEventsInTime(DateTimeOffset.Now.AddDays(-7), DateTimeOffset.Now).Count());
            AddEvent_Test_Successful();
            Assert.AreEqual(1, _dataRepository.GetEventsInTime(DateTimeOffset.Now.AddDays(-7), DateTimeOffset.Now).Count());
        }

        [TestMethod]
        public void GetEventsInTime_Test_WithNewEvent()
        {
            GetEventsInTime_Test();
            var c = new Client("Norbert", "Gierczak", "krzycz@disunio.pl", "Katowice");
            var p = new Product(Guid.NewGuid(), "Topór", "Broń ostra, można nią rzucać", "Broń");
            var o = new Offer(p, 450.00m, 0.23m, 2);
            var e = new Facture(Guid.NewGuid(), c, o, DateTimeOffset.Now.AddDays(-2), 1);
            _dataRepository.AddClient(c);
            _dataRepository.AddProduct(p);
            _dataRepository.AddOffer(o);
            _dataRepository.AddEvent(e);
            Assert.AreEqual(2, _dataRepository.GetEventsInTime(DateTimeOffset.Now.AddDays(-7), DateTimeOffset.Now).Count());
        }

        [TestMethod]
        public void GetClientsFromCity_Test()
        {
            Assert.AreEqual(0, _dataRepository.GetClientsFromCity("Łódź").Count());
            Assert.AreEqual(0, _dataRepository.GetClientsFromCity("Piotrków Trybunalski").Count());
            AddClient_Test_Successful();
            Assert.AreEqual(1, _dataRepository.GetClientsFromCity("Piotrków Trybunalski").Count());
        }

        [TestMethod]
        public void GetClientsFromCity_Test_WithNewClient()
        {
            GetClientsFromCity_Test();
            var c = new Client("Artur", "Krasucki", "artuuur@interia.pl", "Piotrków Trybunalski");
            _dataRepository.AddClient(c);
            Assert.AreEqual(2, _dataRepository.GetClientsFromCity("Piotrków Trybunalski").Count());
        }

        [TestMethod]
        public void GetTheSameTypeProducts_Test()
        {
            Assert.AreEqual(0, _dataRepository.GetTheSameTypeProducts("Artykuły spożywcze").Count());
            Assert.AreEqual(0, _dataRepository.GetTheSameTypeProducts("Elektronika").Count());
            AddProduct_Test_Successful();
            Assert.AreEqual(1, _dataRepository.GetTheSameTypeProducts("Elektronika").Count());
        }

        [TestMethod]
        public void GetTheSameTypeProducts_Test_WithNewProduct()
        {
            GetTheSameTypeProducts_Test();
            var p = new Product(Guid.NewGuid(), "Xiaomi Redmi Note 7", "Telefon marki Xiaomi", "Elektronika");
            _dataRepository.AddProduct(p);
            Assert.AreEqual(2, _dataRepository.GetTheSameTypeProducts("Elektronika").Count());
        }
    }
}