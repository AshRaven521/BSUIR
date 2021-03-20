public class Cyclops implements Unit
{

    @Override
    public String getName()
    {
        return "Циклоп";
    }

    @Override
    public int getX()
    {
        return 3;
    }

    @Override
    public int getY()
    {
        return 4;
    }

    @Override
    public void move(int x, int y)
    {
        System.out.println("Циклоп идет из " + x + " в " + y);
    }

    @Override
    public String shoot()
    {
        return "Циклоп выстрелил";
    }
}
