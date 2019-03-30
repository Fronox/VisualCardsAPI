using System.Collections.Generic;
using System.Runtime.InteropServices;
using DAL;
using Domain;

namespace BLL
{
    public class ColumnBLL : AbstractBLL<Column>
    {
        private ColumnRepo _columnRepo = new ColumnRepo();
        
        public override Column GetItemById(int id)
        {
            var col = _columnRepo.GetItemById(id);
            return col;
        }

        public override Column CreateNewItem(Column column)
        {
            var col = _columnRepo.SaveItem(column);
            return col;
        }

        public override int DeleteItemById(int id)
        {
            return _columnRepo.DeleteItem(id);
        }

        public override Column UpdateItem(Column column)
        {
            return _columnRepo.UpdateItem(column);
        }

        public override IEnumerable<Column> GetAllItems()
        {
            return _columnRepo.GetAllItems();
        }
    }
}