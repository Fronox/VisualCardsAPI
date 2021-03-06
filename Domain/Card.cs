﻿using System;

namespace Domain
{
    public class Card
    {
        public int CardId { get; set; }
        public string Title { get; }
        public string Description { get; }
        public int ColumnId { get; set; }

        public Card(int cardId = -1, string title = "", string description = "", int columnId = -1)
        {
            Title = title;
            Description = description;
            CardId = cardId;
            ColumnId = columnId;
        }
    }
}