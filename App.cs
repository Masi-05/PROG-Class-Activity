using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleWordle
{
    public class App
    {
        private static readonly Random random = new();
        private static string word = string.Empty;
        private readonly Dictionary<string, int> userAttempts = new();

        public App()
        {
            if (string.IsNullOrEmpty(word))
            {
                word = GetRandomWord();
            }
        }

        public string GetWord()
        {
            return string.Join(" ", new string('_', word.Length).ToCharArray());
        }

        public int SubmitGuess(string userId, string guess, out string feedback)
        {
            if (guess.Length != word.Length)
            {
                feedback = $"Invalid guess! Your guess must be exactly {word.Length} letters.";
                return -2;
            }

            if (!userAttempts.ContainsKey(userId))
            {
                userAttempts[userId] = 0;
            }

            if (userAttempts[userId] >= 5)
            {
                feedback = "Game over! You've used all 5 attempts.";
                return -1;
            }

            userAttempts[userId]++;
            feedback = GenerateFeedback(guess);

           
            if (guess.Equals(word, StringComparison.OrdinalIgnoreCase))
            {
                feedback = "Congratulations! You've guessed the word!";
                return 0;
            }

            return 5 - userAttempts[userId]; 
        }


        public int GetAttemptsLeft(string userId)
        {
            return userAttempts.ContainsKey(userId) ? 5 - userAttempts[userId] : 5;
        }

        public int GetWordLength()
        {
            return word.Length;
        }

        private string GenerateFeedback(string guess)
        {
            char[] feedback = new char[word.Length];

            for (int i = 0; i < word.Length; i++)
            {
                if (guess[i] == word[i])
                {
                    feedback[i] = 'G';
                }
                else if (word.Contains(guess[i]))
                {
                    feedback[i] = 'Y';
                }
                else
                {
                    feedback[i] = 'X';
                }
            }

            return string.Join(", ", feedback);
        }

        private string GetRandomWord()
        {
            string filePath = "Dictionary/List.txt";
            if (!File.Exists(filePath))
            {
                return "Error, no file located!";
            }

            var words = File.ReadAllLines(filePath).Where(w => w.Length >= 5).ToList();
            return words.Count > 0 ? words[random.Next(words.Count)] : "List is empty!";
        }
    }
}
