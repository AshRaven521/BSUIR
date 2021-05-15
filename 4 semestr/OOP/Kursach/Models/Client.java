package Models;

public class Client
{
    private Machine machine = new Machine();

    private int clientId;
    private String name;

    public void setClientId(int clientId)
    {
        this.clientId = clientId;
    }

    public void setName(String name)
    {
        this.name = name;
    }

    public Machine getMachine()
    {
        return machine;
    }

    public int getClientId()
    {
        return clientId;
    }

    public String getName()
    {
        return name;
    }
}
