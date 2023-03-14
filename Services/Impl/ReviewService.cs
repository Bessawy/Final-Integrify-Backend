namespace Ecommerce.Services;

using Ecommerce.Models;
using Ecommerce.DTOs;
using Ecommerce.Db;
using System.Threading.Tasks;

public class ReviewService : IReviewService
{

    private readonly AppDbContext _dbContext;
    private readonly IUserService _service;

    public ReviewService(AppDbContext dbContext, IUserService service)
    {
        _dbContext = dbContext;
        _service = service;
    }

    public async Task<Review?> AddReviewAsync(ReviewDTO request, string userId)
    {
        var user = await _service.FindUserByIdAsync(userId);
        var product = await _dbContext.Products.FindAsync(request.ProductId);
        if(user == null || product == null)
            return null;

        await _dbContext.Entry(user).Collection(u => u.Reviews).LoadAsync();
        bool doExist = user.Reviews.Select(r => r.ProductId)
            .Any(id => id == product.Id);

        if(doExist)
            return null;

        var review = new Review
        {
            User = user,
            Product = product,
            Rate = request.Rate,
            Comment = request.Comment
        };

        UpdateProductRating(product, request.Rate);
        user.Reviews.Add(review);
        await _dbContext.SaveChangesAsync();
        return review;
    }

    public async Task<bool> DeleteReviewAsync(int productId, string userId)
    {
        var user = await _service.FindUserByIdAsync(userId);
        if(user is null)
            return false;

        await _dbContext.Entry(user).Collection(u => u.Reviews).LoadAsync();
        var review = user.Reviews.FirstOrDefault(r => r.ProductId == productId);
        
        if(review is null)
            return false;

        await _dbContext.Entry(review).Reference(r => r.Product).LoadAsync();
        UpdateProductRating(review.Product, -1 * review.Product.Rating);
        user.Reviews.Remove(review);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public void UpdateProductRating(Product product, double newRating)
    {
        product.Rating = (product.Rating * product.NumberOfReviews) + newRating;

        if(newRating < 0)
            product.NumberOfReviews--;
        else
            product.NumberOfReviews++;

        product.Rating /= product.NumberOfReviews;
    }

}