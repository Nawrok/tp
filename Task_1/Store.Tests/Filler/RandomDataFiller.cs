using System;
using Store.DAL;

namespace Store.Tests.Filler
{
    internal class RandomDataFiller : IDataFiller
    {
        private static Random _random = new Random();
        private int _clientNumber;
        private int _factureNumber;
        private int _productNumber;

        public RandomDataFiller(int clientNumber, int productNumber, int factureNumber)
        {
            _clientNumber = clientNumber;
            _productNumber = productNumber;
            _factureNumber = factureNumber;
        }

        public void Fill(IDataRepository repository) { }
    }
}