using BLL;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace VisualCardsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnsController : ControllerBase
    {
        private ColumnBLL _columnBll = new ColumnBLL();

        [HttpGet("{id}")]
        public ActionResult<Column> Get(int id)
        {
            return _columnBll.GetItemById(id);
        }

        [HttpPost]
        public ActionResult<Column> Post([FromBody] Column column)
        {
            var savedColumn = _columnBll.CreateNewItem(column);
            return savedColumn;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _columnBll.DeleteItemById(id);
        }
    }
}