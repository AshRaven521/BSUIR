package Models;

public class RepairOrder
{
    private int repairOrderId;
    private String name;
    private String duration;
    private int cost;
    private String ClientName;

    public void setClientName(String clientName)
    {
        this.ClientName = clientName;
    }

    public String  getClientName()
    {
        return ClientName;
    }

    public int getRepairOrderId()
    {
        return repairOrderId;
    }

    public String getName()
    {
        return name;
    }

    public String getDuration()
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

    public void setDuration(String duration)
    {
        this.duration = duration;
    }

    public void setCost(int cost)
    {
        this.cost = cost;
    }
}
