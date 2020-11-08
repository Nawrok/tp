using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.DAL;
using Store.DAL.Model;
using Store.Tests.Filler;
using System;
using System.IO;
using System.Linq;

namespace Store.Tests
{
    [TestClass]
    public class DataRepositoryTest
    {
        private IDataRepository _dataRepository;
        private const int ClientNumber = 5;
        private const int ProductNumber = 10;
        private const int EventNumber = 15;
        private Client c1;
        private Product p1;
        private Offer o1;
        private Event e1;

        [TestInitialize]
        public void TestInitialize()
        {
            _dataRepository = new DataRepository(new RandomDataFiller(ClientNumber, ProductNumber, EventNumber));
            c1 = new Client("Marcin", "Krasucki", "mkrasucki@gmail.com", "Piotrków Trybunalski");
            p1 = new Product(Guid.NewGuid(), "Kierownica", "Taka do komputera", "Elektronika");
            o1 = new Offer(p1, 300.00m, 0.23m, 2);
            e1 = new Facture(Guid.NewGuid(), c1, o1, 1, DateTimeOffset.Now);
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
            _dataRepository.AddClient(c1);
            Assert.AreEqual(ClientNumber + 1, _dataRepository.GetAllClients().Count());
            Assert.AreEqual("mkrasucki@gmail.com", _dataRepository.GetClient("mkrasucki@gmail.com").Email);
        }

        [TestMethod]
        public void AddClient_Test_AlreadyAddedClient()
        {
            _dataRepository.AddClient(c1);
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.AddClient(c1));
        }

        [TestMethod]
        public void AddEvent_Test_ClientOrOfferNotInRepo()
        {
            Assert.ThrowsException<InvalidDataException>(() => _dataRepository.AddEvent(e1));
        }

        [TestMethod]
        public void AddEvent_Test_Successful()
        {
            _dataRepository.AddClient(c1);
            _dataRepository.AddProduct(p1);
            _dataRepository.AddOffer(o1);
            _dataRepository.AddEvent(e1);
            Assert.AreEqual(EventNumber + 1, _dataRepository.GetAllEvents().Count());
            Assert.AreEqual(e1.Id, _dataRepository.GetEvent(e1.Id).Id);
        }

        [TestMethod]
        public void AddEvent_Test_SuccessfulFacture()
        {
            AddEvent_Test_Successful();
            Assert.AreEqual(2 * EventNumber / 3 + 1, _dataRepository.GetAllEvents().Where(e => e is Facture).Count());
        }

        [TestMethod]
        public void AddEvent_Test_SuccessfulReturn()
        {
            AddEvent_Test_Successful();
            var r1 = new Return(e1 as Facture, DateTimeOffset.Now);
            _dataRepository.UpdateEvent(r1.Id, r1);
            Assert.AreEqual(r1.Id, _dataRepository.GetEvent(r1.Id).Id);
            Assert.AreEqual(EventNumber / 3 + 1, _dataRepository.GetAllEvents().Where(e => e is Return).Count());
        }

        [TestMethod]
        public void AddEvent_Test_AlreadyAddedEvent()
        {
            _dataRepository.AddClient(c1);
            _dataRepository.AddProduct(p1);
            _dataRepository.AddOffer(o1);
            _dataRepository.AddEvent(e1);
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.AddEvent(e1));
        }

        [TestMethod]
        public void AddEvent_Test_ReturnDateIsOlderThanPurchaseDate()
        {
            _dataRepository.AddClient(c1);
            _dataRepository.AddProduct(p1);
            _dataRepository.AddOffer(o1);
            _dataRepository.AddEvent(e1);
            var r1 = new Return(e1 as Facture, DateTimeOffset.Now.AddDays(-7));
            Assert.ThrowsException<InvalidDataException>(() => _dataRepository.UpdateEvent(r1.Id, r1));
        }

        [TestMethod]
        public void AddOffer_Test_ProductNotInRepo()
        {
            Assert.ThrowsException<InvalidDataException>(() => _dataRepository.AddOffer(o1));
        }

        [TestMethod]
        public void AddOffer_Test_Successful()
        {
            _dataRepository.AddProduct(p1);
            _dataRepository.AddOffer(o1);
            Assert.AreEqual(ProductNumber + 1, _dataRepository.GetAllOffers().Count());
            Assert.AreEqual(p1.Id, _dataRepository.GetOffer(o1.Product.Id).Product.Id);
        }

        [TestMethod]
        public void AddOffer_Test_AlreadyAddedOffer()
        {
            _dataRepository.AddProduct(p1);
            _dataRepository.AddOffer(o1);
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.AddOffer(o1));
        }

        [TestMethod]
        public void AddProduct_Test_Successful()
        {
            _dataRepository.AddProduct(p1);
            Assert.AreEqual(ProductNumber + 1, _dataRepository.GetAllProducts().Count());
            Assert.AreEqual(p1.Id, _dataRepository.GetProduct(p1.Id).Id);
        }

        [TestMethod]
        public void AddProduct_Test_AlreadyAddedProduct()
        {
            _dataRepository.AddProduct(p1);
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.AddProduct(p1));
        }

        [TestMethod]
        public void DeleteClient_Test_ClientNotInRepo()
        {
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.DeleteClient(c1));
        }

        [TestMethod]
        public void DeleteClient_Test_Successful()
        {
            AddClient_Test_Successful();
            _dataRepository.DeleteClient(c1);
            Assert.AreEqual(ClientNumber, _dataRepository.GetAllClients().Count());
            Assert.AreNotEqual("mkrasucki@gmail.com", _dataRepository.GetAllClients().Any(c => c.Email.Equals("mkrasucki@gmail.com")));
        }

        [TestMethod]
        public void DeleteEvent_Test_EventNotInRepo()
        {
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.DeleteEvent(e1));
        }

        [TestMethod]
        public void DeleteEvent_Test_ClientOrOfferStillInRepo()
        {
            AddEvent_Test_SuccessfulFacture();
            Assert.ThrowsException<InvalidDataException>(() => _dataRepository.DeleteEvent(e1));
        }

        /*TO-DO make test pass (it should be assert.inconclusive with professional approach),
        Delete IsReferringToEvent from DeleteOffer*/
        [TestMethod]
        public void DeleteEvent_Test_Successful()
        {
            AddEvent_Test_SuccessfulFacture();
            _dataRepository.DeleteClient(c1);
            _dataRepository.DeleteProduct(o1.Product);
            _dataRepository.DeleteOffer(o1);
            _dataRepository.DeleteEvent(e1);
            Assert.AreEqual(EventNumber, _dataRepository.GetAllEvents().Count());
            Assert.AreNotEqual(e1.Id, _dataRepository.GetAllEvents().Any(e => e.Id.Equals(e1.Id)));
        }

        [TestMethod]
        public void DeleteOffer_Test_ProductNotInRepo()
        {
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.DeleteOffer(o1));
        }

        [TestMethod]
        public void DeleteOffer_Test_OfferNotInRepo()
        {
            AddProduct_Test_Successful();
            Assert.ThrowsException<InvalidDataException>(() => _dataRepository.DeleteOffer(o1));
        }

        [TestMethod]
        public void DeleteOffer_Test_ProductStillInRepo()
        {
            AddProduct_Test_Successful();
            _dataRepository.AddOffer(o1);
            Assert.ThrowsException<InvalidDataException>(() => _dataRepository.DeleteOffer(o1));
        }

        /*TO-DO make test pass (it should be assert.inconclusive with professional approach)
         Delete IsReferringToOffer in DeleteProduct*/
        [TestMethod]
        public void DeleteOffer_Test_Successful()
        {
            AddOffer_Test_Successful();
            _dataRepository.DeleteProduct(p1);
            _dataRepository.DeleteOffer(o1);
            Assert.AreEqual(ProductNumber, _dataRepository.GetAllOffers().Count());
            Assert.AreNotEqual(p1.Id, _dataRepository.GetAllOffers().Any(o => o.Product.Id.Equals(p1.Id)));
        }

        [TestMethod]
        public void DeleteProduct_Test_ProductNotInRepo()
        {
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.DeleteProduct(p1));
        }

        [TestMethod]
        public void DeleteProduct_Test_Successful()
        {
            AddProduct_Test_Successful();
            _dataRepository.DeleteProduct(p1);
            Assert.AreEqual(ProductNumber, _dataRepository.GetAllProducts().Count());
            Assert.AreNotEqual(p1.Id, _dataRepository.GetAllProducts().Any(p => p.Id.Equals(p1.Id)));
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
        public void GetClient_Test()
        {
            AddClient_Test_Successful();
            Assert.AreEqual("mkrasucki@gmail.com", _dataRepository.GetClient("mkrasucki@gmail.com").Email);
        }

        [TestMethod]
        public void GetEvent_Test()
        {
            AddEvent_Test_Successful();
            Assert.AreEqual(e1.Id, _dataRepository.GetEvent(e1.Id).Id);
        }

        [TestMethod]
        public void GetOffer_Test()
        {
            AddOffer_Test_Successful();
            Assert.AreEqual(p1.Id, _dataRepository.GetOffer(o1.Product.Id).Product.Id);
        }

        [TestMethod]
        public void GetProduct_Test()
        {
            AddProduct_Test_Successful();
            Assert.AreEqual(p1.Id, _dataRepository.GetProduct(p1.Id).Id);
        }

        [TestMethod]
        public void UpdateClient_Test_ClientNotInRepo()
        {
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateClient("mkrasucki@gmail.com", c1));
        }

        [TestMethod]
        public void UpdateClient_Test_Successful()
        {
            AddClient_Test_Successful();
            _dataRepository.UpdateClient("mkrasucki@gmail.com", c1);
        }

        [TestMethod]
        public void UpdateClient_Test_WrongEmail()
        {
            AddClient_Test_Successful();
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateClient("mkrasucki@gmail.pl", c1));
        }

        [TestMethod]
        public void UpdateClient_Test_WrongClient()
        {
            AddClient_Test_Successful();
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateClient("mkrasucki@gmail.com", _dataRepository.GetAllClients().First()));
        }

        [TestMethod]
        public void UpdateEvent_Test_ClientOrOfferNotInRepo()
        {
            Assert.ThrowsException<InvalidDataException>(() =>_dataRepository.UpdateEvent(e1.Id, e1));
        }

        [TestMethod]
        public void UpdateEvent_Test_EventNotInRepo()
        {
            AddClient_Test_Successful();
            AddOffer_Test_Successful();
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateEvent(e1.Id, e1));
        }

        [TestMethod]
        public void UpdateEvent_Test_Successful()
        {
            AddEvent_Test_Successful();
            _dataRepository.UpdateEvent(e1.Id, e1);
        }

        [TestMethod]
        public void UpdateEvent_Test_WrongId()
        {
            AddEvent_Test_Successful();
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateEvent(Guid.NewGuid(), e1));
        }

        [TestMethod]
        public void UpdateEvent_Test_WrongEvent()
        {
            AddEvent_Test_Successful();
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateEvent(e1.Id, _dataRepository.GetAllEvents().First()));
        }

        [TestMethod]
        public void UpdateOffer_Test_ProductNotInRepo()
        {
            Assert.ThrowsException<InvalidDataException>(() => _dataRepository.UpdateOffer(o1.Product.Id, o1));
        }

        [TestMethod]
        public void UpdateOffer_Test_OfferNotInRepo()
        {
            AddProduct_Test_Successful();
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateOffer(o1.Product.Id, o1));
        }

        [TestMethod]
        public void UpdateOffer_Test_Successful()
        {
            AddOffer_Test_Successful();
            _dataRepository.UpdateOffer(o1.Product.Id, o1);
        }

        [TestMethod]
        public void UpdateOffer_Test_WrongId()
        {
            AddOffer_Test_Successful();
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateOffer(Guid.NewGuid(), o1));
        }

        [TestMethod]
        public void UpdateOffer_Test_WrongOffer()
        {
            AddOffer_Test_Successful();
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateOffer(o1.Product.Id, _dataRepository.GetAllOffers().First()));
        }

        [TestMethod]
        public void UpdateProduct_Test_ProductNotInRepo()
        {
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateProduct(p1.Id, p1));
        }

        [TestMethod]
        public void UpdateProduct_Test_Successful()
        {
            AddProduct_Test_Successful();
            _dataRepository.UpdateProduct(p1.Id, p1);
        }

        [TestMethod]
        public void UpdateProduct_Test_WrongId()
        {
            AddProduct_Test_Successful();
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateProduct(Guid.NewGuid(), p1));
        }

        [TestMethod]
        public void UpdateProduct_Test_WrongProduct()
        {
            AddProduct_Test_Successful();
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateProduct(p1.Id, _dataRepository.GetAllProducts().First()));
        }

        [TestMethod]
        public void GetFactures_Test()
        {
            Assert.AreEqual(2 * EventNumber / 3, _dataRepository.GetAllEvents().Where(e => e is Facture).Count());
        }

        [TestMethod]
        public void GetReturns_Test()
        {
            Assert.AreEqual(EventNumber / 3, _dataRepository.GetAllEvents().Where(e => e is Return).Count());
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
            Assert.AreEqual(0, _dataRepository.GetEventsInTime(DateTimeOffset.Now.AddDays(-7), DateTimeOffset.Now).Count());
            AddEvent_Test_Successful();
            Assert.AreEqual(1, _dataRepository.GetEventsInTime(DateTimeOffset.Now.AddDays(-7), DateTimeOffset.Now).Count());
            var c = new Client("Norbert", "Gierczak", "krzycz@disunio.pl", "Katowice");
            var p = new Product(Guid.NewGuid(), "Topór", "Broń ostra, można nią rzucać", "Broń");
            var o = new Offer(p, 450.00m, 0.23m, 2);
            var e = new Facture(Guid.NewGuid(), c, o, 1, DateTimeOffset.Now.AddDays(-2));
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
            Assert.AreEqual(0, _dataRepository.GetClientsFromCity("Łódź").Count());
            Assert.AreEqual(0, _dataRepository.GetClientsFromCity("Piotrków Trybunalski").Count());
            AddClient_Test_Successful();
            Assert.AreEqual(1, _dataRepository.GetClientsFromCity("Piotrków Trybunalski").Count());
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
            Assert.AreEqual(0, _dataRepository.GetTheSameTypeProducts("Artykuły spożywcze").Count());
            Assert.AreEqual(0, _dataRepository.GetTheSameTypeProducts("Elektronika").Count());
            AddProduct_Test_Successful();
            Assert.AreEqual(1, _dataRepository.GetTheSameTypeProducts("Elektronika").Count());
            var p = new Product(Guid.NewGuid(), "Xiaomi Redmi Note 7", "Telefon marki Xiaomi", "Elektronika");
            _dataRepository.AddProduct(p);
            Assert.AreEqual(2, _dataRepository.GetTheSameTypeProducts("Elektronika").Count());
        }
    }
}