public class FlowerFactory
{

    public static Flower getInstance(FlowerType type, int price, String color, Boolean isSelectedInBouquet)
    {
        switch (type)
        {
            case ROSE:
                return new Rose(price, isSelectedInBouquet, color);

            case KNAPWEED:
                return new Knapweed(price, isSelectedInBouquet, color);

            case TULIP:
                return new Tulip(price, isSelectedInBouquet, color);

            default:
                return null;
        }
    }
}
