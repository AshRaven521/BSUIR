package Models;

import java.sql.*;

public class Machine implements IConnection
{
    private int machineId;
    private String producingCountry;
    private int yearOfIssue;
    private String brand;

    private Connection connection;
    private Statement state;
    private ResultSet result;

    public int getMachineId()
    {
        return machineId;
    }

    public String getProducingCountry()
    {
        return producingCountry;
    }

    public void setMachineId(int machineId)
    {
        this.machineId = machineId;
    }

    public void setProducingCountry(String producingCountry)
    {
        this.producingCountry = producingCountry;
    }

    public void setYearOfIssue(int yearOfIssue)
    {
        this.yearOfIssue = yearOfIssue;
    }

    public void setBrand(String brand)
    {
        this.brand = brand;
    }

    public int getYearOfIssue()
    {
        return yearOfIssue;
    }

    public String getBrand()
    {
        return brand;
    }

    @Override
    public  void connect()
    {
        String url = "jdbc:mysql://localhost:3306/oop_kursach";
        String user = "root";
        String password = "0000";

        String sqlQuery = "SELECT * from machines";
        try
        {
            Class.forName("com.mysql.cj.jdbc.Driver").getDeclaredConstructor().newInstance();
            connection = DriverManager.getConnection(url, user, password);
            state = connection.createStatement();
            result = state.executeQuery(sqlQuery);

            while (result.next())
            {
                int id = result.getInt("idmachines");
                String producingCountry = result.getString("producing-country");
                System.out.println(id + producingCountry);
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
