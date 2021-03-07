using System;

namespace Tic_Tac_Toe
{
    public enum State {X, O, Empty}
    public class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to Tic-Tac-Toe Game!\nLet's begin!\n");

            Board ticTacBoard = new Board();
            State player;
            Render renderer = new Render();
            int changePlayer = 1;

            while (!ticTacBoard.IsDraw())
            {
                // establish the player order. First is X
                if (changePlayer > 0) player = State.X;
                else player = State.O;

                //the player's move has to be correct, otherwise repeat move just so the player doesnt lose his/her turn
                bool correctMove = false;
                while (!correctMove) 
                {
                    Position position = new Position(ticTacBoard.GetPosition(player)); // it stores the coordinates based on the position given by the user
                    correctMove = ticTacBoard.AddMove(player, position);
                }

                renderer.PrintBoard(ticTacBoard);

                if (ticTacBoard.HasWinner())
                {
                    Console.WriteLine($"{player} won!");
                    break;
                }
                else
                {
                    changePlayer *= -1;
                }
            }
        }
    }

    public class Position
    {
        public int Row { get; }
        public int Column { get; }

        public Position(int pos)
        {
            Row = (9 - pos) / 3;
            Column = (pos - 1) % 3;
        }
    }

    public class Board
    {
        public State[,] board { get; }

        public Board()
        {
            board = new State[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    board[i, j] = State.Empty;
        }

        // checks for wrong inputs and returns a valid value
        public int GetPosition(State player)
        {
            bool wrongNumber = true;
            int userPosition = 0;

            while (wrongNumber)
            {
                Console.WriteLine($"{player}'s turn");
                Console.WriteLine("Position: ");
                string input = Console.ReadLine();
                while (!int.TryParse(input, out int n))
                {
                    Console.WriteLine("Wrong input. Try a number between 1 and 9");
                    input = Console.ReadLine();
                }

                wrongNumber = false;
                userPosition = Int32.Parse(input);
                if (userPosition < 1 || userPosition > 9)
                {
                    Console.WriteLine("Invalid position. Try between 1 and 9");
                    wrongNumber = true;
                }
            }

            return userPosition;
        }


        public bool IsDraw()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (board[i, j] == State.Empty) return false;

            Console.WriteLine("It's a draw!");
            return true;
        }


        public bool AddMove(State player, Position position)
        {
            if (board[position.Row, position.Column] == State.Empty)
            {
                board[position.Row, position.Column] = player;
                return true;
            }
            return false;
        }


        public bool HasWinner()
        {
            for (int i = 0; i < 3; i++)
                if (board[i, 0] != State.Empty)
                    if (board[i, 0] == board[i, 1] && board[i, 0] == board[i, 2]) return true;


            for (int j = 0; j < 3; j++)
                if (board[0, j] != State.Empty)
                    if (board[0, j] == board[1, j] && board[0, j] == board[2, j]) return true;


            if (board[0, 0] != State.Empty)
                if (board[0, 0] == board[1, 1] && board[0, 0] == board[2, 2]) return true;

            if (board[0, 2] != State.Empty)
                if (board[0, 2] == board[1, 1] && board[0, 2] == board[2, 0]) return true;

            return false;
        }
    }

    public class Render
    {
        public void PrintBoard(Board ticTacBoard)
        {
            char[,] board = new char[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    board[i, j] = GetSymbol(ticTacBoard.board[i, j]);

            Console.WriteLine($" {board[0, 0]} | {board[0, 1]} | {board[0, 2]} ");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {board[1, 0]} | {board[1, 1]} | {board[1, 2]} ");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {board[2, 0]} | {board[2, 1]} | {board[2, 2]} ");
        }

        private char GetSymbol(State symbol)
        {
            switch (symbol)
            {
                case (State.O): return 'O';
                case (State.X): return 'X';
                default: return ' ';
            }
        }
    }
}
