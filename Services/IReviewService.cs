using Ecommerce.DTOs;
using Ecommerce.Models;

namespace Ecommerce.Services;

public interface IReviewService
{
    public Task<Review?> AddReviewAsync(ReviewDTO request, string userId);
    public Task<bool> DeleteReviewAsync(int productId, string userId);
    public Task<Review?> GetReviewAsync(int id, string userId);
}