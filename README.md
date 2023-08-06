# Reversed Tic-Tac-Toe for Windows

## Overview

The "Reversed Tic-Tac-Toe" project is a Windows Forms application that implements the classic Tic-Tac-Toe game. The objective is to create a simple yet functional graphical user interface (GUI) for the game, allowing the user to configure game settings, play against another player, or challenge the computer, and receive visual feedback for the game's progress and outcome.

## Goals

The primary goals of this project are as follows:

1. **Windows Forms Development:** Build a basic Windows Forms application that includes the necessary controls and components to interact with the user and display game information.

2. **Event Handling:** Utilize event handling to respond to user actions, such as button clicks, and update the game's state accordingly.

3. **Matrix Representation:** Create a matrix of buttons to represent the game board, where each button corresponds to a cell on the Tic-Tac-Toe grid.

4. **Player Interaction:** Allow two players to take turns placing their symbols (X or O) on the board until a winning condition or a tie is reached. Alternatively, provide an option to play against the computer.

5. **Game Outcome Display:** Show appropriate message boxes when the game ends, indicating the winner or a tie, and prompt the user to start a new game.

6. **Code Quality and Standards:** Adhere to coding standards and best practices for clean, readable, and maintainable code.

## Functionality

The "Reversed Tic-Tac-Toe" application offers the following functionalities to the users:

1. **Settings Window:** The application starts by displaying a settings window, allowing the user to choose the game mode. The user can select from the following options:
   - **2 Players:** Play a traditional two-player game where two human players take turns.
   - **Player vs. Computer:** Challenge the computer to a game of Tic-Tac-Toe.

   If the "Player vs. Computer" option is chosen, the settings window will also allow the user to enter their name.

2. **Game Board Window:** After the user clicks the "Play" button in the settings window, the main game board window appears. The size of the board is determined by the user's input for rows and columns.

3. **Matrix of Buttons:** The game board consists of a matrix of buttons, where each button represents a cell on the Tic-Tac-Toe grid. Players can click the buttons to place their symbols (X or O) on the board.

4. **Gameplay:** If the "2 Players" option is chosen, two human players will take turns placing their symbols on the board until one of them wins or the game ends in a tie. If the "Player vs. Computer" option is chosen, the player will play against the computer, and the computer's moves will be automatically determined.

5. **Outcome Messages:** When the game ends, a message box will appear, indicating the winner (if any) or a tie. The application will ask if the user wants to start a new game.

## Project Structure

The project should consist of the following components:

1. **Form for Settings:** This form will allow the user to choose game settings, such as the game mode and the name of the second player (if applicable).

2. **Main Game Form:** This form will display the game board and handle the interactions with the user during the game.

3. **Game Logic:** A separate class or module that contains the game logic, including checking for win conditions, determining the next move (in the case of player vs. computer mode), and resetting the game.

## Installation and Usage

To run the "Reversed Tic-Tac-Toe" application:

1. Clone the repository to your local machine using the following command:

   ```
   git clone [https://github.com/your-username/tic-tac-toe.git](https://github.com/GuyBenja/Reverse-Tic-Tac-Toe-using-WinForms.git)
   ```

2. Open the solution file in Visual Studio or your preferred C# development environment.

3. Build the solution to ensure all dependencies are resolved and the projects are compiled.

4. Run the application by starting the project.

5. The settings window will appear first, allowing you to configure the game mode and, if applicable, enter the player's name.

6. Click the "Play" button, and the main game board window will be displayed, allowing you to play the game.

## Contribution and Support

Contributions, suggestions, and bug reports are welcome! Feel free to submit pull requests or open issues for any improvements or changes you'd like to propose.

If you encounter any issues or have questions related to the project, please create an issue in the repository.
