using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Domain;
using Dapper;

namespace DAL
{
    public class CardDapper : Repo<Card>
    {
        
        public override Card GetItemById(int id)
        {
            using (IDbConnection db = new SQLiteConnection(connString))
            {
                if (db.State == ConnectionState.Closed)  
                    db.Open();
                return db.Query<Card>("SELECT * FROM cards WHERE id = @id", new {id}).FirstOrDefault();
            }
        }

        public override Card SaveItem(Card card)
        {
            using (IDbConnection db = new SQLiteConnection(connString))
            {
                if (db.State == ConnectionState.Closed)  
                    db.Open();
                var sqlQuery = "INSERT INTO cards (title, descr, column_id) VALUES(@Title, @Description, @ColumnId); SELECT CAST(SCOPE_IDENTITY() as int)";
                int? cardId = db.Query(sqlQuery, card).FirstOrDefault();
                card.CardId = cardId.Value;
                return card;
            }
        }

        public override int DeleteItem(int id)
        {
            using (IDbConnection db = new SQLiteConnection(connString))
            {
                if (db.State == ConnectionState.Closed)  
                    db.Open();
                var sqlQuery = "DELETE FROM cards WHERE id = @id";
                db.Execute(sqlQuery, new { id });
                return id;
            }
        }

        public override Card UpdateItem(Card card)
        {
            using (IDbConnection db = new SQLiteConnection(connString))
            {
                if (db.State == ConnectionState.Closed)  
                    db.Open();
                var sqlQuery = "UPDATE cards SET title = @Title, descr = @Description, column_id = @ColumnId WHERE id = @CardId";
                db.Execute(sqlQuery, card);
                return card;
            }
        }

        public override IEnumerable<Card> GetAllItems() //useless
        {
            using (IDbConnection db = new SQLiteConnection(connString))
            {
                if (db.State == ConnectionState.Closed)  
                    db.Open();
                throw new NotImplementedException();
            }
        }
        
        public IEnumerable<Card> GetCardsWithTitle(string title)
        {
            using (IDbConnection db = new SQLiteConnection(connString))
            {
                if (db.State == ConnectionState.Closed)  
                    db.Open();
                return db.Query<Card>("SELECT * FROM cards WHERE title = @title", new {title});
            }
        }
        
        public List<Card> GetCardsFromCol(int colId)
        {
            using (IDbConnection db = new SQLiteConnection(connString))
            {
                if (db.State == ConnectionState.Closed)  
                    db.Open();
                
                string sqlSelectCards = "SELECT * FROM cards WHERE column_id = @colId ORDER BY id";
                return db.Query<Card>(sqlSelectCards, new {colId}) as List<Card>;
            }
        }
    }
}