using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ConsoleWordle.Controllers
{
    [ApiController]
    [Route("api/wordle")]
    public class WordleController : ControllerBase
    {
        private readonly App _app;
        private static readonly Dictionary<string, int> UserAttempts = new(); 

        public WordleController(App app)
        {
            _app = app;
        }

        [HttpGet("new-user")]
        public IActionResult GenerateUserId()
        {
            string userId = Guid.NewGuid().ToString();
            UserAttempts[userId] = 0; 
            return Ok(new { userId });
        }

        [HttpGet("word")]
        public IActionResult GetWord()
        {
            var hiddenWord = _app.GetWord();
            var length = _app.GetWordLength();
            return Ok(new { word = hiddenWord, wordLength =length , attemptsLeft = 5 });
        }

        [HttpPost("guess/{userId}")]
        public IActionResult SubmitGuess(string userId, [FromBody] string guess)
        {
            var attemptsLeft = _app.SubmitGuess(userId, guess, out string feedback);

            if (attemptsLeft == -1)
                return BadRequest(new { message = "Game over! No attempts left." });

            if (attemptsLeft == -2)
                return BadRequest(new { message = feedback });

            return Ok(new { message = "Guess recorded.", feedback, attemptsLeft });
        }

        [HttpGet("attempts/{userId}")]
        public IActionResult GetAttemptsLeft(string userId)
        {
            int attemptsLeft = _app.GetAttemptsLeft(userId);
            return Ok(new { attemptsLeft });
        }
    }
}
