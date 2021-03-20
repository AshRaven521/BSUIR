public class Elf implements Unit
{

    @Override
    public String getName()
    {
        return "Эльф";
    }

    @Override
    public int getX()
    {
        return 7;
    }

    @Override
    public int getY()
    {
        return 8;
    }

    @Override
    public void move(int x, int y)
    {
        System.out.println("Эльф летит из " + x + " в " + y);
    }

    @Override
    public String shoot()
    {
        return "Эльф выстрелил";
    }
}
