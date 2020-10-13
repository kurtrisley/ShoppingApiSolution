using ShoppingApi.Domain;
using ShoppingApi.Models.Catalog.Curbside;
using System.Threading.Tasks;

namespace ShoppingApi.Controllers
{
    public interface IDoCurbsideCommands
    {
        Task<CurbsideOrder> AddOrder(PostCurbsideOrderRequest orderToPlace);
    }
}