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
        public override Card SaveItem(Card card)
        {
            var sqlInsertString = "INSERT INTO cards (title, descr, column_id) VALUES " +
                                  $"(\"{card.Title}\", \"{card.Description}\", \"{card.ColumnId}\")";
            int id = InsertIntoDB(sqlInsertString);
            card.CardId = id;
            return card;
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
            var card = new Card(Convert.ToInt32(data[0]), (string) data[1],
                (string) data[2], Convert.ToInt32(data[3]));
            return card;
        }
        
        public override IEnumerable<Card> GetAllItems() //useless
        {
            var sqlSelectAllCards = "SELECT * FROM cards ORDER BY cards.id";
            var rawList = QueryDB(sqlSelectAllCards);
            var resList = new List<Card>();
            foreach (DataRow row in rawList.Rows)
            {
                var card = new Card(Convert.ToInt32(row[0]), (string) row[1], 
                    (string) row[2], Convert.ToInt32(row[3]));
                resList.Add(card);
            }
            
            return resList;
        }

        public List<Card> GetCardsFromCol(int cardId)
        {
            string sqlSelectCards = $"SELECT * FROM cards WHERE column_id = {cardId} ORDER BY id";
            var rawCards = QueryDB(sqlSelectCards).Rows;
            List<Card>cardList = new List<Card>();
            for(int i = 0; i < rawCards.Count; i++)
            {
                var rawCard = rawCards[i].ItemArray;
                var card = new Card(Convert.ToInt32(rawCard[0]), rawCard[1].ToString(), 
                    rawCard[2].ToString(), Convert.ToInt32(rawCard[3]));
                cardList.Add(card);
            }

            return cardList;
        }

        public IEnumerable<Card> GetCardsWithTitle(string title)
        {
            string sqlSelectCardsWithString = $"SELECT * FROM cards WHERE title = \"{title}\" ORDER BY id";
            var rawRes = QueryDB(sqlSelectCardsWithString).Rows;
            List<Card> cardList = new List<Card>();
            foreach (DataRow dataRow in rawRes)
            {
                var rawCard = dataRow.ItemArray;
                var card = new Card(Convert.ToInt32(rawCard[0]), rawCard[1].ToString(),
                    rawCard[2].ToString(), Convert.ToInt32(rawCard[3]));
                cardList.Add(card);
            }

            return cardList;
        }
    }
}