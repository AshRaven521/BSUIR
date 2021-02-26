import java.util.ArrayList;

public class FlowerShop
{
    private ArrayList<Flower> flowerList = new ArrayList();


    public void getFlowersInShop()
    {

        for (Flower flower : flowerList)
        {
            System.out.println(flower.name + " " + flower.color);
        }
    }

    public ArrayList<Flower> getFlowers()
    {
        return flowerList;
    }

    public void addFlowersInShop(FlowerType type, int price, String color, Boolean isSelectedInBouquet)
    {
        Flower flow = FlowerFactory.getInstance(type, price, color, isSelectedInBouquet);
        flowerList.add(flow);
        flow.Add();
    }
}
