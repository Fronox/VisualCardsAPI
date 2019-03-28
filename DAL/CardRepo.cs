using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;
using Domain;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;


namespace DAL
{
    public class CardRepo : Repo<Card>
    {        
        public override Card SaveItem(Card column)
        {
            var sqlInsertString = "INSERT INTO cards (title, descr, column_id) VALUES " +
                                  $"(\"{column.Title}\", \"{column.Description}\", \"{column.ColumnId}\")";
            int id = InsertIntoDB(sqlInsertString);
            column.CardId = id;
            return column;
        }

        public override int DeleteItem(int id)
        {
            var sqlDeleteString = $"DELETE FROM cards WHERE id = {id}";
            QueryDB(sqlDeleteString);
            return id;//TODO: return value?
        }

        public override Card UpdateItem(Card card)
        {
            var sqlUpdate = "UPDATE cards " +
                            $"SET title=\"{card.Title}\", " +
                            $"descr=\"{card.Description}\"," +
                            $"column_id={card.ColumnId} " +
                            $"WHERE cards.id = {card.CardId}";
            QueryDB(sqlUpdate);
            return card;
        }

        public override Card GetItemById(int id)
        {
            var sqlDeleteString = $"SELECT * FROM cards WHERE id = {id}";
            var res = QueryDB(sqlDeleteString);
            var data = res.Rows[0].ItemArray;
            var card = new Card(Convert.ToInt32(data[0]), Convert.ToInt32(data[3]), 
                (string) data[1], (string) data[2]);
            return card;
        }
        
        public override IEnumerable<Card> GetAllItems()
        {
            var sqlSelectAllCards = "SELECT * FROM cards ORDER BY cards.id";
            var rawList = QueryDB(sqlSelectAllCards);
            var resList = new List<Card>();
            foreach (DataRow row in rawList.Rows)
            {
                var card = new Card(Convert.ToInt32(row[0]), Convert.ToInt32(row[3]), 
                    (string) row[1], (string) row[2]);
                resList.Add(card);
            }
            
            return resList;
        }

        public List<Card> GetCardsFromCol(int colId)
        {
            string sqlSelectCards = $"SELECT cards.id, cards.column_id, cards.title, cards.descr FROM columns JOIN cards ON columns.id = cards.column_id WHERE cards.column_id = {colId} ORDER BY cards.id";
            var rawCards = QueryDB(sqlSelectCards).Rows;
            List<Card>cardList = new List<Card>();
            for(int i = 0; i < rawCards.Count; i++)
            {
                var rawCard = rawCards[i].ItemArray;
                var card = new Card(Convert.ToInt32(rawCard[0]), Convert.ToInt32(rawCard[1]), 
                    rawCard[2].ToString(), rawCard[3].ToString());
                cardList.Add(card);
            }

            return cardList;
        }
    }
}