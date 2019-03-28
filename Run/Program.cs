using System;
using BLL;
using DAL;
using Microsoft.Extensions.Configuration.Memory;

namespace Run
{
    class Program
    {
        static void Main(string[] args)
        {
            var rep = new CardRepo();
            var cardBll = new CardBLL(rep);
            cardBll.PrintAllCards();
            var card = cardBll.GetCardById(1);
            Console.WriteLine(card.CardId + " " + card.Title + " " + card.Description);
        }
    }
}