using System;
using System.Collections.Generic;
using System.Data;
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
            foreach (Card card in column.CardList)
            {
                card.ColumnId = column.ColumnId;
                _cardRepo.SaveItem(card);
            }
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

        public override Column UpdateItem(Column column)
        {
            var sqlUpdateColTitle = $"UPDATE columns SET title = \"{column.Title}\" WHERE id = {column.ColumnId}";
            QueryDB(sqlUpdateColTitle);
            return column;
        }

        public override IEnumerable<Column> GetAllItems()
        {
            var sqlSelectAllColumns = "SELECT * FROM columns ORDER BY id";
            var rawCols = QueryDB(sqlSelectAllColumns);
            var colList = new List<Column>();
            foreach (DataRow dataRow in rawCols.Rows)
            {
                var rawCol = dataRow.ItemArray;
                var colId = Convert.ToInt32(rawCol[0]);
                var cardList = _cardRepo.GetCardsFromCol(colId);
                var title = rawCol[1].ToString();
                var col = new Column(cardList, colId, title);
                colList.Add(col);
            }

            return colList;
        }
    }
}