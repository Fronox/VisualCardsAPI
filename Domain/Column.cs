using System.Collections.Generic;

namespace Domain
{
    public class Column
    {
        public int ColumnId { get; set; }

        public List<Card> CardList { get; set; }

        public string Title { get; set; }

        public Column(List<Card> cards, int columnId = -1, string title = "")
        {
            ColumnId = columnId;
            CardList = cards;
            Title = title;
        }
    }
}