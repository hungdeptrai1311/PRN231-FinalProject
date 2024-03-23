namespace DataAccess.Repositories.CartDAO;

using BusinessObject.Models;

public interface ICartRepository
{
    List<Cart> GetCartsByUserId(int userId);
    Cart?      GetCartById(int id);
    void       AddCart(Cart cart);
    void       UpdateCart(Cart cart);
    void       DeleteCart(int id);
}