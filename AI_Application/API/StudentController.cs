using Microsoft.AspNetCore.Mvc;

namespace AI_Application.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        [HttpPost("ask")]
        public IActionResult AskQuestion([FromBody] StudentQuestion question)
        {
            var response = new
            {
                Question = question.QuestionText,
                Answer = "AI: This is an auto-generated answer.",
                RespondedAt = DateTime.UtcNow
            };

            return Ok(response);
        }
    }

    public class StudentQuestion
    {
        public required string StudentId { get; set; }
        public required string QuestionText { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
