import java.util.Random;
import java.util.Scanner;


public class TicTacToe
{
    public static final char DOT_EMPTY = ' ';
    public static final char DOT_X = 'X';
    public static final char DOT_O = '0';
    public static char[][] map = new char[3][3];
    public static char[][] previousMap = new char[3][3];
    public static int size;  // Размер поля
    public static int block; // Сколько должно быть одинаковых значение подряд для победы
    public static Scanner sc = new Scanner(System.in);
    public static Random rand = new Random();
    private TicTacToeMemento ticTacToeMemento = new TicTacToeMemento(this);


    public char[][] getArray()
    {
        return map;
    }

    public void save(TicTacToeMemento memento)
    {
        memento.setArray(getArray());
    }

    public void restore(TicTacToeMemento memento)
    {
        map = memento.getArray();
    }



    public void mainGame()
    {
        customizeGame();
        initMap();
        printMap();
        while (true)
        {
            save(ticTacToeMemento);
            humanTurn();
            printMap();
            if (checkWin(DOT_X))
            {
                System.out.println("Выиграл человек!");
                break;
            }
            if (isMapFull())
            {
                System.out.println("Ничья!");
                break;
            }
            aiTurn();
            printMap();
            if (checkWin(DOT_O))
            {
                System.out.println("Выиграл компьютер!");
                break;
            }
            if (isMapFull())
            {
                System.out.println("Ничья!");
                break;
            }
            System.out.println("Хотите вернуться на предыдущий ход?(Y/N)");
            Scanner scan = new Scanner(System.in);
            while (scan.hasNext())
            {
                String str = scan.next();
                if(str.equals("Y"))
                {
                    restore(ticTacToeMemento);
                    for(int i = 0; i < previousMap.length; i++)
                    {
                        System.arraycopy(map[i], 0, previousMap[i], 0, previousMap[i].length);
                    }
                    printMap();
                    break;
                }
                else
                {
                    if(str.equals("N"))
                    {
                        printMap();
                        break;
                    }

                }
            }
        }
        System.out.println("Игра окончена");
    }


    /** Запрашиваем размер поля и условия победы */
    void customizeGame()
    {
        do {
            System.out.print("\nВведите размер игрового поля [3-10]: ");
            size = sc.nextInt();
        } while(size<3 || size>10);

        do {
            System.out.print("Сколько блоков для победы? [3-"+size+"]: ");
            block = sc.nextInt();
        } while (block<3 || block>size);

    }

    /** Логика победы изменина для работы с любым размером поля. */
    boolean checkWin(char symb)
    {
        for (int col=0; col<size-block+1; col++)
        {
            for (int row=0; row<size-block+1; row++)
            {
                if (checkDiagonal(symb, col, row) || checkLines(symb, col, row))
                {
                    return true;
                }
            }
        }
        return false;
    }

    /** Проверяем диагонали */
    static boolean checkDiagonal(char symb, int offsetX, int offsetY)
    {
        boolean toright, toleft;
        toright = true;
        toleft = true;
        for (int i=0; i<block; i++)
        {
            toright &= (map[i+offsetX][i+offsetY] == symb);
            toleft &= (map[block-i-1+offsetX][i+offsetY] == symb);
        }

        if (toright || toleft)
        {
            return true;
        }

        return false;
    }

    /** Проверяем горизонтальные и вертикальные линии */
    boolean checkLines(char symb, int offsetX, int offsetY)
    {
        boolean cols, rows;
        for (int col=offsetX; col<block+offsetX; col++)
        {
            cols = true;
            rows = true;
            for (int row=offsetY; row<block+offsetY; row++)
            {
                cols &= (map[col][row] == symb);
                rows &= (map[row][col] == symb);
            }

            if (cols || rows)
            {
                return true;
            }
        }

        return false;
    }

    boolean isMapFull()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (map[i][j] == DOT_EMPTY)
                {
                    return false;
                }
            }
        }
        return true;
    }

    void aiTurn() {
        int x, y;
        do {
            x = rand.nextInt(size);
            y = rand.nextInt(size);
        } while (!isCellValid(x, y));

        System.out.println("Компьютер сделал ход " + (x + 1) + " " + (y + 1));

        map[y][x] = DOT_O;
    }

    void humanTurn()
    {
        int x, y;
        do {
            System.out.println("Введите координаты в виде X Y");
            x = sc.nextInt() - 1;
            y = sc.nextInt() - 1;
        } while (!isCellValid(x, y));

        map[y][x] = DOT_X;
    }

    boolean isCellValid(int x, int y)
    {
        if (x < 0 || x >= size || y < 0 || y >= size)
        {
            return false;
        }
        if (map[y][x] == DOT_EMPTY)
        {
            return true;
        }
        return false;
    }

    void initMap()
    {
        map = new char[size][size];
        previousMap = new char[size][size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                map[i][j] = DOT_EMPTY;
                previousMap[i][j] = DOT_EMPTY;
            }
        }
    }

    void printMap()
    {
        for (int i = 0; i <= size; i++)
        {
            System.out.print(i + " ");
        }
        System.out.println();
        for (int i = 0; i < size; i++)
        {
            System.out.print((i + 1) + " ");
            for (int j = 0; j < size; j++)
            {
                System.out.print(map[i][j] + " ");
            }
            System.out.println();
        }
        System.out.println();
    }
}
