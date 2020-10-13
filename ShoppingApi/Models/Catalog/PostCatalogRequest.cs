using System.ComponentModel.DataAnnotations;

namespace ShoppingApi.Models.Catalog
{
    public class PostCatalogRequest
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Cost { get; set; }
    }
}
