using System;
using System.Collections.Generic;
using System.Text;

namespace Store
{
    public interface IDataFiller
    {
        void Fill(DataContext context);
    }
}
