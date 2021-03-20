import java.util.ArrayList;

public class Army
{
    ArrayList<Unit> armyWarriors = new ArrayList<>();

    private String shootingResult = "";
    private String movingResult = "";

    public void addUnitInArmy(Unit unit)
    {
        armyWarriors.add(unit);
    }

    public void deleteUnitFromArmy(Unit unit)
    {
        armyWarriors.remove(unit);
    }

    public String shoot()
    {
        for (int i = 0; i < armyWarriors.size(); i++)
        {
            shootingResult += armyWarriors.get(i).shoot() + " в армии" + "\n";
        }
        return shootingResult;
    }

    public void move(int a, int b)
    {

        for (Unit unit: armyWarriors)
        {
            unit.move(9,10);
        }

    }
}
