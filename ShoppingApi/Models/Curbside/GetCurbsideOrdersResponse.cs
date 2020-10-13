using ShoppingApi.Domain;

namespace ShoppingApi.Models.Catalog.Curbside
{
    public class GetCurbsideOrdersResponse : Collection<CurbsideOrder>
    {
        public int NumberOfPendingOrders { get; set; }
        public int NumberOfApprovedOrders { get; set; }
        public int NumberOfDeniedOrders { get; set; }
        public int NumberOffulfilledOrders { get; set; }
    }
}
