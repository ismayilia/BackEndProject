using Christmas.Areas.Admin.ViewModels.Cart;
using Christmas.Helpers.Responses;
using Christmas.Models;

namespace Christmas.Services.Interfaces
{
    public interface ICartService
    {
        List<CartVM> GetDatasFromCookie();
        void SetDatasToCookie(List<CartVM> carts, Product dbProduct, CartVM existProduct);
        Task<DeleteBasketResponse> DeleteData(int? id);
        Task<Cart> GetByUserIdAsync(string userId);
        Task<List<CartProduct>> GetAllByCartIdAsync(int? cartId);
		int GetCount();
		Task<IconBasketPlusAndMinus> PlusIcon(int id);
		Task<IconBasketPlusAndMinus> MinusIcon(int id);
	}
}
