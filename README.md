Rock Paper Scissors Lizard Spock - Backend Service

This project is the backend implementation for the Rock Paper Scissors Lizard Spock game, using C# and .NET 7. The backend exposes RESTful APIs to manage game choices, play the game, and maintain a scoreboard.


##API Endpoints

Choices
```sh
GET /api/choices
```
- Get all the choices that are usable for the game.

```sh
GET /api/choice
```
- Get a randomly generated choice.

Game
```sh
POST /api/game/play
```
- Play a round against a computer opponent.

Scoreboard
```sh
GET /api/game/scores
```
- Get the scoreboard with the 10 most recent results.

```sh
POST /api/game/scores/reset
```
- Resets the scoreboard.
