using Final.Project.DAL;
using System.Collections.Generic;

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

    //public bool Edit(UserUpdateDto updateDto,string id)
    //{

    //    User? user = _userRepo.GetById(id);

    //    if (user is null) { return false; };

    //    user.FName = updateDto.Fname;

    //    user.LName = updateDto.Lname;

    //    user.Street = updateDto.Street;

    //    user.City = updateDto.City;

    //   // _userRepo.Update(user);

    //    _userRepo.savechanges();
    //    return true;

    //}
    public bool Edit(UserUpdateDto updateDto, User user)
    {



        user.FName = updateDto.Fname;

        user.LName = updateDto.Lname;

        user.Street = updateDto.Street;

        user.City = updateDto.City;

        // _userRepo.Update(user);

        _userRepo.savechanges();
        return true;

    }

    #region UserOrderDetails
    public IEnumerable<UserOrderDetailsDto> GetUserOrderDetailsDto(int id)
    {
        
        IEnumerable<OrderProductDetails> OrderProductDetails = _userRepo.GetUsersOrderDetails(id);
        if (OrderProductDetails == null)
        {
            return null;
        }

        IEnumerable < UserOrderDetailsDto > products = OrderProductDetails.Select(p=>new  UserOrderDetailsDto
        {
            Image = p.Product.Image,
            Price = p.Product.Price,
            Quantity = p.Quantity,
            title=p.Product.Name
        }); 

        return products;
        
    }
    #endregion

    #region UserOrder
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

    #endregion

    #region View user Profile
    //public UserReadDto GetUserReadDto(string id)
    //{
    //    User? user = _userRepo.GetById(id);
    //    if (user == null)
    //    {
    //        return null;
    //    }
    //    return new UserReadDto
    //    {
    //        Fname = user.FName,

    //        Lname = user.LName,

    //        City = user.City,

    //        Street = user.Street,

    //    };

    //}
    public UserReadDto GetUserReadDto(User user)
    {
        
        return new UserReadDto
        {
            Fname = user.FName,

            Lname = user.LName,

            City = user.City,

            Street = user.Street,
            
            Email = user.Email

        };

    }




    #endregion
}
