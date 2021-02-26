import java.util.Scanner;

public class Main
{
    public static void main(String[] args)
    {
        Boolean menucheaker = true;
        BouquetBuilder builder = new BouquetBuilder();
        FlowerShop shop = new FlowerShop();

        while (menucheaker)
        {
            System.out.println("Меню: ");
            System.out.println("1 - Добавить цветок в магазин\n" +
                    "2 - Цветы, находящиеся в магазине\n" +
                    "3 - Добавить цветок в букет\n" +
                    "4 - Цветы, находящиеся в букете\n" +
                    "5 - Узнать цену букета\n" +
                    "6 - Выход из программы");
            Scanner Scan = new Scanner(System.in);
            int input = Scan.nextInt();

            switch (input)
            {
                case 1:
                {
                    FlowerType type = subTypeMenu();
                    String color = subColorMenu();
                    int quantity = subQuantityMenu();
                    for (int i = 0; i < quantity; i++)
                    {
                        shop.addFlowersInShop(type, 10, color, false);
                    }
                    break;
                }
                case 2:
                {
                    shop.getFlowersInShop();
                    break;
                }
                case 3:
                {
                    FlowerType type = subTypeMenu();
                    int quantity = subQuantityMenu();
                    builder.addFlowersToBouquet(quantity, type, shop);
                    break;
                }
                case 4:
                {
                    builder.getBouquet();
                    break;
                }
                case 5:
                {
                    System.out.println("Итоговая цена букета: " + builder.getBouquetPrice());
                    break;
                }
                case 6:
                {
                    menucheaker = false;
                    break;
                }
            }

        }
    }

    private static FlowerType subTypeMenu()
    {
        System.out.println("Какой цветок добавим?\n" + "1 - Роза\n2 - Василек\n3 - Тюльпан");
        Scanner subScan = new Scanner(System.in);
        int subInput = subScan.nextInt();
        switch (subInput)
        {
            case 1:
            {
                return FlowerType.ROSE;
            }
            case 2:
            {
                return FlowerType.KNAPWEED;
            }
            case 3:
            {
                return FlowerType.TULIP;
            }
            default:
                return null;
        }
    }

    private static String subColorMenu()
    {
        System.out.println("Цвет?");
        Scanner in = new Scanner(System.in);
        String inStr = in.next();

        return inStr;
    }

    private static int subQuantityMenu()
    {
        System.out.println("Количество цветов: ");
        Scanner scan = new Scanner(System.in);
        int input = scan.nextInt();

        return input;
    }
}
