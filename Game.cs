namespace Wordle
{
    public class Game
    {
        private readonly List<string> _wordList = new() { "coconut", "banana", "cake", "sam", "cheese" };
        private string _selectedWord;
        private int _attemptsLeft;
        private HashSet<char> _guessedLetters = new();

        public Game()
        {
            StartNew();
        }

        public void StartNew()
        {
            var random = new Random();
            _selectedWord = _wordList[random.Next(_wordList.Count)].ToLower(); 
            _attemptsLeft = 10;
            _guessedLetters.Clear();
        }

        public (string maskedWord, int attemptsLeft) GuessLetter(char letter)
        {
            letter = char.ToLower(letter); 

            if (!_selectedWord.Contains(letter) && !_guessedLetters.Contains(letter))
                _attemptsLeft--;

            _guessedLetters.Add(letter);
            var maskedWord = new string(_selectedWord.Select(c => _guessedLetters.Contains(c) ? c : '_').ToArray());

            return (maskedWord, _attemptsLeft);
        }

        public bool IsGameOver() => _attemptsLeft <= 0;
    }
}
