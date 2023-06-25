

namespace Final.Project.DAL;

public interface IReviewRepo : IGenericRepo<Review>
{
    IEnumerable<Review> GetReviewsByProduct(int productId);

}
