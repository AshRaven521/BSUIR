package Models;

import Utilities.IConnection;

import java.sql.*;
import java.util.ArrayList;

public class RepairCompany implements IConnection
{
    private ArrayList<Machine> machines = new ArrayList<Machine>();
    private ArrayList<RepairOrder> orders = new ArrayList<RepairOrder>();
    private ArrayList<Client> clients = new ArrayList<Client>();

    private Connection connection;
    private Statement state;
    private ResultSet result;
    String url = "jdbc:mysql://localhost:3306/oop_kursach";
    String user = "root";
    String password = "0000";

    public ArrayList<Machine> getMachines()
    {
        return machines;
    }

    public ArrayList<RepairOrder> getOrders()
    {
        return orders;
    }

    public void setMachines(ArrayList<Machine> machines)
    {
        this.machines = machines;
    }

    public void setOrders(ArrayList<RepairOrder> orders)
    {
        this.orders = orders;
    }

    public void setClients(ArrayList<Client> clients)
    {
        this.clients = clients;
    }

    public ArrayList<Client> getClients()
    {
        return clients;
    }

    public  void connect()
    {

        String sqlQuery = "SELECT * from repairs_order, clients WHERE repairs_order.idclients = clients.idclients";
        try
        {
            Class.forName("com.mysql.cj.jdbc.Driver").getDeclaredConstructor().newInstance();
            connection = DriverManager.getConnection(url, user, password);
            state = connection.createStatement();
            result = state.executeQuery(sqlQuery);

            orders.clear();
            while (result.next())
            {
                RepairOrder order = new RepairOrder();
                order.setRepairOrderId(result.getInt("idrepairs_order"));
                order.setName(result.getString("name"));
                order.setDuration(result.getString("duration"));
                order.setCost(result.getInt("cost"));
                order.setClientName(result.getString("clients.name"));
                orders.add(order);
            }
        }
        catch (Exception exception)
        {
            System.out.println("Something goes wrong: " + exception.getStackTrace());
        }
        finally
        {
            try { connection.close(); } catch(SQLException se) { }
            try { state.close(); } catch(SQLException se) { }
            try { result.close(); } catch(SQLException se) { }
        }
    }

    public void connectToMachinesTable()
    {
        String sqlQuery = "SELECT * from machines,orders_machines WHERE orders_machines.idmachines = machines.idmachines";
        try
        {
            Class.forName("com.mysql.cj.jdbc.Driver").getDeclaredConstructor().newInstance();
            connection = DriverManager.getConnection(url, user, password);
            state = connection.createStatement();
            result = state.executeQuery(sqlQuery);

            while (result.next())
            {
                Machine machine = new Machine();
                machine.setYearOfIssue(result.getInt("year_of_issue"));
                machine.setProducingCountry(result.getString("producing-country"));
                machine.setBrand(result.getString("brand"));
                machine.setMachineId(result.getInt("orders_machines.idmachines"));
                machine.setOrderId(result.getInt("orders_machines.idorders"));
                machines.add(machine);
            }
        }
        catch (Exception exception)
        {
            System.out.println("Something goes wrong: " + exception.getStackTrace());
        }
        finally
        {
            try { connection.close(); } catch(SQLException se) { }
            try { state.close(); } catch(SQLException se) { }
            try { result.close(); } catch(SQLException se) { }
        }
    }
}
