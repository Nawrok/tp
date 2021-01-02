using System.Reflection;

namespace LINQ.MyProduct
{
    public class MyProduct : Product
    {
        public MyProduct(Product product)
        {
            foreach (PropertyInfo property in product.GetType().GetProperties())
            {
                if (property.CanWrite)
                {
                    property.SetValue(this, property.GetValue(product));
                }
            }
        }
    }
}