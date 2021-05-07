package Models;

public class Client
{
    private Machine machine = new Machine();

    private int clientId;
    private String name;

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
    //private int id = machine.getMachineId();
}
