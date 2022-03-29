using System;
using System.Data;

namespace WebsiteBanHang
{
    internal class ProperyInfo
    {
        public DataColumn Name { get; internal set; }

        internal object GetVallue<T>(T item, object p)
        {
            throw new NotImplementedException();
        }
    }
}