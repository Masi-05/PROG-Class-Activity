using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RunWordle
{
    public class Run
    {
        private readonly HttpClient _client = new() { BaseAddress = new Uri("http://localhost:5293/api/wordle/") };
        private string _userId;

        public async Task PlayGameAsync()
        {
            await InitializeUserAsync();

            var wordData = await GetWordAsync();
            if (wordData == null)
            {
                Console.WriteLine("Error: Word data not retrieved.");
                return;
            }

            Console.WriteLine($"Word to guess: {wordData.Word} \n Length: {(wordData.Word.Length + 1) / 2}");

            int attemptsLeft = wordData.AttemptsLeft;
            while (attemptsLeft > 0)
            {
                Console.Write("Enter your guess: ");
                string guess = Console.ReadLine();

                var guessResult = await SubmitGuessAsync(guess);
                if (guessResult == null)
                {
                    Console.WriteLine("Error: Guess submission failed.");
                    continue;
                }

                Console.WriteLine($"Feedback: {guessResult.Feedback}");
                attemptsLeft = guessResult.AttemptsLeft;

               
                if (guessResult.Feedback.Contains("Congratulations! You've guessed the word!"))
                {
                    Console.WriteLine("You guessed the word correctly!");
                    break; 
                }

                attemptsLeft = await GetAttemptsLeftAsync();
                Console.WriteLine($"Attempts left: {attemptsLeft}");
            }

            if (attemptsLeft == 0)
            {
                Console.WriteLine("Game Over! You've used all attempts.");
            }
        }



        private async Task InitializeUserAsync()
        {
            var response = await _client.GetAsync("new-user");
            var data = JsonSerializer.Deserialize<Data.NewUserResponse>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            _userId = data.UserId;
            Console.WriteLine($"Generated User ID: {_userId}");
        }

        private async Task<Data.WordResponse> GetWordAsync()
        {
            var response = await _client.GetAsync("word");
            return JsonSerializer.Deserialize<Data.WordResponse>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        private async Task<Data.GuessResponse> SubmitGuessAsync(string guess)
        {
            var content = new StringContent(JsonSerializer.Serialize(guess), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"guess/{_userId}", content);
            var result = JsonSerializer.Deserialize<Data.GuessResponse>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: {result.Message}");
                return null;
            }

            return result;
        }

        private async Task<int> GetAttemptsLeftAsync()
        {
            var response = await _client.GetAsync($"attempts/{_userId}");
            var data = JsonSerializer.Deserialize<Data.AttemptsResponse>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return data.AttemptsLeft;
        }
    }
}

