using DataLayer.Entities.Store;

namespace Core.DTOs.General
{
    /// <summary>
    /// use for new,populare,best seller, better products
    /// </summary>
    public class SepcialProducts
    {
        public List<Product> Products { get; set; } = new();
        public string? CartId { get; set; }
        public string? Title { get; set; }
    }
}
