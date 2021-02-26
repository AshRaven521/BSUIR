public class Rose extends Flower
{


    @Override
    public String getColor()
    {
        return super.color;
    }

    @Override
    public void setColor(String color)
    {
        super.color = color;
    }

    @Override
    public int getPrice()
    {
        return super.price;
    }

    @Override
    public void setPrice(int price)
    {
        super.price = price;
    }

    @Override
    public Boolean getIsSelectedInBouquet()
    {
        return super.isSelectedInBouquet;
    }

    @Override
    public void setIsSelectedInBouquet(Boolean isSelectedInBouquet)
    {
        super.isSelectedInBouquet = isSelectedInBouquet;
    }

    @Override
    public void Add()
    {
        System.out.println("Роза добавлена");
    }

    Rose(int price, Boolean isSelectedInBouquet, String color)
    {
        super(20, isSelectedInBouquet, color);
        name = "Роза";
        typeName = "ROSE";
    }

}
