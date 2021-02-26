public abstract class Flower
{
    protected String color;
    protected int price;
    protected Boolean isSelectedInBouquet;
    protected String name;
    protected String typeName;

    public abstract String getColor();

    public abstract void setColor(String color);

    public abstract int getPrice();

    public abstract void setPrice(int price);

    public abstract Boolean getIsSelectedInBouquet();

    public abstract void setIsSelectedInBouquet(Boolean isSelectedInBouquet);

    public abstract void Add();

    Flower(int price, Boolean isSelectedInBouquet, String color)
    {
        this.price = price;
        this.isSelectedInBouquet = isSelectedInBouquet;
        this.color = color;
    }
}
