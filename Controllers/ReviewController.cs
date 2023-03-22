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
    public async Task<IActionResult> DeleteReview(int id)
    {
        string? userId = GetUserIdFromToken();
        if(userId is null)
            return Unauthorized();

        if(await _service.DeleteReviewAsync(id, userId))
            return Ok(new {Message = "Item is deleted!"});
        else
            return NotFound("Item not found!");
    } 

    [HttpPost]
    public async Task<ActionResult<Review>> CreateReview(ReviewDTO request)
    {
        string? userId = GetUserIdFromToken();
        if(userId is null)
            return Unauthorized();

        try
        {
            var review = await _service.AddReviewAsync(request, userId);
            if(review is null)
                return BadRequest();
            return Ok(review);
        }
        catch(Exception e)
        {
            // Exception invoked when user tries to create multiple reviews 
            // for the same product!
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Review>> GetUserReview(int id)
    {
        string? userId = GetUserIdFromToken();
        if(userId is null)
            return Unauthorized();

        var review = await _service.GetReviewAsync(id, userId);
        if(review is null)
            return NotFound();
        return Ok(review);
    }  
}

