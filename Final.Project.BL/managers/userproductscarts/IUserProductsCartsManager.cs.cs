
namespace Final.Project.BL;

public interface IUserProductsCartsManager
{
    void AddProductToCart(productToAddToCartDto product,string userId);
    void UpdateProductQuantityInCart(ProductQuantityinCartUpdateDto product, string userId);

    void DeleteProductFromCart(UserProductInCartDeleteDto product, string userId);
    IEnumerable<AllProductsReadDto> GetAllUserProductsInCart(string userId);
}
