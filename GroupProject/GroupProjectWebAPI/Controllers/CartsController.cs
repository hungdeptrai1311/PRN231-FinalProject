namespace GroupProjectApi.Controllers;

using BusinessObject.Models;
using DataAccess.Repositories.CartDAO;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]/[action]")]
[ApiController]
public class CartsController : ControllerBase
{
    #region Inject
    
    private readonly ICartRepository cartRepository;

    public CartsController(ICartRepository cartRepository) { this.cartRepository = cartRepository; }

    #endregion

    [HttpGet]
    public IEnumerable<Cart> GetCartsByUserId(int userId) => this.cartRepository.GetCartsByUserId(userId);

    [HttpGet]
    public Cart? GetCartById(int id) => this.cartRepository.GetCartById(id);

    [HttpPost]
    public IActionResult AddCart(Cart cart)
    {
        this.cartRepository.AddCart(cart);
        return this.Ok();
    }
    
    [HttpPut]
    public IActionResult UpdateCart(Cart cart)
    {
        this.cartRepository.UpdateCart(cart);
        return this.Ok();
    }
    
    [HttpDelete]
    public IActionResult DeleteCart(int id)
    {
        this.cartRepository.DeleteCart(id);
        return this.Ok();
    }
}