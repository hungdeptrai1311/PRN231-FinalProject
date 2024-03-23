namespace DataAccess.Repositories.CartDAO;

using BusinessObject.Models;
using DataAccess.DAOs;

public class CartRepository : ICartRepository
{
    public List<Cart> GetCartsByUserId(int userId) => CartDAO.GetCartsByUserId(userId);
    public Cart?      GetCartById(int id)          => CartDAO.GetCartById(id);
    public void       AddCart(Cart cart)           => CartDAO.AddCart(cart);
    public void       UpdateCart(Cart cart)        => CartDAO.UpdateCart(cart);
    public void       DeleteCart(int id)           => CartDAO.DeleteCart(id);
}