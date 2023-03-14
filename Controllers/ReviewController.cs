using Ecommerce.DTOs;
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

}