using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Domain;

namespace DAL
{
    public class ColumnRepo : Repo<Column>
    {
        CardRepo _cardRepo = new CardRepo();
        public override Column GetItemById(int id)
        {
            var sqlSelectColumn = $"SELECT * FROM columns WHERE id = {id}";
            var res = QueryDB(sqlSelectColumn);
            var rawCol = res.Rows[0].ItemArray;
            var colId = Convert.ToInt32(rawCol[0]);
            var colTitle = rawCol[1].ToString();
            var cardList = _cardRepo.GetCardsFromCol(colId);        
            return new Column(cardList, colId, colTitle);
        }

        public override Column SaveItem(Column column) //TODO: Add column.cardList in DB
        {
            var sqlInsertColumn = $"INSERT INTO columns (title) VALUES (\"{column.Title}\")";
            int id = InsertIntoDB(sqlInsertColumn);
            column.ColumnId = id;
            return column;
        }

        public override int DeleteItem(int id)
        {
            var sqlDeleteColumn = $"DELETE FROM columns WHERE id = {id}";
            var sqlDeleteCards = $"DELETE FROM cards WHERE column_id = {id}";
            QueryDB(sqlDeleteColumn);
            QueryDB(sqlDeleteCards);
            return id;
        }

        public override Column UpdateItem(Column item)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<Column> GetAllItems()
        {
            throw new System.NotImplementedException();
        }
    }
}