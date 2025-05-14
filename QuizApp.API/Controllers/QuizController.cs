using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Dtos;
using QuizApp.Application.Services;

namespace QuizApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly QuizService _quizService;
        public QuizController(QuizService quizService)
        {
            _quizService = quizService;
        }


        [HttpGet("questions/{quizId}")]
        public async Task<ActionResult<QuestionDto>> GetQuestions(int quizId)
        {
            try
            {
                var questions = await _quizService.GetQuestionsAsync(quizId);
                return Ok(questions);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("validate-answer")]
        public async Task<ActionResult<AnswerFeedbackDto>> ValidateAnswer([FromBody] UserAnswerDto userAnswerDto)
        {
            try
            {
                var feedback = await _quizService.ValidateAnswerAsync(userAnswerDto);
                return Ok(feedback);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("results/{attemptId}")]
        public async Task<ActionResult<QuizResultDto>> GetQuizResults(int attemptId)
        {
            try
            {
                var result = await _quizService.GetQuizResultsAsync(attemptId);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
