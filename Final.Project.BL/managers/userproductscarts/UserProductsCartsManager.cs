
using Final.Project.DAL;

namespace Final.Project.BL;

public class UserProductsCartsManager : IUserProductsCartsManager
{
    private readonly IUnitOfWork _unitOfWork;
    //test by abdo
    public UserProductsCartsManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void AddProductToCart(productToAddToCartDto product, string userId)
    {
        var productToAddToCart = new UserProductsCart
        {
            ProductId = product.ProductId,
            UserId = userId,
            Quantity = product.Quantity
        };
        _unitOfWork.UserProdutsCartRepo.Add(productToAddToCart);
        _unitOfWork.Savechanges();
    }

    public void UpdateProductQuantityInCart(productToAddToCartDto product, string userId)
    {
        var productToAddToCart = new UserProductsCart
        {
            ProductId = product.ProductId,
            UserId = userId,
            Quantity = product.Quantity
        };
        _unitOfWork.UserProdutsCartRepo.Add(productToAddToCart);
        _unitOfWork.Savechanges();
    }

    public void DeleteProductFromCart(UserProductInCartDeleteDto product, string userId)
    {
        UserProductsCart productToRemove = new UserProductsCart
        {
            ProductId = product.ProductId,
           // Quantity = 0,
            UserId = userId,
        };
        _unitOfWork.UserProdutsCartRepo.Delete(productToRemove);
        _unitOfWork.Savechanges();

    }

    public void UpdateProductQuantityInCart(ProductQuantityinCartUpdateDto product, string userId)
    {
        UserProductsCart? productToEdit = _unitOfWork.UserProdutsCartRepo.GetByCompositeId(product.ProductId, userId);
        productToEdit.Quantity = product.Quantity;
        _unitOfWork.Savechanges();
    }

    public IEnumerable<AllProductsReadDto> GetAllUserProductsInCart(string userId)
    {
        IEnumerable<UserProductsCart> ProductsFromDB = _unitOfWork.UserProdutsCartRepo.GetAllProductsByUserId(userId);
        IEnumerable<AllProductsReadDto> products = ProductsFromDB.Select(p => new AllProductsReadDto
        {
            Id = p.ProductId,
            Quantity = p.Quantity,
            Name = p.Product.Name,
            Image = p.Product.Image,
            Price = p.Product.Price
        });

        return products;
    }
}
