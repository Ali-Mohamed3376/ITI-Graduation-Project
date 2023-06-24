
using Final.Project.DAL;

namespace Final.Project.BL;

public class ReviewsManager:IReviewsManager
{
    private readonly IUnitOfWork _unitOfWork;

    public ReviewsManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public void AddReview(string userId, int productId, string comment, int rating)
    {
        Review newReview = new Review
        {
            UserId = userId,
            ProductId = productId,
            Comment = comment,
            CreationDate = DateTime.Now,
            Rating = rating
        };

        _unitOfWork.ReviewRepo.Add(newReview);
        _unitOfWork.Savechanges();
    }



    public double GetAverageRating(int productId)
    {
        var reviews = _unitOfWork.ReviewRepo.GetReviewsByProduct(productId);
        return reviews.Any() ? reviews.Average(r => r.Rating) : 0;
    }
}
