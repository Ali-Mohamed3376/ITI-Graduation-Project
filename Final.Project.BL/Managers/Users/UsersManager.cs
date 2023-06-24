using Final.Project.DAL;

namespace Final.Project.BL;

public class UsersManager : IUsersManager
{
    private readonly IUserRepo? _userRepo;


    public UsersManager(IUserRepo userRepo)
    {
        _userRepo = userRepo;
    }

    #region profile
    public bool delete(string id)
    {
        User? user = _userRepo.GetById(id);

        if (user is null) { return false; }

        _userRepo.Delete(user);

        _userRepo.savechanges();

        return true;
    }

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

    }

    

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
    #endregion

    #region Order

    public UserOrderDto GetUserOrderDto(string id)
    {
        Order? order = _userRepo.GetUsersOrder(id);
        if (order == null) { return null; }
        return new UserOrderDto
        {
            Id = order.Id,
            OrderStatus = order.OrderStatus,
            DeliverdDate = order.DeliverdDate,
            Products = order.OrdersProductDetails.Select(ip => new ProductChildDto
            {
                Image = ip.Product.Image,
                Name = ip.Product.Name,
                Price = ip.Product.Price

            }

            )

        };
    }


    #endregion

    #region order details
    #endregion
}
