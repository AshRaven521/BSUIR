import java.util.ArrayList;
import java.util.NoSuchElementException;

public class BouquetBuilder
{
    private int totalPrice;
    private ArrayList<Flower> bouquet = new ArrayList();

    public int getTotalPrice()
    {
        return totalPrice;
    }

    public int getBouquetPrice()
    {

        for (Flower flower : bouquet)
        {
            totalPrice += flower.getPrice();
        }

        return totalPrice;
    }

    public void getBouquet()
    {

        for (Flower flower : bouquet)
        {
            if (flower.getIsSelectedInBouquet())
            {
                System.out.println(flower.name + " " + flower.color);
            }
        }
    }

    public void addFlowerToBouquet(FlowerType type, FlowerShop shop)
    {
        try
        {
            Flower flower = shop.getFlowers().stream()
                    .filter(x -> !x.getIsSelectedInBouquet() && x.typeName == type.name()).findFirst().get();
            flower.setIsSelectedInBouquet(true);

            bouquet.add(flower);
            flower.Add();
        }
        catch (NoSuchElementException e)
        {
            System.out.println("Нет такого цветка в магазине");
        }


    }

    public void addFlowersToBouquet(int quantity, FlowerType type, FlowerShop shop)
    {
        for (int i = 0;i < quantity; i++)
        {
            addFlowerToBouquet(type, shop);
        }
    }

}
