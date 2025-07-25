using Microsoft.AspNetCore.Mvc;

namespace AI_Application.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacultyController : Controller
    {
        [HttpGet("review")]
        public IActionResult GetPendingReviews()
        {
            var fakeData = new[]
            {
                new { Id = 1, Question = "What is Agile?", SuggestedAnswer = "AI answer about Agile" },
                new { Id = 2, Question = "Explain CI/CD", SuggestedAnswer = "AI answer about CI/CD" }
            };

            return Ok(fakeData);
        }

        [HttpPost("approve")]
        public IActionResult ApproveAnswer([FromBody] ReviewDecision decision)
        {
            return Ok(new
            {
                Status = decision.IsApproved ? "Approved" : "Rejected",
                QuestionId = decision.QuestionId,
                Comments = decision.Comments,
                ReviewedAt = DateTime.UtcNow
            });
        }

        public class ReviewDecision
        {
            public int QuestionId { get; set; }
            public bool IsApproved { get; set; }
            public required string Comments { get; set; }
        }
    }
}
