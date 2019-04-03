using System;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace Domain
{
    public class Card
    {
        public int CardId { get; set; }
        public string Title { get; }
        public string Description { get; }
        public int ColumnId { get; set; }
        
        [JsonConstructor]
        public Card(int cardId = -1, string title = "", string description = "", int columnId = -1)
        {
            Title = title;
            Description = description;
            CardId = cardId;
            ColumnId = columnId;
        }

        public Card(long id, string title, string descr, long column_id) 
            : this(Convert.ToInt32(id), title, descr, Convert.ToInt32(column_id)){}
       
    }
}