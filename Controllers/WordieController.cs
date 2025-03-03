using Microsoft.AspNetCore.Mvc;

namespace Wordle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordieController : Controller
    {
        private readonly Game _game;

        public WordieController(Game game)
        {
            _game = game;
        }

        [HttpPost("Start")]
        public IActionResult Game()
        {
            _game.StartNew();
            return Ok(new { message = "Start a new game" });
        }
        [HttpPost("guess")]
        public IActionResult Guess([FromBody] string letter) 
        {
            if (string.IsNullOrEmpty(letter) || letter.Length != 1)
            {
                return BadRequest(new { message = "Please provide a single letter." });
            }

            var result = _game.GuessLetter(letter[0]);

            if (_game.IsGameOver())
                return Ok(new { message = "Game Over", maskedWord = result.maskedWord, attemptsLeft = result.attemptsLeft });

            return Ok(new { maskedWord = result.maskedWord, attemptsLeft = result.attemptsLeft });
        }
    }
}
