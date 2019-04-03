using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Domain
{
    public class Column
    {
        public int ColumnId { get; set; }

        public List<Card> CardList { get; set; }

        public string Title { get; set; }

        [JsonConstructor]
        public Column(List<Card> cards, int columnId = -1, string title = "")
        {
            ColumnId = columnId;
            CardList = cards;
            Title = title;
        }

        public Column(long id, string title) : this(null, Convert.ToInt32(id), title){}
    }
}