import java.util.ArrayList;

public class Squad implements Unit
{
    private ArrayList<Unit> warriors = new ArrayList<>();

    @Override
    public String getName()
    {
        return "Отряд";
    }

    @Override
    public int getX()
    {
        return 110;
    }

    @Override
    public int getY()
    {
        return 120;
    }

    @Override
    public void move(int x, int y)
    {
        for (Unit unit: warriors)
        {
            unit.move(7,8);
        }
    }

    @Override
    public String shoot()
    {
        return "Отряд выстрелил";
    }

    public void addUnitToSquad(Unit unit)
    {
        warriors.add(unit);
    }
    public void deleteUnitFromSquad(Unit unit)
    {
        warriors.remove(unit);
    }
}
