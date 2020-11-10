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
            Assert.AreSame(_client, _dataRepository.GetClient(_client.Email));
        }

        [TestMethod]
        public void AddClient_Test_AlreadyAddedClient()
        {
            _dataRepository.AddClient(_client);
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.AddClient(_client));
        }


        [TestMethod]
        public void AddProduct_Test_Successful()
        {
            _dataRepository.AddProduct(_product);
            Assert.AreEqual(ProductNumber + 1, _dataRepository.GetAllProducts().Count());
            Assert.AreSame(_product, _dataRepository.GetProduct(_product.Id));
        }

        [TestMethod]
        public void AddProduct_Test_AlreadyAddedProduct()
        {
            _dataRepository.AddProduct(_product);
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.AddProduct(_product));
        }

        [TestMethod]
        public void AddOffer_Test_ProductNotInRepo()
        {
            Assert.ThrowsException<InvalidDataException>(() => _dataRepository.AddOffer(_offer));
        }

        [TestMethod]
        public void AddOffer_Test_Successful()
        {
            AddProduct_Test_Successful();
            _dataRepository.AddOffer(_offer);
            Assert.AreEqual(ProductNumber + 1, _dataRepository.GetAllOffers().Count());
            Assert.AreSame(_offer, _dataRepository.GetOffer(_offer.Product.Id));
        }

        [TestMethod]
        public void AddOffer_Test_AlreadyAddedOffer()
        {
            AddProduct_Test_Successful();
            _dataRepository.AddOffer(_offer);
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.AddOffer(_offer));
        }

        [TestMethod]
        public void AddEvent_Test_Successful()
        {
            AddClient_Test_Successful();
            AddOffer_Test_Successful();
            _dataRepository.AddEvent(_facture);
            Assert.AreEqual(EventNumber + 1, _dataRepository.GetAllEvents().Count());
            Assert.AreSame(_facture, _dataRepository.GetEvent(_facture.Id));
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
            AddEvent_Test_SuccessfulFacture();
            var returned = new Return(Guid.NewGuid(), _facture, DateTimeOffset.Now, _facture.BoughtProducts);
            _dataRepository.AddEvent(returned);
            Assert.AreSame(returned, _dataRepository.GetEvent(returned.Id));
            Assert.AreEqual(EventNumber / 3 + 1, _dataRepository.GetAllReturns().Count());
        }

        [TestMethod]
        public void AddEvent_Test_AlreadyAddedEvent()
        {
            AddEvent_Test_Successful();
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.AddEvent(_facture));
        }

        [TestMethod]
        public void AddEvent_Test_ClientOrOfferNotInRepo()
        {
            Assert.ThrowsException<InvalidDataException>(() => _dataRepository.AddEvent(_facture));
        }

        [TestMethod]
        public void DeleteClient_Test_Successful()
        {
            AddClient_Test_Successful();
            _dataRepository.DeleteClient(_client.Email);
            Assert.AreEqual(ClientNumber, _dataRepository.GetAllClients().Count());
            Assert.IsFalse(_dataRepository.GetAllClients().Any(c => c.Email.Equals(_client.Email)));
        }

        [TestMethod]
        public void DeleteClient_Test_ClientNotInRepo()
        {
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.DeleteClient(_client.Email));
        }

        [TestMethod]
        public void DeleteEvent_Test_Successful()
        {
            AddEvent_Test_SuccessfulFacture();
            _dataRepository.DeleteEvent(_facture.Id);
            Assert.AreEqual(EventNumber, _dataRepository.GetAllEvents().Count());
            Assert.IsFalse(_dataRepository.GetAllEvents().Any(e => e.Id.Equals(_facture.Id)));
        }

        [TestMethod]
        public void DeleteEvent_Test_EventNotInRepo()
        {
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.DeleteEvent(_facture.Id));
        }

        [TestMethod]
        public void DeleteOffer_Test_Successful()
        {
            AddOffer_Test_Successful();
            _dataRepository.DeleteOffer(_offer.Product.Id);
            Assert.AreEqual(ProductNumber, _dataRepository.GetAllOffers().Count());
            Assert.IsFalse(_dataRepository.GetAllOffers().Any(o => o.Product.Id.Equals(_product.Id)));
        }

        [TestMethod]
        public void DeleteOffer_Test_OfferNotInRepo()
        {
            AddProduct_Test_Successful();
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.DeleteOffer(_offer.Product.Id));
        }

        [TestMethod]
        public void DeleteOffer_Test_ProductNotInRepo()
        {
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.DeleteOffer(_offer.Product.Id));
        }

        [TestMethod]
        public void DeleteProduct_Test_Successful()
        {
            AddProduct_Test_Successful();
            _dataRepository.DeleteProduct(_product.Id);
            Assert.AreEqual(ProductNumber, _dataRepository.GetAllProducts().Count());
            Assert.IsFalse(_dataRepository.GetAllProducts().Any(p => p.Id.Equals(_product.Id)));
        }

        [TestMethod]
        public void DeleteProduct_Test_ProductNotInRepo()
        {
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.DeleteProduct(_product.Id));
        }

        [TestMethod]
        public void DeleteProduct_Test_OfferInRepo()
        {
            AddOffer_Test_Successful();
            Assert.ThrowsException<InvalidDataException>(() => _dataRepository.DeleteProduct(_product.Id));
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
            Assert.AreSame(_client, _dataRepository.GetClient(_client.Email));
        }

        [TestMethod]
        public void GetClient_Test_ClientNotInRepo()
        {
            Assert.IsNull(_dataRepository.GetClient(_client.Email));
        }

        [TestMethod]
        public void GetEvent_Test_Successful()
        {
            AddEvent_Test_Successful();
            Assert.AreSame(_facture, _dataRepository.GetEvent(_facture.Id));
        }

        [TestMethod]
        public void GetEvent_Test_EventNotInRepo()
        {
            Assert.IsNull(_dataRepository.GetEvent(_facture.Id));
        }

        [TestMethod]
        public void GetOffer_Test_Successful()
        {
            AddOffer_Test_Successful();
            Assert.AreSame(_offer, _dataRepository.GetOffer(_offer.Product.Id));
        }

        [TestMethod]
        public void GetOffer_Test_OfferNotInRepo()
        {
            Assert.IsNull(_dataRepository.GetOffer(_offer.Product.Id));
        }

        [TestMethod]
        public void GetProduct_Test_Successful()
        {
            AddProduct_Test_Successful();
            Assert.AreSame(_product, _dataRepository.GetProduct(_product.Id));
        }

        [TestMethod]
        public void GetProduct_Test_ProductNotInRepo()
        {
            Assert.IsNull(_dataRepository.GetProduct(_product.Id));
        }

        [TestMethod]
        public void UpdateClient_Test_Successful()
        {
            AddClient_Test_Successful();
            var updatedClient = new Client(_client.Name, _client.Surname, _client.Email, "Białystok");
            _dataRepository.UpdateClient(updatedClient.Email, updatedClient);
            Assert.AreSame(updatedClient, _dataRepository.GetClient(_client.Email));
        }

        [TestMethod]
        public void UpdateClient_Test_InvalidEmail()
        {
            AddClient_Test_Successful();
            var updatedClient = new Client(_client.Name, _client.Surname, "rafon@edu.p.lodz.pl", "Białystok");
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateClient(_client.Email, updatedClient));
        }

        [TestMethod]
        public void UpdateClient_Test_ClientNotInRepo()
        {
            var updatedClient = new Client(_client.Name, _client.Surname, _client.Email, "Białystok");
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateClient(_client.Email, updatedClient));
        }

        [TestMethod]
        public void UpdateEvent_Test_Successful()
        {
            AddEvent_Test_Successful();
            var updatedEvent = new Facture(_facture.Id, _facture.Client, _facture.Offer, _facture.Date, _facture.BoughtProducts + 10);
            _dataRepository.UpdateEvent(updatedEvent.Id, updatedEvent);
            Assert.AreSame(updatedEvent, _dataRepository.GetEvent(_facture.Id));
        }

        [TestMethod]
        public void UpdateEvent_Test_InvalidId()
        {
            AddEvent_Test_Successful();
            var updatedEvent = new Facture(Guid.NewGuid(), _facture.Client, _facture.Offer, _facture.Date, _facture.BoughtProducts);
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateEvent(_facture.Id, updatedEvent));
        }

        [TestMethod]
        public void UpdateEvent_Test_ClientOrOfferNotInRepo()
        {
            var updatedEvent = new Facture(_facture.Id, _facture.Client, _facture.Offer, _facture.Date, _facture.BoughtProducts + 10);
            Assert.ThrowsException<InvalidDataException>(() => _dataRepository.UpdateEvent(updatedEvent.Id, updatedEvent));
        }

        [TestMethod]
        public void UpdateEvent_Test_EventNotInRepo()
        {
            AddClient_Test_Successful();
            AddOffer_Test_Successful();
            var updatedEvent = new Facture(_facture.Id, _facture.Client, _facture.Offer, _facture.Date, _facture.BoughtProducts + 10);
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateEvent(updatedEvent.Id, updatedEvent));
        }

        [TestMethod]
        public void UpdateOffer_Test_Successful()
        {
            AddOffer_Test_Successful();
            var updatedOffer = new Offer(_offer.Product, _offer.NetPrice, _offer.Tax, _offer.ProductsInStock + 10);
            _dataRepository.UpdateOffer(updatedOffer.Product.Id, updatedOffer);
            Assert.AreSame(updatedOffer, _dataRepository.GetOffer(_offer.Product.Id));
        }

        [TestMethod]
        public void UpdateOffer_Test_InvalidProductId()
        {
            AddOffer_Test_Successful();
            var newProduct = new Product(Guid.NewGuid(), _product.Name, _product.Description, _product.Type);
            var updatedOffer = new Offer(newProduct, _offer.NetPrice, _offer.Tax, _offer.ProductsInStock + 10);
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateOffer(_offer.Product.Id, updatedOffer));
        }

        [TestMethod]
        public void UpdateOffer_Test_ProductNotInRepo()
        {
            var updatedOffer = new Offer(_offer.Product, _offer.NetPrice, _offer.Tax, _offer.ProductsInStock + 10);
            Assert.ThrowsException<InvalidDataException>(() => _dataRepository.UpdateOffer(updatedOffer.Product.Id, updatedOffer));
        }

        [TestMethod]
        public void UpdateOffer_Test_OfferNotInRepo()
        {
            AddProduct_Test_Successful();
            var updatedOffer = new Offer(_offer.Product, _offer.NetPrice, _offer.Tax, _offer.ProductsInStock + 10);
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateOffer(updatedOffer.Product.Id, updatedOffer));
        }

        [TestMethod]
        public void UpdateProduct_Test_Successful()
        {
            AddProduct_Test_Successful();
            var updatedProduct = new Product(_product.Id, _product.Name, "zmieniony opis", _product.Type);
            _dataRepository.UpdateProduct(updatedProduct.Id, updatedProduct);
            Assert.AreSame(updatedProduct, _dataRepository.GetProduct(_product.Id));
        }

        [TestMethod]
        public void UpdateProduct_Test_InvalidId()
        {
            AddProduct_Test_Successful();
            var updatedProduct = new Product(Guid.NewGuid(), _product.Name, "zmieniony opis", _product.Type);
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateProduct(_product.Id, updatedProduct));
        }

        [TestMethod]
        public void UpdateProduct_Test_ProductNotInRepo()
        {
            var updatedProduct = new Product(_product.Id, _product.Name, "zmieniony opis", _product.Type);
            Assert.ThrowsException<ArgumentException>(() => _dataRepository.UpdateProduct(updatedProduct.Id, updatedProduct));
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