using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using Domain;

namespace DAL
{
    public class ColumnDapper : Repo<Column>
    {
        CardDapper _cardDapper = new CardDapper();
        public override Column GetItemById(int id) // useless
        {
            using (IDbConnection db = new SQLiteConnection(connString))
            {
                if (db.State == ConnectionState.Closed)  
                    db.Open();
                throw new NotImplementedException();
            }
        }

        public override Column SaveItem(Column column)
        {
            using (IDbConnection db = new SQLiteConnection(connString))
            {
                if (db.State == ConnectionState.Closed)  
                    db.Open();
                
                var sqlInsertColumn = "INSERT INTO columns (title) VALUES (@Title); SELECT CAST(SCOPE_IDENTITY() as int)";
                int? id = db.Query(sqlInsertColumn, column).FirstOrDefault();
                column.ColumnId = id.Value;
                foreach (Card card in column.CardList)
                {
                    card.ColumnId = column.ColumnId;
                    _cardDapper.SaveItem(card);
                }
                return column;
            }
        }

        public override int DeleteItem(int id)
        {
            using (IDbConnection db = new SQLiteConnection(connString))
            {
                if (db.State == ConnectionState.Closed)  
                    db.Open();
                var sqlDeleteColumn = "DELETE FROM columns WHERE id = @id";
                var sqlDeleteCards = "DELETE FROM cards WHERE column_id = @id";
                db.Execute(sqlDeleteColumn, new {id});
                db.Execute(sqlDeleteCards, new {id});
                return id;
            }
        }

        public override Column UpdateItem(Column card) //useless
        {
            using (IDbConnection db = new SQLiteConnection(connString))
            {
                if (db.State == ConnectionState.Closed)  
                    db.Open();
                throw new NotImplementedException();
            }
        }

        public override IEnumerable<Column> GetAllItems()
        {
            var rawCols = GetColumns();
            var colList = new List<Column>();
            foreach (var rawCol in rawCols)
            {
                var cardList = _cardDapper.GetCardsFromCol(rawCol.ColumnId);
                var col = new Column(cardList, rawCol.ColumnId, rawCol.Title);
                colList.Add(col);
            }

            return colList;
        }

        private List<Column> GetColumns()
        {
            using (IDbConnection db = new SQLiteConnection(connString))
            {
                var sqlSelectAllColumns = "SELECT * FROM columns ORDER BY id";
                return db.Query<Column>(sqlSelectAllColumns) as List<Column>;
            }
        }
    }
}