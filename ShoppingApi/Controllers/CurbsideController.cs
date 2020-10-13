using Microsoft.AspNetCore.Mvc;
using ShoppingApi.Domain;
using ShoppingApi.Models.Catalog.Curbside;
using System.Threading.Tasks;

namespace ShoppingApi.Controllers
{
    public class CurbsideController : ControllerBase
    {

        private readonly IDoCurbsideQueries _curbsideQueries;
        private readonly IDoCurbsideCommands _curbsideCommands;

        public CurbsideController(IDoCurbsideQueries curbsideQueries, IDoCurbsideCommands curbsideCommands)
        {
            _curbsideQueries = curbsideQueries;
            _curbsideCommands = curbsideCommands;
        }

        [HttpPost("curbsideorders")]
        public async Task<ActionResult> AddAnOrder([FromBody] PostCurbsideOrderRequest orderToPlace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                CurbsideOrder response = await _curbsideCommands.AddOrder(orderToPlace);
                return CreatedAtRoute("curbside#getbyid", new { orderId = response.Id }, response);
            }
        }


        [HttpGet("curbsideorders")]
        public async Task<ActionResult> GetAllOrders()
        {
            GetCurbsideOrdersResponse response = await _curbsideQueries.GetAll();

            return Ok(response);
        }
        [HttpGet("curbsideorders/{orderId:int}", Name = "curbside#getbyid")]
        public async Task<ActionResult> GetOrderById(int orderId)
        {
            CurbsideOrder response = await _curbsideQueries.GetById(orderId);
            if (response == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(response);
            }
        }
    }
}