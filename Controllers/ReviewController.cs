using Ecommerce.DTOs;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers;

[Authorize]
[Route("api/v1/my-reviews")]
public class ReviewController : ApiControllerBase
{

    private readonly IReviewService _service;
    private readonly ILogger<ReviewService> _logger;

    public ReviewController(IReviewService service, ILogger<ReviewService> logger)
    {
        _service = service;
        _logger = logger;
    }


    [HttpDelete("{id}")]
    public async Task<bool> DeleteReview(int id)
    {
        string? userId = GetUserIdFromToken();
        if(userId is null)
            return false;

        return await _service.DeleteReviewAsync(id, userId);
    } 

    [HttpPost]
    public async Task<ActionResult<Review>> CreateReview(ReviewDTO request)
    {
        string? userId = GetUserIdFromToken();
        if(userId is null)
            return BadRequest();

        var review = await _service.AddReviewAsync(request, userId);
        if(review is null)
            return BadRequest();

        return Ok(review);
    } 
}

