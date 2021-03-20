public class Centaur implements Unit
{

    @Override
    public String getName()
    {
        return "Кентавр";
    }

    @Override
    public int getX()
    {
        return 1;
    }

    @Override
    public int getY()
    {
        return 2;
    }

    @Override
    public void move(int x, int y)
    {
        System.out.println("Кентавр идет из " + x + " в " + y);
    }

    @Override
    public String shoot()
    {
        return "Кентавр выстрелил";
    }

}
