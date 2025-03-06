<h1> Wordle Game API 🎮 </h1>

Welcome to the Wordle Game API! This API allows players to enjoy a simple word-guessing game with predefined words.

<h2>📝 Rules </h2>

The game selects a random word from a predefined list.

The player has 10 attempts to guess letters.

Correctly guessed letters will be revealed in the word.

Incorrect guesses reduce the number of remaining attempts.

If the player runs out of attempts, they lose and must start a new game.

<h2>🚀 Endpoints </h2>

<h3>🎬 Start Game Endpoint </h3>

Endpoint: /api/Wordie/Start

Method: POST

Request Parameters: None

Response Format:

{

    "message": "Start a new game"
    
}

<h3>🔤 Guess Letter Endpoint </h3>

Endpoint: /api/Wordie/guess

Method: POST

📌 Response Format

✅ Correct Guess:

{

    "maskedWord": "_a_a_a",
    "attemptsLeft": 9
    
}

❌ Game Over:

{

    "message": "Game Over",
    "maskedWord": "banana",
    "attemptsLeft": 0
    
}

🔄 Example Response:

{

    "maskedWord": "_a_a_a",
    "attemptsLeft": 7
    
}

⚠️ Error Handling

Missing or Invalid Letter:

{

    "message": "Please provide a single letter."
    
}

Incorrect Guess:

Reduces the attempt count.

If all attempts are used, a "Game Over" message is displayed.
