public class Hydra implements Unit
{

    @Override
    public String getName()
    {
        return "Гидра";
    }

    @Override
    public int getX()
    {
        return 9;
    }

    @Override
    public int getY()
    {
        return 10;
    }

    @Override
    public void move(int x, int y)
    {
        System.out.println("Гидра идет из " + x + " в " + y);
    }

    @Override
    public String shoot()
    {
        return "Гидра выстрелила";
    }
}
