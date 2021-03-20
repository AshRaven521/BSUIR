public class Knight implements Unit
{

    @Override
    public String getName()
    {
        return "Рыцарь";
    }

    @Override
    public int getX()
    {
        return 11;
    }

    @Override
    public int getY()
    {
        return 12;
    }

    @Override
    public void move(int x, int y)
    {
        System.out.println("Рыцарь идет из " + x + " в " + y);
    }

    @Override
    public String shoot()
    {
        return "Рыцарь выстрелил";
    }
}
