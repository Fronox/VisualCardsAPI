using System;
using System.Collections.Generic;
using DAL;
using Domain;

namespace BLL
{
    public class CardBLL : AbstractBLL<Card>
    {
        private readonly CardRepo _cardRepo;
        
        public CardBLL()
        {
            _cardRepo = new CardRepo();
        }

        public override Card GetItemById(int id)
        {
            //var card = new Card(1, "2", "3");
            var card = _cardRepo.GetItemById(id);
            return card;
        }
        
        public override Card CreateNewItem(Card column)
        {
            //var newCard = new Card(title, descr, 0);
            var savedCard = _cardRepo.SaveItem(column);
            return savedCard;
        }

        public override int DeleteItemById(int id)
        {
            return _cardRepo.DeleteItem(id);
        }

        public override Card UpdateItem(Card card)
        {
            var updatedCard = _cardRepo.UpdateItem(card);
            return updatedCard;
        }

        public override IEnumerable<Card> GetAllItems()
        {
            return _cardRepo.GetAllItems();
        }

        public IEnumerable<Card> GetItemsWithTitle(string title)
        {
            return _cardRepo.GetCardsWithTitle(title);
        }
    }
}