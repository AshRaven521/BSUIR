public class Dragon implements Unit
{

    @Override
    public String getName()
    {
        return "Дракон";
    }

    @Override
    public int getX()
    {
        return 5;
    }

    @Override
    public int getY()
    {
        return 6;
    }

    @Override
    public void  move(int x, int y)
    {
        System.out.println("Дракон летит из " + x + " в " + y);
    }

    @Override
    public String shoot()
    {
        return "Дракон выстрелил";
    }

}
