using Final.Project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.BL;

public class OrdersManager : IOrdersManager
{
    private readonly IUnitOfWork _unitOfWork;

    public OrdersManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public void AddNewOrder(string userId, int addressId)
    {
        //1-Add new order in order table
        Order newOrder = new Order
        {
            OrderStatus = OrderStatus.Pending,
            OrderDate = DateTime.Now,
            UserId = userId,
            UserAddressId = addressId
        };
        _unitOfWork.OrderRepo.Add(newOrder);
        _unitOfWork.Savechanges();
        //2-we need the orderId of the new order to use it in orderdetails table
        //so lets get that 
        int LastOrderId = _unitOfWork.OrderRepo.GetLastUserOrder(userId);

        //3-transfer products from cart to order 
        IEnumerable<UserProductsCart> productsFromCart = _unitOfWork.UserProdutsCartRepo.GetAllProductsByUserId(userId);

        //4-we need  productId,OrderId,Quantity for each row in orderDetails table
        //so lets extract this data from products we got from cart and insert them
        // to userProductsDetails Table

        var orderProducts = productsFromCart.Select(p => new OrderProductDetails
        {
            OrderId = LastOrderId,
            ProductId = p.ProductId,
            Quantity = p.Quantity
        });

        _unitOfWork.OrdersDetailsRepo.AddRange(orderProducts);

        //5-Make The UserCart Empty

        _unitOfWork.UserProdutsCartRepo.DeleteAllProductsFromUserCart(userId);
        _unitOfWork.Savechanges();
    }
}
