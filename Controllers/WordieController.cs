using Microsoft.AspNetCore.Mvc;

namespace Wordle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordieController : ControllerBase
    {
        private readonly Game _game;

        public WordieController(Game game)
        {
            _game = game;
        }

        [HttpPost("start")]
        public IActionResult StartGame()
        {
            _game.StartNew();
            return Ok(new { message = "Game started", wordLength = _game.GetWord().Length });
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

        // New endpoint to retrieve the current word
        [HttpGet("word")]
        public IActionResult GetWord()
        {
            return Ok(new { word = _game.GetWord() });
        }
    }
}
