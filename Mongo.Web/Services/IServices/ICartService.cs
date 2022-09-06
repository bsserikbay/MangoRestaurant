using Mango.Web.Models.Dto;

namespace Mango.Web.Services.IServices
{
    public interface ICartService
    {
        Task<T> GetCartByUserIdAsync<T>(string userId, string? token = null);
        Task<T> AddToCartAsync<T>(CartDto cartDTO, string? token = null);
        Task<T> UpdateCartAsync<T>(CartDto cartDTO, string? token = null);
        Task<T> RemoveFromCartAsync<T>(int cartId, string? token = null);
        Task<T> ApplyCoupon<T>(CartDto cartDto, string? token = null);
        Task<T> RemoveCoupon<T>(string userId, string? token = null);
        Task<T> Checkout<T>(CartHeaderDto cartHeader, string? token = null);

    }
}
