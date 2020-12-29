using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
    public static class Tools
    {
        public static List<Product> GetProductsByName(string namePart)
        {
            using (ProductionDataContext _dataContext = new ProductionDataContext())
            {
                Table<Product> products = _dataContext.GetTable<Product>();
                List<Product> result = (from product in products
                                        where product.Name.Contains(namePart)
                                        select product).ToList();
                return result;
            }
        }
    }
}
