import java.util.Scanner;

public class Main
{
    public static void main(String[] args)
    {

        TicTacToe ticTacToe = new TicTacToe();
        Scanner scan = new Scanner(System.in);
        boolean menuCheaker = true;

        while (menuCheaker)
        {
            System.out.println("Меню: ");
            System.out.println("1 - Запуск\n" +
                    "2 - Выход\n");
            int input = scan.nextInt();
            switch (input)
            {
                case 1:
                {
                    ticTacToe.mainGame();
                    break;
                }


                case 2:
                {
                    menuCheaker = false;
                    break;
                }
            }

        }

    }
}
