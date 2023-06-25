namespace Final.Project.BL;

public interface IReviewsManager
{
    void AddReview(string userId, int productId, string comment, int rating);

    public double GetAverageRating(int productId);
}
