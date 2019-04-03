using System.Collections.Generic;
using BLL;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace VisualCardsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private CardBLL _cardBll = new CardBLL();

        // GET api/cards/id
        [HttpGet("{id}")]
        public ActionResult<Card> Get(int id)
        {
            return _cardBll.GetItemById(id);
        }

        // POST api/cards
        [HttpPost]
        public ActionResult<Card> Post([FromBody] Card card)
        {
            //Console.WriteLine(card);
            var savedCard = _cardBll.CreateNewItem(card);
            return savedCard;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Card card)
        {
            card.CardId = id;
            _cardBll.UpdateItem(card);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _cardBll.DeleteItemById(id);
        }
        
        //GET all cards with this title
        [HttpGet("search")]
        public ActionResult<IEnumerable<Card>> Get([FromQuery(Name="title")] string title)
        {
            return new ActionResult<IEnumerable<Card>>(_cardBll.GetItemsWithTitle(title));
        }
        
    }
}