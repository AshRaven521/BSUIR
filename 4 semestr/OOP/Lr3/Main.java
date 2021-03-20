import java.util.Scanner;

public class Main {
    private static Army army = new Army();
    private static Squad squad = new Squad();
    private static Squad subSquad = new Squad();

    public static void main(String[] args) {
        Boolean menuCheaker = true;
        Centaur centaur = new Centaur();
        Elf elf = new Elf();

        while (menuCheaker) {
            System.out.println("Меню:");
            System.out.println("1 - Сформировать армию\n" +
                    "2 - Вывести результат на экран\n" +
                    "3 - Выход из программы");

            Scanner scan = new Scanner(System.in);
            int input = scan.nextInt();

            switch (input) {
                case 1: {
                    subArmyMenu();
                    break;
                }
                case 2: {
                    army.move(5, 6);

                    break;
                }
                case 3: {
                    menuCheaker = false;
                }
            }
        }
    }


    private static void subArmyMenu() {
        System.out.println("1 - Добавить отряд\n" +
                "2 - Добавить одного персонажа\n");

        Scanner scan = new Scanner(System.in);
        int userInput = scan.nextInt();

        switch (userInput)
        {
            case 1:
            {
                subSquadMenu();
                army.addUnitInArmy(squad);
                break;
            }
            case 2:
            {
                subPrintMenu();

                Scanner scanner = new Scanner(System.in);
                int input = scanner.nextInt();

                switch (input) {
                    case 1: {
                        army.addUnitInArmy(new Centaur());
                        break;
                    }
                    case 2: {
                        army.addUnitInArmy(new Cyclops());
                        break;
                    }
                    case 3: {
                        army.addUnitInArmy(new Dragon());
                        break;
                    }
                    case 4: {
                        army.addUnitInArmy(new Elf());
                        break;
                    }
                    case 5: {
                        army.addUnitInArmy(new Hydra());
                        break;
                    }
                    case 6: {
                        army.addUnitInArmy(new Knight());
                        break;
                    }
                    case 7: {
                        army.addUnitInArmy(new Minotaur());
                        break;
                    }
                    case 8: {
                        army.addUnitInArmy(new Orc());
                        break;
                    }
                }
            }
        }
    }

    private static void subSquadMenu()
    {
        Boolean flag = true;
        while (flag)
        {
            System.out.println("Каких персонажей добавим?");
            System.out.println("1 - Кентавр\n" +
                    "2 - Циклоп\n" +
                    "3 - Дракон\n" +
                    "4 - Эльф\n" +
                    "5 - Гидра\n" +
                    "6 - Рыцарь\n" +
                    "7 - Минотавр\n" +
                    "8 - Орк\n" +
                    "9 - Выход");

            Scanner scanner = new Scanner(System.in);
            int input = scanner.nextInt();

            switch (input)
            {
                case 1: {
                    squad.addUnitToSquad(new Centaur());
                    break;
                }
                case 2: {
                    squad.addUnitToSquad(new Cyclops());
                    break;
                }
                case 3: {
                    squad.addUnitToSquad(new Dragon());
                    break;
                }
                case 4: {
                    squad.addUnitToSquad(new Elf());
                    break;
                }
                case 5: {
                    squad.addUnitToSquad(new Hydra());
                    break;
                }
                case 6: {
                    squad.addUnitToSquad(new Knight());
                    break;
                }
                case 7: {
                    squad.addUnitToSquad(new Minotaur());
                    break;
                }
                case 8: {
                    squad.addUnitToSquad(new Orc());
                    break;
                }
                case 9:
                {
                    flag = false;
                }
            }

        }
    }

    private static void subPrintMenu()
    {
        System.out.println("Каких персонажей добавим?");
        System.out.println("1 - Кентавр\n" +
                "2 - Циклоп\n" +
                "3 - Дракон\n" +
                "4 - Эльф\n" +
                "5 - Гидра\n" +
                "6 - Рыцарь\n" +
                "7 - Минотавр\n" +
                "8 - Орк\n" +
                "9 - Выход");
    }
}
