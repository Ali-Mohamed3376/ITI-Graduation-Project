using Final.Project.DAL;

namespace Final.Project.BL;

public class UsersManager : IUsersManager
{
    private readonly IUserRepo? _userRepo;


    public UsersManager(IUserRepo userRepo)
    {
        _userRepo = userRepo;
    }
    #region delete user's account
    public bool delete(string id)
    {
        User? user = _userRepo.GetById(id);

        if (user is null) { return false; }

        _userRepo.Delete(user);
        _userRepo.savechanges();

        return true;
    }
    #endregion

    public bool Edit(UserUpdateDto updateDto)
    {

        User? user = _userRepo.GetById(updateDto.Id);

        if (user is null) { return false; };

        user.FName = updateDto.Fname;

        user.LName = updateDto.Lname;

        user.Street = updateDto.Street;

        user.City = updateDto.City;

        _userRepo.Update(user);

        _userRepo.savechanges();
        return true;


        //return new User
        //{
        //    FName = updateDto.Fname,

        //    Lname = updateDto.Lname,

        //     Street = updateDto.Street,

        //     City = updateDto.City,

        //};

        //if (user is null) { return false; };

        //user.FName = updateDto.Fname;
        //user.LName = updateDto.Lname;
        //user.Street = updateDto.Street;
        //user.City = updateDto.City;
        //_userRepo.Update(user);
        //_userRepo.savechanges();
        //return true;

    }

    public UserOrderDetailsDto GetUserOrderDetailsDto(int id)
    {
        
        Product? product = (Product?)_userRepo.GetUsersOrderDetails(id);
        if (product== null)
        {
            return null;
        }
        return new UserOrderDetailsDto
        {
            Image = product.Image,
            Price = product.Price,
        };
    }

    public UserOrderDto GetUserOrderDto(string id)
    {
        Order? order = _userRepo.GetUsersOrder(id);
        if (order == null) { return null; }
        return new UserOrderDto
        {
            Id = order.Id,
            OrderStatus = order.OrderStatus,
            DeliverdDate = order.DeliverdDate,
            Products = order.OrdersProductDetails.Select(ip => new UserProductDto
            {
                Image = ip.Product.Image,
                title = ip.Product.Name,
            }

            )

        };
    }

   





    #region View user Profile
    public UserReadDto GetUserReadDto(string id)
    {
        User? user = _userRepo.GetById(id);
        if (user == null)
        {
            return null;
        }
        return new UserReadDto
        {
            Fname = user.FName,

            Lname = user.LName,

            City = user.City,

            Street = user.Street,
            
        };

    }

    //public UserOrderDto userOrder(string id)
    //{
    //    Order? user = _userRepo.GetUsersOrder(id);
    //    if(user == null)
    //    {
    //        return null;
    //    }
    //    return new UserOrderDto
    //    {
    //        OrderStatus = user.OrderStatus,
    //        DeliverdDate = user.DeliverdDate,
    //    };
    //}

    

    #endregion

    //public IEnumerable<UserOrderDto> UserOrders()
    //{
    //    IEnumerable<Order>? orders = _userRepo.GetUsersOrder();
    //    IEnumerable<UserOrderDto> orderDto = orders
    //        .Select(c => new UserOrderDto
    //        {
    //            DeliverdDate=c.DeliverdDate,
    //            OrderStatus=c.OrderStatus,
    //            Products = c.OrdersProductDetails.Select(p => new UserProductDto
    //            {
    //                Image=p.Product.Image,
    //                title=p.Product.Name
    //            }).ToList()

    //        });
    //    return orderDto;
    //}
}
