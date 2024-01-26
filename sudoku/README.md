# Sudoku Generator
## Opis
Generator Sudoku w języku C#, który umożliwia generowanie planszy o różnym poziomie trudności. Gracz może następnie wypełnić planszę zgodnie z zasadami Sudoku, a program sprawdza poprawność ruchów i umożliwia restart gry.
## Informacje Techniczne
- Język programowania: C#
- Framework: .NET8
- Program wykorzystuje konsolowy interfejs użytkownika.
- Autor: Karolina Wasilewska
## Instrukcja Obsługi
- Poruszanie po planszy: Strzałki góra, dół, lewo, prawo.
- Wprowadzanie liczby na planszę: Klawisze 1-9.
- Usunięcie liczby z aktualnego pola: Klawisz Backspace.
- Restart gry: Klawisz R.
- Wyjście z programu: Klawisz Esc.
Poziomy trudności: "łatwy", "średni", "trudny".<br>
Domyślny poziom trudności: "średni".

## Ogólne zasady gry
- Plansza: Gra odbywa się na planszy o wymiarach 9x9 podzielonej na 9 obszarów 3x3, zwanymi regionami.
- Liczby od 1 do 9: Celem jest umieszczenie cyfr od 1 do 9 w każdym rzędzie, kolumnie i regionie tak, aby nie było powtórzeń.
- Początkowe cyfry: Na początku gry część komórek jest już wypełniona liczbami. Te liczby stanowią punkt wyjścia dla rozwiązania łamigłówki.
- Rozwiązanie jednoznaczne: Prawidłowo ułożona łamigłówka Sudoku ma tylko jedno możliwe rozwiązanie.
- Strategia i logiczne myślenie: Gra opiera się na logicznych rozważaniach, eliminacji możliwości i analizie, bez potrzeby zgadywania.

## Opis klas
### Klasa SudokuGenerator
#### Metody
- GenerateSudoku(): Generuje planszę Sudoku zgodnie z zadanym rozmiarem i ilością brakujących cyfr.
- FillDiagonal(): Wypełnia diagonalne bloki o rozmiarze SRN x SRN.
- UnUsedInBox(int rowStart, int colStart, int num): Sprawdza, czy dana liczba nie występuje w danym bloku 3x3.
- FillBox(int row, int col): Wypełnia blok 3x3 liczbami, zapewniając unikalność w wierszach, kolumnach i bloku.
- RandomGenerator(int num): Generuje losową liczbę całkowitą z zakresu 1 do num.
- CheckIfSafe(int i, int j, int num): Sprawdza, czy możliwe jest bezpieczne umieszczenie danej liczby w danym miejscu.
- UnUsedInRow(int i, int num): Sprawdza, czy dana liczba nie występuje już w danym wierszu.
- UnUsedInCol(int j, int num): Sprawdza, czy dana liczba nie występuje już w danej kolumnie.
- FillRemaining(int i, int j): Funkcja rekurencyjna do uzupełniania pozostałej części planszy.
- RemoveKDigits(): Usuwa losowo K cyfr z planszy.
### Klasa App
#### Zmienne
- currentRow: Aktualny wiersz wskazywany przez gracza.
- currentCol: Aktualna kolumna wskazywana przez gracza.
- playerFilled: Tablica przechowująca informacje o wypełnionych przez gracza polach.
#### Metody
- Main(): Główna metoda programu, umożliwiająca interakcję z graczem.
- PrintInstructions(): Wyświetla instrukcje obsługi gry.
- HandleInput(ConsoleKeyInfo key, int[,] sudoku): Obsługuje wprowadzanie danych przez gracza.
- GenerateSudoku(string difficulty): Generuje planszę Sudoku na podstawie wybranego poziomu trudności.
- GetTargetCellsToRemove(string difficulty): Zwraca liczbę komórek do usunięcia na podstawie poziomu trudności.
- IsValidMove(int[,] sudoku, int row, int col, int num): Sprawdza, czy dany ruch gracza jest poprawny.
- UsedInRow(int[,] sudoku, int row, int num): Sprawdza, czy dana liczba już występuje w danym wierszu.
- UsedInCol(int[,] sudoku, int col, int num): Sprawdza, czy dana liczba już występuje w danej kolumnie.
- UsedInBox(int[,] sudoku, int boxStartRow, int boxStartCol, int num): Sprawdza, czy dana liczba już występuje w danym bloku 3x3.
- PrintSudoku(int[,] sudoku): Wyświetla planszę Sudoku w konsoli, wraz z wyróżnieniem aktualnego pola i pól wypełnionych przez gracza.
