using System;

class SudokuGenerator
{
    private int[,] mat;
    private int N; // numer kolumny/wiersza
    private int SRN; // pierwiastek kwadratowy z N
    private int K; // liczba usuniętych cyfr
    private Random random;

    public SudokuGenerator(int N, int K)
    {
        this.N = N;
        this.K = K;
        this.random = new Random();

        double SRNd = Math.Sqrt(N);
        SRN = (int)SRNd;

        mat = new int[N, N];
    }

    public int[,] GenerateSudoku()
    {
        FillDiagonal();
        FillRemaining(0, SRN);
        RemoveKDigits();

        return mat;
    }
    // Wypełnia diagonalne bloki o rozmiarze SRN x SRN
    private void FillDiagonal()
    {
        for (int i = 0; i < N; i = i + SRN)
        {
            FillBox(i, i);
        }
    }

    // sprawdza, czy dana liczba nie występuje w danym bloku 3x3
    private bool UnUsedInBox(int rowStart, int colStart, int num)
    {
        for (int i = 0; i < SRN; i++)
        {
            for (int j = 0; j < SRN; j++)
            {
                if (mat[rowStart + i, colStart + j] == num)
                {
                    return false;
                }
            }
        }
        return true;
    }

    // Wypełnia 3 x 3 blok.
    private void FillBox(int row, int col)
    {
        int num;
        for (int i = 0; i < SRN; i++)
        {
            for (int j = 0; j < SRN; j++)
            {
                do
                {
                    num = RandomGenerator(N);
                }
                while (!UnUsedInBox(row, col, num));

                mat[row + i, col + j] = num;
            }
        }
    }

    // Random generator
    private int RandomGenerator(int num)
    {
        return (int)Math.Floor((double)(random.NextDouble() * num + 1));
    }

    // Sprawdza czy można umieścić umieścić cyfre w danym miejscu
    private bool CheckIfSafe(int i, int j, int num)
    {
        return (UnUsedInRow(i, num) && UnUsedInCol(j, num) && UnUsedInBox(i - i % SRN, j - j % SRN, num));
    }

    // sprawdza czy cyfra występuje we wierszu
    private bool UnUsedInRow(int i, int num)
    {
        for (int j = 0; j < N; j++)
        {
            if (mat[i, j] == num)
            {
                return false;
            }
        }
        return true;
    }

    // sprawdza czy cyfra występuje w kolumnie
    private bool UnUsedInCol(int j, int num)
    {
        for (int i = 0; i < N; i++)
        {
            if (mat[i, j] == num)
            {
                return false;
            }
        }
        return true;
    }

    // Funkcja rekurencyjna do wypełnienia pozostałej macierzy
    private bool FillRemaining(int i, int j)
    {
        if (j >= N && i < N - 1)
        {
            i = i + 1;
            j = 0;
        }
        if (i >= N && j >= N)
        {
            return true;
        }

        if (i < SRN)
        {
            if (j < SRN)
                j = SRN;
        }
        else if (i < N - SRN)
        {
            if (j == (int)(i / SRN) * SRN)
                j = j + SRN;
        }
        else
        {
            if (j == N - SRN)
            {
                i = i + 1;
                j = 0;
                if (i >= N)
                    return true;
            }
        }

        for (int num = 1; num <= N; num++)
        {
            if (CheckIfSafe(i, j, num))
            {
                mat[i, j] = num;
                if (FillRemaining(i, j + 1))
                    return true;

                mat[i, j] = 0;
            }
        }
        return false;
    }

    // usuwa losowo K cyfr z planszy
    private void RemoveKDigits()
    {
        int count = K;
        while (count != 0)
        {
            int cellId = RandomGenerator(N * N) - 1;

            int i = (cellId / N);
            int j = cellId % N;
            if (j != 0)
                j = j - 1;

            if (mat[i, j] != 0)
            {
                count--;
                mat[i, j] = 0;
            }
        }
    }
}