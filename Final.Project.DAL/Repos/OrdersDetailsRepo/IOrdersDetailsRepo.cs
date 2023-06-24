namespace Final.Project.DAL;
public interface IOrdersDetailsRepo : IGenericRepo<OrderProductDetails>
{
    void AddRange(IEnumerable<OrderProductDetails> orderProducts);
}
