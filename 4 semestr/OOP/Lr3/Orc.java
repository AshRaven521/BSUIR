public class Orc implements Unit
{

    @Override
    public String getName()
    {
        return null;
    }

    @Override
    public int getX()
    {
        return 15;
    }

    @Override
    public int getY()
    {
        return 16;
    }

    @Override
    public void move(int x, int y)
    {
        System.out.println("Орк идет из " + x + " в " + y);
    }

    @Override
    public String shoot()
    {
        return "Орк выстрелил";
    }
}
