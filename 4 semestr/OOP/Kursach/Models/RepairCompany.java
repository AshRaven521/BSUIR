package Models;

import java.util.ArrayList;

public class RepairCompany
{
    ArrayList<Machine> machines = new ArrayList<Machine>();
    ArrayList<RepairOrder> orders = new ArrayList<RepairOrder>();
    ArrayList<Client> clients = new ArrayList<Client>();

    public ArrayList<Machine> getMachines()
    {
        return machines;
    }

    public ArrayList<RepairOrder> getOrders()
    {
        return orders;
    }

    public ArrayList<Client> getClients()
    {
        return clients;
    }
}
