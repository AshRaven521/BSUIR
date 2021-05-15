package Models;

public class RepairOrder
{
    private int repairOrderId;
    private String name;
    private int duration;
    private int cost;

    public int getRepairOrderId()
    {
        return repairOrderId;
    }

    public String getName()
    {
        return name;
    }

    public int getDuration()
    {
        return duration;
    }

    public int getCost()
    {
        return cost;
    }

    public void setRepairOrderId(int repairOrderId)
    {
        this.repairOrderId = repairOrderId;
    }

    public void setName(String name)
    {
        this.name = name;
    }

    public void setDuration(int duration)
    {
        this.duration = duration;
    }

    public void setCost(int cost)
    {
        this.cost = cost;
    }
}
