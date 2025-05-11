using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using server.Extensions;
using server.Models;
using TaskTracker.Models;
using TaskTracker.Services;

namespace TaskTracker.Controllers
{
    [Route("api/recommendations")]
    [ApiController]
    [Authorize]
    public class RecommendationController : ControllerBase
    {
        private readonly RecommendationService _recommendationService;
        private readonly UserManager<AppUser> _userManager;

        public RecommendationController(RecommendationService recommendationService, UserManager<AppUser> userManager)
        {
            _recommendationService = recommendationService;
            _userManager = userManager;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateRecommendations()
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return Unauthorized();
            }

            await _recommendationService.GenerateRecommendationsForUser(user.Id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUserRecommendations()
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return Unauthorized();
            }

            var recommendations = await _recommendationService.GetUserRecommendations(user.Id);
            return Ok(recommendations);
        }

        [HttpPut("{id}/read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return Unauthorized();
            }

            var success = await _recommendationService.MarkRecommendationAsRead(id, user.Id);
            
            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }
    }
} 