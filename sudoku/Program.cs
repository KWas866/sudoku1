using System;
using static SudokuGenerator;

class App
{
    static int currentRow = 0;
    static int currentCol = 0;
    static bool[,] playerFilled = new bool[9, 9];

    static void Main()
    {
        Console.WriteLine("Witaj w generatorze Sudoku!");
        Console.WriteLine("Podaj poziom trudności (łatwy, średni, trudny): ");
        string difficulty = Console.ReadLine().ToLower();

        while (true)
        {
            int[,] sudoku = GenerateSudoku(difficulty);
            currentRow = 0;
            currentCol = 0;
            Array.Clear(playerFilled, 0, playerFilled.Length);

            while (true)
            {
                Console.Clear();
                PrintSudoku(sudoku);
                PrintInstructions();
                ConsoleKeyInfo key = Console.ReadKey();

                if (key.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (playerFilled[currentRow, currentCol])
                    {
                        sudoku[currentRow, currentCol] = 0;
                        playerFilled[currentRow, currentCol] = false;
                    }
                }
                else if (key.Key == ConsoleKey.R)
                {
                    break;
                }
                else
                {
                    HandleInput(key, sudoku);
                }
            }
        }
    }

    // Wyświetlanie instrukcji
    static void PrintInstructions()
    {
        Console.WriteLine("\nInstrukcja:");
        Console.WriteLine("Strzałki: Poruszanie się po planszy");
        Console.WriteLine("1-9: Wprowadzenie liczby na planszę");
        Console.WriteLine("Backspace: Usunięcie liczby z aktualnego pola");
        Console.WriteLine("R: Restart gry");
        Console.WriteLine("Esc: Wyjście z programu");
    }

    // Obsługa danych wprowadzanych przez użytkownika
    static void HandleInput(ConsoleKeyInfo key, int[,] sudoku)
    {
        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                currentRow = (currentRow - 1 + 9) % 9;
                break;
            case ConsoleKey.DownArrow:
                currentRow = (currentRow + 1) % 9;
                break;
            case ConsoleKey.LeftArrow:
                currentCol = (currentCol - 1 + 9) % 9;
                break;
            case ConsoleKey.RightArrow:
                currentCol = (currentCol + 1) % 9;
                break;
            case ConsoleKey.Backspace:
                sudoku[currentRow, currentCol] = 0;
                break;

            case ConsoleKey.D1:
            case ConsoleKey.D2:
            case ConsoleKey.D3:
            case ConsoleKey.D4:
            case ConsoleKey.D5:
            case ConsoleKey.D6:
            case ConsoleKey.D7:
            case ConsoleKey.D8:
            case ConsoleKey.D9:
                int number = int.Parse(key.KeyChar.ToString());
                if (IsValidMove(sudoku, currentRow, currentCol, number))
                {
                    sudoku[currentRow, currentCol] = number;
                    playerFilled[currentRow, currentCol] = true; // Oznacza, że pole zostało wypełnione przez gracza
                }
                break;
        }
    }

    static int[,] GenerateSudoku(string difficulty)
    {
        int N = 9;
        int K = GetTargetCellsToRemove(difficulty);

        SudokuGenerator sudokuGenerator = new SudokuGenerator(N, K);
        return sudokuGenerator.GenerateSudoku();
    }

    // Zwraca liczbę komórek do usunięcia na podstawie poziomu trudności.
    static int GetTargetCellsToRemove(string difficulty)
    {
        switch (difficulty)
        {
            case "łatwy":
                return 30;
            case "średni":
                return 40;
            case "trudny":
                return 50;
            default:
                return 40;
        }
    }

    // Sprawdza czy dany ruch gracza jest poprawny
    static bool IsValidMove(int[,] sudoku, int row, int col, int num)
    {
        return !UsedInRow(sudoku, row, num) && !UsedInCol(sudoku, col, num) && !UsedInBox(sudoku, row - row % 3, col - col % 3, num);
    }

    // Sprawdza czy dana liczba wystepuje już we wierszu
    static bool UsedInRow(int[,] sudoku, int row, int num)
    {
        for (int col = 0; col < 9; col++)
        {
            if (sudoku[row, col] == num)
            {
                return true;
            }
        }
        return false;
    }

    // Sprawdza czy dana liczba występuje już w kolumnie
    static bool UsedInCol(int[,] sudoku, int col, int num)
    {
        for (int row = 0; row < 9; row++)
        {
            if (sudoku[row, col] == num)
            {
                return true;
            }
        }
        return false;
    }

    // Sprawdza czy dana liczba występuje już w bloku 3x3
    static bool UsedInBox(int[,] sudoku, int boxStartRow, int boxStartCol, int num)
    {
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                if (sudoku[row + boxStartRow, col + boxStartCol] == num)
                {
                    return true;
                }
            }
        }
        return false;
    }

    // Wyświetla plansze sudoku
    static void PrintSudoku(int[,] sudoku)
    {
        Console.WriteLine("Wygenerowane Sudoku:");

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (i == currentRow && j == currentCol)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                }
                else if (playerFilled[i, j])
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                }

                Console.Write(sudoku[i, j] == 0 ? "  " : $"{sudoku[i, j]} ");
                Console.ResetColor();

                if (j == 2 || j == 5)
                    Console.Write("| ");
            }

            Console.WriteLine();

            if (i == 2 || i == 5)
                Console.WriteLine("------+-------+------");
        }
    }
}
