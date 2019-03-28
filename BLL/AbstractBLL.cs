using System.Collections.Generic;
using System.Data;

namespace BLL
{
    public abstract class AbstractBLL<T>
    {
        public abstract T GetItemById(int id);
        public abstract T CreateNewItem(T column);

        public abstract int DeleteItemById(int id);

        public abstract T UpdateItem(T card);

        public abstract IEnumerable<T> GetAllItems();

    }
}